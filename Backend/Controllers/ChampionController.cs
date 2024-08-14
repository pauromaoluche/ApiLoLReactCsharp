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
        private readonly IChampion _championService;

        public ChampionController(IChampion championService)
        {
            _championService = championService;
        }

        [HttpGet]
        [Route("champions")]
        public async Task<IActionResult> GetChampions()
        {
            var champions = await _championService.GetChampions();
            return Ok(champions);
        }
    }
}
