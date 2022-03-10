using Application.DTO.Create;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerSuspensionController : FootballLeagueControllerBase
    {
        private readonly IPlayerSuspensionService _playerSuspensionService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="playerSuspensionService">The player suspension service</param>
        public PlayerSuspensionController(IPlayerSuspensionService playerSuspensionService)
        {
            _playerSuspensionService = playerSuspensionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPlayersSuspensionById(int suspensionId)
        {
            try
            {
                var responseData = await _playerSuspensionService.GetPlayerSusnepsionByIdAsync(suspensionId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("Player")]
        public async Task<ActionResult> GetPlayersSuspensionByPlayerId(int playerId)
        {
            try
            {
                var responseData = await _playerSuspensionService.GetPlayerSusnepsionByPlayerIdAsync(playerId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        
        [HttpPost]
        public async Task<ActionResult> AddPlayerSuspension(CreatePlayerSuspensionDto playerSuspension)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePlayerSuspension(int playerId)
        {
            return Ok();
        }
    }
}
