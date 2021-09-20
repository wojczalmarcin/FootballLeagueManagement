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
    /// <summary>
    /// The match score repository
    /// </summary>
    public class MatchRepository : IMatchRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public MatchRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets Match score by match Id
        /// </summary>
        /// <param name="matchId">match Id</param>
        /// <returns>Match score</returns>
        public async Task<Match> GetMatchByIdAsync(int matchId)
        => await _dbContext.Matches
                .AsNoTracking()
                .Include(m => m.Season)
                .Include(m => m.TeamHome)
                .Include(m => m.TeamAway)
                .Include(m => m.MatchScore)
                .FirstOrDefaultAsync(m => m.Id == matchId);
    }
}
