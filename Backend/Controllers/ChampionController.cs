using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChampionController : ControllerBase
    {
        private readonly IChampion _champion;

        public ChampionController(IChampion champion)
        {
            _champion = champion;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetChampions()
        {
            var champions = await _champion.GetChampions();
            return Ok(champions);
        }

        [HttpGet("{championName}")]
        public async Task<IActionResult> GetChampion(string championName)
        {
            try
            {
                var result = await _champion.GetChampion(championName);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
