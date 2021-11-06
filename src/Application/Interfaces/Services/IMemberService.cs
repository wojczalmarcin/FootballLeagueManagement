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
        /// <param name="member">The member</param>
        /// <returns>Response data with edited member</returns>
        Task<ResponseData<MemberDto>> EditMemberAsync(MemberEditDto memberEdit);
    }
}
