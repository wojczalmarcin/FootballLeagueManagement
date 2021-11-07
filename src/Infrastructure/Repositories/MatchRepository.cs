using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Gets matches from given season
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Collection of matches</returns>
        public async Task<IEnumerable<Match>> GetMatchesBySeasonIdAsync(int seasonId)
        => await _dbContext.Matches
                .AsNoTracking()
                .Include(m => m.Season)
                .Include(m => m.TeamHome)
                .Include(m => m.TeamAway)
                .Include(m => m.MatchScore)
                .Where(m => m.SeasonId == seasonId).ToListAsync();


        /// <summary>
        /// Gets matches witch given team ids
        /// </summary>
        /// <param name="teamIds">List of team ids</param>
        /// <returns>Collection of matches</returns>
        public async Task<IEnumerable<Match>> GetMatchesByTeamIdsAsync(List<int> teamIds)
        => await _dbContext.Matches
                .AsNoTracking()
                .Include(m => m.MatchScore)
                .Where(m => teamIds.Contains(m.TeamHomeId)).ToListAsync();

        /// <summary>
        /// Adds new match
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Returns id of added season. If fails return 0</returns>
        public async Task<int> AddMatchAsync(Match match)
        {
            await _dbContext.Matches.AddAsync(match);
            if (await _dbContext.SaveChangesAsync() > 0)
                return match.Id;
            return 0;
        }

        /// <summary>
        /// Delete match
        /// </summary>
        /// <param name="matchId">The match id </param>
        /// <returns>Returns true if match was deleted</returns>
        public async Task<bool> DeleteMatchAsync(int matchId)
        {
            var match = await _dbContext.Matches.FindAsync(matchId);
            if (match == null)
                return false;

            _dbContext.Matches.Remove(match);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Edits match
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Returns true if match edited</returns>
        public async Task<bool> EditMatchAsync(Match match)
        {
            _dbContext.Entry(match).State = EntityState.Modified;
            _dbContext.Entry(match).Reference(x => x.MatchScore).IsModified = false;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
