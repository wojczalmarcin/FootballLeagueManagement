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
    public class TeamController : FootballLeagueControllerBase
    {
        private readonly ITeamService _teamService;

        /// <summary>
        /// The costructor
        /// </summary>
        /// <param name="teamService">The team serivce</param>
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Gets team by id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>Response data</returns>
        [HttpGet("{teamId}")]
        public async Task<ActionResult<ResponseData<TeamDto>>> Get (int teamId)
        {
            try
            {
                var responseData = await _teamService.GetTeamByIdAsync(teamId);
                return HttpResponse(responseData);
            }
            catch(Exception e)
            {
                return this.Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets teams by season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data</returns>
        [HttpGet("Season/{seasonId}")]
        public async Task<ActionResult<ResponseData<IEnumerable<TeamDto>>>> GetBySeason(int seasonId)
        {
            try
            {
                var responseData = await _teamService.GetTeamsBySeasonIdAsync(seasonId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return this.Problem(e.Message);
            }
        }
    }
}
