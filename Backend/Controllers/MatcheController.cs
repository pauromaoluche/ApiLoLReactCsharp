using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatcheController : ControllerBase
    {

        private readonly IMatch _match;

        public MatcheController(IMatch match)
        {
            // Instancia o serviço que consome a API do League of Legends.
            _match = match;
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetMatcheInfo([FromQuery] string[] matchIds)
        {
            try
            {
                var result = await _match.GetMatcheInfo(matchIds);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}