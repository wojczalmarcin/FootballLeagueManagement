using Application.DTO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// The member service interface
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Gets member by id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with <see cref="SeasonDto"/></returns>
        Task<ResponseData<MemberDto>> GetMemberByIdAsync(int memberId);

        /// <summary>
        /// Edits member
        /// </summary>
        /// <param name="memberEdit">The member to edit</param>
        /// <returns>Response data with edited member</returns>
        Task<ResponseData<MemberDto>> EditMemberAsync(MemberEditDto memberEdit);

        /// <summary>
        /// Gets players by match Id
        /// </summary>
        /// <param name="matchId">The match Id</param>
        /// <returns>The response data</returns>
        Task<ResponseData<IEnumerable<MemberDto>>> GetPlayersByMatchIdAsync(int matchId);

        /// <summary>
        /// Gets number of members
        /// </summary>
        /// <returns>Response data with number of members</returns>
        Task<ResponseData<int?>> GetNumberOfMembersAsync();

        /// <summary>
        /// Gets number of members of given team
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <returns>Response data with number of members</returns>
        Task<ResponseData<int?>> GetNumberOfMembersByTeamAsync(int teamId);

        /// <summary>
        /// Gets members by team id and page
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="teamId">The team id</param>
        /// <returns>Response data with the collection of the members</returns>
        Task<ResponseData<IEnumerable<MemberDto>>> GetMembersByTeamIdAndPageAsyc((int size, int number) page, int teamId);
    }
}
