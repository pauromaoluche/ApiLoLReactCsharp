using System;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controlller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummonerController : ControllerBase
    {
        private readonly ISummoner _summoner;


        public SummonerController(ISummoner summoner)
        {
            // Instancia o serviço que consome a API do League of Legends.
            _summoner = summoner;
        }

        [HttpGet("{summonerName}/{tagLine}")]
        public async Task<IActionResult> GetSummoner(string summonerName, string tagLine)
        {
            try
            {
                var result = await _summoner.GetSummonerInformationAsync(summonerName, tagLine);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}