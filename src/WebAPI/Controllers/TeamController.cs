using Application;
using Application.DTO;
using Application.DTO.Create;
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
        public async Task<ActionResult> Get (int teamId)
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
        /// Gets all teams
        /// </summary>
        /// <returns>Response data</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var responseData = await _teamService.GetAllTeams();
                return HttpResponse(responseData);
            }
            catch (Exception e)
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
        public async Task<ActionResult> GetBySeason(int seasonId)
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

        /// <summary>
        /// Puts team
        /// </summary>
        /// <param name="team">Team to put</param>
        /// <returns>Response data</returns>
        [HttpPut]
        public async Task<ActionResult> PutAsync(TeamDto team)
        {
            try
            {
                var responseData = await _teamService.EditTeamAsync(team);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Puts team
        /// </summary>
        /// <param name="team">Team to post</param>
        /// <returns>Response data</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateTeamDto team)
        {
            try
            {
                var responseData = await _teamService.CreateTeamAsync(team);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
