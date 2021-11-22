using Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class FootballLeagueControllerBase : ControllerBase
    {
        public ActionResult HttpResponse<T> (ResponseData<T> responseData)
        {
            switch (responseData.ResponseStatus)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(responseData.ValidationErrors);
                case HttpStatusCode.NotFound:
                    return NotFound(responseData.ValidationErrors);
                case HttpStatusCode.InternalServerError:
                    return Problem(responseData.ValidationErrors.ToString());
                case HttpStatusCode.Forbidden:
                    return Forbid(responseData.ValidationErrors.ToString());
                default:
                    return Ok(responseData.Data);
            }
        }
    }
}
