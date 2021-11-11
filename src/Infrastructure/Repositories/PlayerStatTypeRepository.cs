using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PlayerStatTypeRepository : IPlayerStatTypeRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public PlayerStatTypeRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets player stat type by id
        /// </summary>
        /// <param name="statTypeId">The player stat type Id</param>
        /// <returns>The player stat type</returns>
        public async Task<PlayerStatType> GetPlayerStatTypeByIdAsync(int statTypeId)
            => await _dbContext.PlayerStatTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == statTypeId);

        /// <summary>
        /// Gets all player stat types
        /// </summary>
        /// <returns>The collection of player stat types</returns>
        public async Task<IEnumerable<PlayerStatType>> GetAllPlayerStatTypesAsync()
            => await _dbContext.PlayerStatTypes
                    .AsNoTracking()
                    .ToListAsync();
        
    }
}
