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
        public async Task<Season> GetSeasonById(int seasonId)
            => await _dbContext.Seasons.AsNoTracking().FirstOrDefaultAsync(s => s.Id == seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Collection of seasons</returns>
        public async Task<IEnumerable<Season>> GetAllSeasons()
            => await _dbContext.Seasons.AsNoTracking().ToListAsync();
    }
}
