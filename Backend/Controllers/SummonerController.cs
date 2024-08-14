using System;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controlller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummonerController : ControllerBase
    {
        private readonly LolApiService _lolApiService;

        public SummonerController(LolApiService lolApiService)
        {
            // Instancia o serviço que consome a API do League of Legends.
            _lolApiService = lolApiService;
        }

        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetSummoner(string summonerName, string tagLine)
        {
            try
            {
                var result = await _lolApiService.GetSummonerInformationAsync(summonerName, tagLine);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}