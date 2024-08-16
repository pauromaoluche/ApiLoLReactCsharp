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

        public SummonerService(HttpClient httpClient, IChampion champion)
        {
            _httpClient = httpClient;
            _champion = champion;
            _httpClient.BaseAddress = new Uri("https://americas.api.riotgames.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://developer.riotgames.com");
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-9ac4f4e8-7d7b-4b28-ba90-38f4e110a277");
        }

        public async Task<CombinedSummonerDTO> GetSummonerInformationAsync(string summonerName, string tag)
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
            var url = $"https://br1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SummonerDTO>(jsonResponse);
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

        public async Task<List<ChampionMasteryDTO>> GetTopChampionMastery(string puuid)
        {
            var url = $"https://br1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top";

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
                        if(champion.Value.Key == mastery.championId.ToString()){
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
    }

}
