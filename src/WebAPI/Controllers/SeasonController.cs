using Application;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonController : ControllerBase
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
        public async Task<ActionResult<ResponseData<SeasonDto>>> GetSeasonById(int seasonId)
        {
            try
            {
                var responseData = await _seasonService.GetSeasonById(seasonId);
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
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<SeasonDto>>> GetAllSeasons()
        {
            try
            {
                var responseData = await _seasonService.GetAllSeasons();
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
