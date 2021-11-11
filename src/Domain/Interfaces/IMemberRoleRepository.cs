using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Member role repository interface
    /// </summary>
    public interface IMemberRoleRepository
    {
        /// <summary>
        /// Gets member role by id
        /// </summary>
        /// <param name="memberRoleId">The member role id</param>
        /// <returns>Member role</returns>
        Task<MemberRole> GetMemberRoleByIdAsync(int memberRoleId);


        /// <summary>
        /// Gets player role
        /// </summary>
        /// <returns>Member role</returns>
        Task<MemberRole> GetPlayerRoleAsync();
    }
}
