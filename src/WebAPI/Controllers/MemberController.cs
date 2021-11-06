using Application;
using Application.DTO;
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
    public class MemberController : FootballLeagueControllerBase
    {
        private readonly IMemberService _memberService;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="memberService">The member service</param>
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// Gets member by id
        /// </summary>
        /// <param name="memberId">The member id</param>
        /// <returns>Response data</returns>
        [HttpGet("{memberId}")]
        public async Task<ActionResult<ResponseData<MemberDto>>> GetSeasonByIdAsync(int memberId)
        {
            try
            {
                var responseData = await _memberService.GetMemberByIdAsync(memberId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Puts member
        /// </summary>
        /// <param name="member">Member to put</param>
        /// <returns>Response data</returns>
        [HttpPut]
        public async Task<ActionResult<ResponseData<MemberDto>>> PutAsync(MemberEditDto member)
        {
            try
            {
                var responseData = await _memberService.EditMemberAsync(member);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
