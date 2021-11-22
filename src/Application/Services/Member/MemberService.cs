using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
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

        private readonly IPageValidator _pageValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memberRepository">The member repository</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="memberValidator">The member validator</param>
        /// <param name="memberRoleRepository">The member role repository</param>
        /// <param name="pageValidator">The page validator</param>
        public MemberService(IMemberRepository memberRepository, 
            IMapper mapper, 
            IMemberValidator memberValidator,
            ITeamService teamService,
            IMemberRoleRepository memberRoleRepository,
            IMemberRoleValidator memberRoleValidator,
            IPageValidator pageValidator)
                : base(mapper)
        {
            _memberRepository = memberRepository;
            _memberValidator = memberValidator;
            _teamService = teamService;
            _memberRoleRepository = memberRoleRepository;
            _memberRoleValidator = memberRoleValidator;
            _pageValidator = pageValidator;
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

        /// <summary>
        /// Gets number of members
        /// </summary>
        /// <returns>Response data with number of members</returns>
        public async Task<ResponseData<int?>> GetNumberOfMembersAsync()
        {
            var responseData = new ResponseData<int?>();

            var numberOfMembers = await _memberRepository.CountMembersAsync();

            if(numberOfMembers < 1)
            {
                responseData.ResponseStatus = HttpStatusCode.NotFound;
                responseData.ValidationErrors.Add("No members have been found");
                return responseData;
            }

            responseData.ResponseStatus = HttpStatusCode.OK;
            responseData.Data = numberOfMembers;
            return responseData;
        }

        /// <summary>
        /// Gets number of members of given team
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <returns>Response data with number of members</returns>
        public async Task<ResponseData<int?>> GetNumberOfMembersByTeamAsync(int teamId)
        {
            var responseData = new ResponseData<int?>();
            var teamResponseData = await _teamService.GetTeamByIdAsync(teamId);
            
            if(teamResponseData.ResponseStatus != HttpStatusCode.OK)
            {
                responseData.ValidationErrors = teamResponseData.ValidationErrors;
                responseData.ResponseStatus = teamResponseData.ResponseStatus;
                return responseData;
            }

            var numberOfMembers = await _memberRepository.CountMembersByTeamIdAsync(teamId);

            if (numberOfMembers < 1)
            {
                responseData.ValidationErrors = new List<string>();
                responseData.ResponseStatus = HttpStatusCode.NotFound;
                responseData.ValidationErrors.Add("No members have been found");
                return responseData;
            }

            responseData.ResponseStatus = HttpStatusCode.OK;
            responseData.Data = numberOfMembers;
            return responseData;
        }

        /// <summary>
        /// Gets members by team id and page
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="teamId">The team id</param>
        /// <returns>Response data with the collection of the members</returns>
        public async Task<ResponseData<IEnumerable<MemberDto>>> GetMembersByTeamIdAndPageAsyc((int size, int number) page, int teamId)
        {
            var responseData = new ResponseData<IEnumerable<MemberDto>>();
           
            var numberOfMembersResponse = await this.GetNumberOfMembersByTeamAsync(teamId);

            if (numberOfMembersResponse.ResponseStatus != HttpStatusCode.OK)
            {
                responseData.ValidationErrors = numberOfMembersResponse.ValidationErrors;
                responseData.ResponseStatus = numberOfMembersResponse.ResponseStatus;
                return responseData;
            }

            var pageValidation = _pageValidator.ValidatePageExistence(page, numberOfMembersResponse.Data.Value);

            if(pageValidation.statusCode != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = pageValidation.statusCode;
                responseData.ValidationErrors = pageValidation.validationErrors;
                return responseData;
            }

            var members = _mapper.Map<IEnumerable<MemberDto>>(await _memberRepository.GetMembersByTeamIdAsync(page, teamId));

            var membersValidation = _memberValidator.ValidateMembersExistence(members);

            if (membersValidation.statusCode != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = membersValidation.statusCode;
                responseData.ValidationErrors = membersValidation.validationErrors;
                return responseData;
            }

            responseData.Data = members;
            responseData.ResponseStatus = HttpStatusCode.OK;

            return responseData;
        }

        /// <summary>
        /// Edits member
        /// </summary>
        /// <param name="memberEdit">The member to edit</param>
        /// <returns>Response data with edited member</returns>
        public async Task<ResponseData<MemberDto>> EditMemberAsync(MemberEditDto memberEdit)
        {
            var responseData = new ResponseData<MemberDto>()
            {
                    ResponseStatus = HttpStatusCode.OK,
                    ValidationErrors = new List<string>()
            };

            var member = _mapper.Map<MemberDto>(memberEdit);

            var teamResponseData = await _teamService.GetTeamByIdAsync(memberEdit.TeamId);

            if(teamResponseData.ResponseStatus != HttpStatusCode.OK)
            {
                responseData.ResponseStatus = teamResponseData.ResponseStatus;
                responseData.ValidationErrors = teamResponseData.ValidationErrors;
                return responseData;
            }

            var memberToEdit = _mapper.Map<MemberDto>(await _memberRepository.GetMemberByIdAsync(memberEdit.Id));

            var editingValidation = _memberValidator.ValidateMemberEdit(memberEdit, memberToEdit);

            
            memberToEdit.FirstName = memberEdit.FirstName;
            memberToEdit.LastName = memberEdit.LastName;
            if (memberEdit.TeamId != memberToEdit.Team.Id)
            {
                teamResponseData = await _teamService.GetTeamByIdAsync(memberEdit.TeamId);
                if (teamResponseData.ResponseStatus != HttpStatusCode.OK)
                {
                    responseData.ResponseStatus = teamResponseData.ResponseStatus;
                    responseData.ValidationErrors = teamResponseData.ValidationErrors;
                    return responseData;
                }
                memberToEdit.Team = teamResponseData.Data;
            }

            return await EditAsync(memberToEdit, _memberRepository.GetMemberByIdAsync, _memberRepository.EditMemberAsync, responseData, editingValidation);
        }

    }
}
