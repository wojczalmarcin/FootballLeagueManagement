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
        public async Task<ActionResult> GetMemberByIdAsync(int memberId)
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
        /// Gets members by match id
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Response data</returns>
        [HttpGet("match/{matchId}")]
        public async Task<ActionResult> GetPlayersByMatchId(int matchId)
        {
            try
            {
                var responseData = await _memberService.GetPlayersByMatchIdAsync(matchId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets members of given team on given page
        /// </summary>
        /// <param name="pageSize">The page size</param>
        /// <param name="pageNumber">The page number</param>
        /// <param name="teamId">The team id</param>
        /// <returns>Response data</returns>
        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult> GetMembersByTeamIdAndPage(int pageSize, int pageNumber, int teamId)
        {
            try
            {
                var responseData = await _memberService.GetMembersByTeamIdAndPageAsyc((pageSize, pageNumber), teamId);
                return HttpResponse(responseData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Gets number of members in team with given id
        /// </summary>
        /// <param name="teamId">The team Id</param>
        /// <returns>Response data</returns>
        [HttpGet("Count")]
        public async Task<ActionResult> GetNumberOfMembersByTeamIdAsync(int teamId)
        {
            try
            {
                var responseData = await _memberService.GetNumberOfMembersByTeamAsync(teamId);
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
        public async Task<ActionResult> PutAsync(MemberEditDto member)
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
