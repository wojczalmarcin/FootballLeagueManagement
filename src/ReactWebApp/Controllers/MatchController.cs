﻿using Application;
using Application.DTO;
using Application.DTO.Edit;
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
        public async Task<ActionResult> GetTable(int seasonId)
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
        public async Task<ActionResult> GetMatch(int matchId)
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
        public async Task<ActionResult> GetMatchesBySeasonId(int seasonId)
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
        /// Creates new match
        /// </summary>
        /// <param name="match">The match to create</param>
        /// <returns>Response data</returns>
        [HttpPost]
        public async Task<ActionResult> CreateMatch(CreateMatchDto match)
        {
            try
            {
                var responseData = await _matchService.CreateMatchAsync(match);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Deletes match by id
        /// </summary>
        /// <param name="matchId">The match Id</param>
        /// <returns>Response data</returns>
        [HttpDelete("Delete/{matchId}")]
        public async Task<ActionResult> DeleteMatch(int matchId)
        {
            try
            {
                var responseData = await _matchService.DeleteMatchAsync(matchId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Edits match
        /// </summary>
        /// <param name="match">The match to create</param>
        /// <returns>Response data</returns>
        [HttpPut]
        public async Task<ActionResult> EditMatch(EditMatchDto match)
        {
            try
            {
                var responseData = await _matchService.EditMatchAsync(match);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Sets match to finish
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Response data</returns>
        [HttpPut("Finish")]
        public async Task<ActionResult> FinishMatch(int matchId)
        {
            try
            {
                var responseData = await _matchService.FinishTheMatch(matchId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
