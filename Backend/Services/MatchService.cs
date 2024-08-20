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
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-81fc9c19-f4ab-4f06-a811-d9e46e1a5541");
        }

        public async Task<List<MatchResponse>> GetMatcheInfo(string[] matchIds)
        {
            var result = new List<MatchResponse>();
            foreach (var matchId in matchIds)
            {
                var response = await _httpClient.GetAsync($"/lol/match/v5/matches/{matchId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var matchData = JsonSerializer.Deserialize<MatchResponse>(jsonResponse);
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

    }
}