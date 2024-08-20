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

        /// <summary>
        /// Obtem varias informações do usuario
        /// </summary>
        /// <param name="summonerName">Nick Riot</param>
        /// <param name="tagLine">Tag</param>
        /// <returns>todas informações</returns>
        [HttpGet("info/{summonerName}/{tagLine}")]
        public async Task<IActionResult> GetSummonerInformation(string summonerName, string tagLine)
        {
            try
            {
                var result = await _summoner.GetSummonerInformation(summonerName, tagLine);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{summonerName}/{tagLine}")]
        public async Task<IActionResult> GetAccountInfo(string summonerName, string tagLine)
        {
            try
            {
                var result = await _summoner.GetAccountInfo(summonerName, tagLine);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("matche/{puuid}")]
        public async Task<IActionResult> GetSummonerMatches(string puuid, int startTime, int endTime, int count, string typeQueue = "")
        {
            try
            {
                var result = await _summoner.GetSummonerMatches(puuid, startTime, endTime, count, typeQueue);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}