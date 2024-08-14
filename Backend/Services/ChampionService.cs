using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public class ChampionService : IChampion
    {
        private readonly HttpClient _httpClient;

        public ChampionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public async Task<Dictionary<string, ChampionDTO>> GetChampions()
        {
            var version = await GetVersion();
            var url = $"https://ddragon.leagueoflegends.com/cdn/{version}/data/pt_BR/champion.json";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Debug: Exibir o JSON recebido
                System.Diagnostics.Debug.WriteLine(jsonResponse);

                // Desserializar o JSON diretamente para ChampionApiResponse
                var championApiResponse = JsonSerializer.Deserialize<ChampionApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                // Verificar se 'Data' não é nulo e retorná-lo
                if (championApiResponse?.Data != null)
                {
                    return championApiResponse.Data;
                }
                else
                {
                    throw new InvalidOperationException("Os dados dos campeões não foram encontrados na resposta.");
                }
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
