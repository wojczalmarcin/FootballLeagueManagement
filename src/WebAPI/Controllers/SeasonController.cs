using Application;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonController : FootballLeagueControllerBase
    {
        private readonly ISeasonService _seasonService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="seasonService">The season service</param>
        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        /// <summary>
        /// Gets season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data</returns>
        [HttpGet("{seasonId}")]
        public async Task<ActionResult<ResponseData<SeasonDto>>> GetSeasonByIdAsync(int seasonId)
        {
            try
            {
                var responseData = await _seasonService.GetSeasonByIdAsync(seasonId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<SeasonDto>>> GetAllSeasonsAsync()
        {
            try
            {
                var responseData = await _seasonService.GetAllSeasonsAsync();
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Puts season
        /// </summary>
        /// <param name="season">Season to put</param>
        /// <returns>Response data</returns>
        [HttpPut]
        public async Task<ActionResult<ResponseData<SeasonDto>>> PutAsync(SeasonDto season)
        {
            try
            {
                var responseData = await _seasonService.EditSeasonAsync(season);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Puts season
        /// </summary>
        /// <param name="season">Season to put</param>
        /// <returns>Response data</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseData<SeasonDto>>> PostAsync(CreateSeasonDto season)
        {
            try
            {
                var responseData = await _seasonService.CreateSeasonAsync(season);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
