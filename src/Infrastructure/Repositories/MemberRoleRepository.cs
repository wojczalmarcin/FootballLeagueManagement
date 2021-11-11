using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Member role repository
    /// </summary>
    public class MemberRoleRepository: IMemberRoleRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public MemberRoleRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        /// <summary>
        /// Gets member role by id
        /// </summary>
        /// <param name="memberRoleId">The member role id</param>
        /// <returns>Member role</returns>
        public async Task<MemberRole> GetMemberRoleByIdAsync(int memberRoleId)
            => await _dbContext.MembersRoles.AsNoTracking().FirstOrDefaultAsync(s => s.Id == memberRoleId);

        /// <summary>
        /// Gets player role
        /// </summary>
        /// <returns>Member role</returns>
        public async Task<MemberRole> GetPlayerRoleAsync()
            => await _dbContext.MembersRoles.AsNoTracking().FirstOrDefaultAsync(s => s.IsPlayer == true);
    }
}
