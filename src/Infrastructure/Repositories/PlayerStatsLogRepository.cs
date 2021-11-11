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
    public class PlayerStatsLogRepository : IPlayerStatsLogRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public PlayerStatsLogRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets player stats log by id
        /// </summary>
        /// <param name="playerStatsLogId">The player stats log id</param>
        /// <returns>Player stats log</returns>
        public async Task<PlayerStatsLog> GetPlayerStatsLogByIdAsync(int playerStatsLogId)
            => await _dbContext.PlayersStatsLogs
                .AsNoTracking()
                .Include(p => p.Player)
                .FirstOrDefaultAsync(p => p.Id == playerStatsLogId);

        /// <summary>
        /// Gets player stats log by player id
        /// </summary>
        /// <param name="playerId">The player Id</param>
        /// <returns>Player stats log</returns>
        public async Task<IEnumerable<PlayerStatsLog>> GetPlayerStatsLogByPlayerIdAsync(int playerId)
            => await _dbContext.PlayersStatsLogs
                .AsNoTracking()
                .Include(p => p.Player)
                .Where(p => p.PlayerId == playerId)
                .ToListAsync();


        /// <summary>
        /// Gets players stats by match id
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Collection of player stats log</returns>
        public async Task<IEnumerable<PlayerStatsLog>> GetPlayersStatsLogByMatchIdAsync(int matchId)
            => await _dbContext.PlayersStatsLogs
                .AsNoTracking()
                .Include(p => p.Player)
                .Include(p => p.StatType)
                .Include(p => p.Team)
                .Where(p => p.MatchId == matchId)
                .ToListAsync();

        /// <summary>
        /// Adds new player stats
        /// </summary>
        /// <param name="playerStatsLog">The player stats log</param>
        /// <returns>Returns id of added player stats log. If fails return 0</returns>
        public async Task<int> AddPlayersStatsAsync(PlayerStatsLog playerStatsLog)
        {
            await _dbContext.PlayersStatsLogs.AddAsync(playerStatsLog);
            if (await _dbContext.SaveChangesAsync() > 0)
                return playerStatsLog.Id;
            return 0;
        }

        /// <summary>
        /// Delete player stats log
        /// </summary>
        /// <param name="playerStatsLogId">The player stats log id </param>
        /// <returns>Returns true if player stats log was deleted</returns>
        public async Task<bool> DeletePlayerStatsLogAsync(int playerStatsLogId)
        {
            var playerStatsLog = await _dbContext.PlayersStatsLogs.FindAsync(playerStatsLogId);
            if (playerStatsLog == null)
                return false;

            _dbContext.PlayersStatsLogs.Remove(playerStatsLog);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
