﻿using Application;
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
    public class MatchController : FootballLeagueControllerBase
    {
        private readonly IMatchService _matchService;

        private readonly IPlayerStatsService _playerStatsService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="matchService">The match service</param>
        public MatchController(IMatchService matchService, IPlayerStatsService playerStatsService)
        {
            _matchService = matchService;
            _playerStatsService = playerStatsService;
        }

        /// <summary>
        /// Gets season table
        /// </summary>
        /// <param name="seasonId">season Id</param>
        /// <returns>Response data</returns>
        [HttpGet("Table/{seasonId}")]
        public async Task<ActionResult<ResponseData<IEnumerable<TeamStatisticsDto>>>> GetTable(int seasonId)
        {
            try
            {
                var responseData =  await _matchService.GetSeasonTable(seasonId);
                return HttpResponse(responseData);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets match data by id
        /// </summary>
        /// <param name="matchId">match Id</param>
        /// <returns>Response data</returns>
        [HttpGet("{matchId}")]
        public async Task<ActionResult<ResponseData<MatchDto>>> GetMatch(int matchId)
        {
            try
            {
                var responseData = await _matchService.GetMatchDataById(matchId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets matches by season
        /// </summary>
        /// <param name="seasonId">season Id</param>
        /// <returns>Response data with matches</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<IEnumerable<MatchDto>>>> GetMatchesBySeasonId(int seasonId)
        {
            try
            {
                var responseData = await _matchService.GetMatchesDataBySeasonId(seasonId);
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
        /// <param name="matchId">The match id</param>
        /// <returns>Response data with stats</returns>
        [HttpGet("Stats/{matchId}")]
        public async Task<ActionResult<ResponseData<IEnumerable<PlayerStatsDto>>>> GetPlayersStatsByMatchId(int matchId)
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
    }
}