using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public class MatchService : IMatch
    {
        private readonly HttpClient _httpClient;

        public MatchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://americas.api.riotgames.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://developer.riotgames.com");
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-d041aa16-7b3f-431c-b91d-c9ccc1cb178c");
        }

        public async Task<List<MatchResponse>> GetMatcheInfo(string[] matchIds)
        {
            var version = await GetVersion();
            var result = new List<MatchResponse>();
            foreach (var matchId in matchIds)
            {
                var response = await _httpClient.GetAsync($"/lol/match/v5/matches/{matchId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var matchData = JsonSerializer.Deserialize<MatchResponse>(jsonResponse);
                    foreach (var participant in matchData.info.participants)
                    {
                        participant.urlChampIcon = $"https://ddragon.leagueoflegends.com/cdn/{version}/img/champion/{participant.championName}.png";;
                    }
                    result.Add(matchData);
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
            return result;
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