using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Member
{
    public class MemberService : Service, IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        private readonly IMemberValidator _memberValidator;

        private readonly ITeamService _teamService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memberRepository">The member repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="memberValidator">The member validator</param>
        /// <param name="teamRepository">The team repository</param>
        public MemberService(IMemberRepository memberRepository, 
            IMapper mapper, 
            IMemberValidator memberValidator,
            ITeamService teamService) : base(mapper)
        {
            _memberRepository = memberRepository;
            _memberValidator = memberValidator;
            _teamService = teamService;
        }

        /// <summary>
        /// Gets member by id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with <see cref="SeasonDto"/></returns>
        public async Task<ResponseData<MemberDto>> GetMemberByIdAsync(int memberId)
            => await GetByIdAsync<MemberDto, Domain.Entities.Member>(memberId,
                _memberRepository.GetMemberByIdAsync,
                _memberValidator.ValidateMemberExistence);

        //public async Task<ResponseData<IEnumerable<MemberDto>>> GetMembersByRoleIdAsync(int roleId)
        //{

        //}

        //public async Task<ResponseData<IEnumerable<MemberDto>>> GetMembersByNameMatchAsync(string nameMatch)
        //{
        //
        //}

        /// <summary>
        /// Edits member
        /// </summary>
        /// <param name="member">The member</param>
        /// <returns>Response data with edited member</returns>
        public async Task<ResponseData<MemberDto>> EditMemberAsync(MemberEditDto memberEdit)
        {
            var member = _mapper.Map<MemberDto>(memberEdit);

            var teamResponseData = await _teamService.GetTeamByIdAsync(memberEdit.TeamId);

            if(teamResponseData.ResponseStatus != HttpStatusCode.OK)
            {
                return new ResponseData<MemberDto>() { 
                    ResponseStatus = teamResponseData.ResponseStatus, 
                    ValidationErrors = teamResponseData.ValidationErrors 
                };
            }

            return await EditAsync(member,
                _memberRepository.GetMemberByIdAsync,
                _memberRepository.EditMemberAsync,
                _memberValidator.ValidateMemberEdit);
        }

    }
}
