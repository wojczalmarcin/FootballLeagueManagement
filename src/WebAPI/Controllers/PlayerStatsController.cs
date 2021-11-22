using Application;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerStatsController : FootballLeagueControllerBase
    {
        private readonly IPlayerStatsService _playerStatsService;

        private readonly IPlayerStatTypeService _playerStatTypeService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="playerStatsService">The player stats service</param>
        /// <param name="playerStatTypeService">The player stat types service</param>
        public PlayerStatsController(IPlayerStatsService playerStatsService, IPlayerStatTypeService playerStatTypeService)
        {
            _playerStatsService = playerStatsService;
            _playerStatTypeService = playerStatTypeService;
        }

        /// <summary>
        /// Gets players stats by match id
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Response data with stats</returns>
        [HttpGet]
        public async Task<ActionResult> GetPlayersStatsByMatchId(int matchId)
        {
            try
            {
                var responseData = await _playerStatsService.GetPlayersStatsByMatchId(matchId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets players stats by match id
        /// </summary>
        /// <param name="playerStats">The player stats to create</param>
        /// <returns>Response data with player stats</returns>
        [HttpPost]
        public async Task<ActionResult> CreatePlayerStats(CreatePlayerStatsDto playerStats)
        {
            try
            {
                var responseData = await _playerStatsService.CreatePlayerStats(playerStats);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Deletes players stat by id
        /// </summary>
        /// <param name="playerStatId">The player stat id</param>
        /// <returns>Response data with deleted player stats</returns>
        [HttpDelete("{playerStatId}")]
        public async Task<ActionResult> DeletePlayerStat(int playerStatId)
        {
            try
            {
                var responseData = await _playerStatsService.DeletePlayerStats(playerStatId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("Type")]
        public async Task<ActionResult> GetAllPlayerStatTypes()
        {
            try
            {
                var responseData = await _playerStatTypeService.GetAllPlayerStatTypesAsync();
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
