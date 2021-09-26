﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// The team repository
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public TeamRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets team by it's Id
        /// </summary>
        /// <param name="teamId">team Id</param>
        /// <returns>Teams</returns>
        public async Task<Team> GetTeamByIdAsync(int teamId)
            => await this._dbContext.Teams
            .AsNoTracking()
            .Include(t => t.Stadium)
            .Include(t => t.Address)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        /// <summary>
        /// Gets teams by season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Collection of teams</returns>
        public async Task<IEnumerable<Team>> GetTeamsBySeasonIdAsync(int seasonId)
            => await this._dbContext.Teams
            .AsNoTracking()
            .Include(t=>t.Stadium)
            .Include(t => t.Address)
            .Include(t => t.SeasonTeams).Where(t=>t.SeasonTeams.Any(s=>s.SeasonId==seasonId)).ToListAsync();
    }
}
