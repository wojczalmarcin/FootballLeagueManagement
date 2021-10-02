using Application;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : FootballLeagueControllerBase
    {
        private readonly IMatchService _matchService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="matchService">The match service</param>
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
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
                switch (responseData.ResponseStatus)
                {
                    case HttpStatusCode.BadRequest:
                        return BadRequest(responseData);
                    case HttpStatusCode.NotFound:
                        return NotFound(responseData);
                    case HttpStatusCode.Forbidden:
                        return Forbid(responseData.ValidationErrors.ToString());
                    default:
                        return Ok(responseData);
                }
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
        /// <returns>Response data</returns>
        [HttpGet("{seasonId}")]
        public async Task<ActionResult<ResponseData<IEnumerable<MatchDto>>>> GetMatchesBySeasonId(int seasonId)
        {
            try
            {
                var responseData = await _matchService.GetMatchesDataBySeasonId(seasonId);
                switch (responseData.ResponseStatus)
                {
                    case HttpStatusCode.BadRequest:
                        return BadRequest(responseData);
                    case HttpStatusCode.NotFound:
                        return NotFound(responseData);
                    case HttpStatusCode.Forbidden:
                        return Forbid(responseData.ValidationErrors.ToString());
                    default:
                        return Ok(responseData);
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
