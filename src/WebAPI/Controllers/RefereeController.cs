using Application.DTO.Create;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace ReactWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefereeController : FootballLeagueControllerBase
    {
        
        [HttpGet("{refereeId}")]
        public async Task<ActionResult> GetRefereenById(int refereeId)
        {
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAllRefeeres()
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult> AddPlayerSuspension(CreateRefereeDto playerSuspension)
        {
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeletePlayerSuspension(int playerId)
        {
            return Ok();
        }
    }
}
