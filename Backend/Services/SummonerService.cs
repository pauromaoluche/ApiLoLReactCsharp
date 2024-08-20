using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public class SummonerService : ISummoner
    {
        private readonly HttpClient _httpClient;
        private readonly IChampion _champion;
        private readonly IMatch _match;

        public SummonerService(HttpClient httpClient, IChampion champion, IMatch match)
        {
            _httpClient = httpClient;
            _champion = champion;
            _match = match;
            _httpClient.BaseAddress = new Uri("https://americas.api.riotgames.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://developer.riotgames.com");
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-d041aa16-7b3f-431c-b91d-c9ccc1cb178c");
        }

        public async Task<CombinedSummonerDTO> GetSummonerInformation(string summonerName, string tag)
        {
            var accountInfo = await GetAccountInfo(summonerName, tag);

            var summonerInfo = await GetSummonerInfo(accountInfo.puuid);

            var summonerLeagueInfo = await GetSummonerLeagueInfo(summonerInfo.id);

            var topChampMastery = await GetTopChampionMastery(accountInfo.puuid);

            //var GetChampion = await _champion.GetChampion("Aatrox");

            var combinedDTO = new CombinedSummonerDTO
            (
                accountInfo,
                summonerInfo,
                summonerLeagueInfo,
                //GetChampion,
                topChampMastery
            );
            return combinedDTO;
        }

        public async Task<AccountDTO> GetAccountInfo(string summonerName, string tag)
        {
            var response = await _httpClient.GetAsync($"riot/account/v1/accounts/by-riot-id/{summonerName}/{tag}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AccountDTO>(jsonResponse);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<SummonerDTO> GetSummonerInfo(string puuid)
        {
            var version = await GetVersion();
            var url = $"https://br1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var summoner = JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);
                summoner.urlIcon = $"https://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{summoner.profileIconId}.png";
                return summoner;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<List<LeagueDTO>> GetSummonerLeagueInfo(string summonerId)
        {
            var url = $"https://br1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summonerId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<LeagueDTO>>(jsonResponse);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<List<ChampionMasteryDTO>> GetTopChampionMastery(string puuid, int count = 1)
        {
            var url = $"https://br1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top?count={count}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var championMastery = JsonSerializer.Deserialize<List<ChampionMasteryDTO>>(jsonResponse);

                var champions = await _champion.GetChampions();

                foreach (var mastery in championMastery)
                {
                    foreach (var champion in champions)
                    {
                        if (champion.Value.Key == mastery.championId.ToString())
                        {
                            mastery.championName = champion.Value.Name;
                            mastery.urlSplash = champion.Value.Image.UrlSplash;
                        }
                    }
                }

                return championMastery;

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<List<MatchResponse>> GetSummonerMatches(string puuid, int startTime, int endTime, int count, string typeQueue)
        {
            var url = $"https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start=0&count=3";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var matchs = JsonSerializer.Deserialize<List<string>>(jsonResponse).ToArray();
                var matchesInfo = await _match.GetMatcheInfo(matchs);
                return matchesInfo;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task<string> GetVersion()
        {
            var url = "https://ddragon.leagueoflegends.com/api/versions.json";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var versions = JsonSerializer.Deserialize<string[]>(jsonResponse);
                return versions.Length > 0 ? versions[0] : null;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}\n" +
                                   $"Request URI: {response.RequestMessage.RequestUri}\n" +
                                   $"Detalhes: {errorContent}";

                throw new HttpRequestException(errorMessage);
            }
        }
    }

}
