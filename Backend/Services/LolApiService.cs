using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Service
{
    public class LolApiService
    {
        private readonly HttpClient _httpClient;

        public LolApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://americas.api.riotgames.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-02aebba2-b150-4a92-b201-3fb8f755441c");
        }

        public async Task<CombinedSummonerDTO> GetSummonerInformationAsync(string summonerName, string tag)
        {
            var accountInfo = await GetAccountInfo(summonerName, tag);

            var puuid = accountInfo.puuid;

            var summonerInfo = await GetSummonerInfo(puuid);

            var combinedDTO = new CombinedSummonerDTO
            (
                accountInfo,
                summonerInfo
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
    }
}
