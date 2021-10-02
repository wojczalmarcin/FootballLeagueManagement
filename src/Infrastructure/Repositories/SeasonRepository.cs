using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public SeasonRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        /// <summary>
        /// Gets season by id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns>Season</returns>
        public async Task<Season> GetSeasonByIdAsync(int seasonId)
            => await _dbContext.Seasons.AsNoTracking().FirstOrDefaultAsync(s => s.Id == seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Collection of seasons</returns>
        public async Task<IEnumerable<Season>> GetAllSeasonsAsync()
            => await _dbContext.Seasons.AsNoTracking().ToListAsync();

        /// <summary>
        /// Adds new season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Returns id of added season. If fails return 0</returns>
        public async Task<int> AddSeasonAsync(Season season)
        {
            await _dbContext.Seasons.AddAsync(season);
            if (await _dbContext.SaveChangesAsync() > 0)
                return season.Id;
            return 0;
        }

        /// <summary>
        /// Edits season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Returns true if season edited</returns>
        public async Task<bool> EditSeasonAsync(Season season)
        {
            _dbContext.Seasons.Update(season);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
