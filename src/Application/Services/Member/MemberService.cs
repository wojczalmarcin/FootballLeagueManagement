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

        private readonly IMemberRoleRepository _memberRoleRepository;

        private readonly IMemberRoleValidator _memberRoleValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memberRepository">The member repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="memberValidator">The member validator</param>
        /// <param name="memberRoleRepository">The member role repository</param>
        public MemberService(IMemberRepository memberRepository, 
            IMapper mapper, 
            IMemberValidator memberValidator,
            ITeamService teamService,
            IMemberRoleRepository memberRoleRepository,
            IMemberRoleValidator memberRoleValidator) : base(mapper)
        {
            _memberRepository = memberRepository;
            _memberValidator = memberValidator;
            _teamService = teamService;
            _memberRoleRepository = memberRoleRepository;
            _memberRoleValidator = memberRoleValidator;
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

        /// <summary>
        /// Gets players by match Id
        /// </summary>
        /// <param name="matchId">The match Id</param>
        /// <returns>The response data</returns>
        public async Task<ResponseData<IEnumerable<MemberDto>>> GetPlayersByMatchIdAsync(int matchId)
        {
            var responseData = new ResponseData<IEnumerable<MemberDto>>();

            var playerRole = _mapper.Map<MemberRoleDto>(await _memberRoleRepository.GetPlayerRoleAsync());
            var playerRoleValidation = _memberRoleValidator.ValidateMemberRoleExistence(playerRole);

            responseData.ResponseStatus = playerRoleValidation.statusCode;
            responseData.ValidationErrors = playerRoleValidation.validationErrors;

            if (playerRoleValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var players = _mapper.Map<IEnumerable<MemberDto>>(await _memberRepository.GetMembersByMatchIdAndRoleIdAsync(matchId, playerRole.Id));
            var membersValidation = _memberValidator.ValidateMembersExistence(players);

            if (membersValidation.statusCode != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = membersValidation.statusCode;
                responseData.ValidationErrors = membersValidation.validationErrors;
                return responseData;
            }

            responseData.Data = players;

            return responseData;
        }

        //public async Task<ResponseData<IEnumerable<MemberDto>>> GetMembersByRoleIdAsync(int roleId)
        //{

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
