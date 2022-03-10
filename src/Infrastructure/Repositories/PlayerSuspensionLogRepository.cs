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
    /// The player suspension log repository
    /// </summary>
    public class PlayerSuspensionLogRepository : IPlayerSuspensionLogRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public PlayerSuspensionLogRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets player suspension log by id
        /// </summary>
        /// <param name="playerSuspensionLogId">The player suspension log id</param>
        /// <returns>Player suspension log</returns>
        public async Task<PlayerSuspensionLog> GetPlayerSuspensionLogByIdAsync(int playerSuspensionLogId)
            => await _dbContext.PlayerSuspensionLogs
                .AsNoTracking()
                .Include(p => p.Player)
                .FirstOrDefaultAsync(p => p.Id == playerSuspensionLogId);

        /// <summary>
        /// Gets player suspension log by player id
        /// </summary>
        /// <param name="playerId">The player Id</param>
        /// <returns>Player suspension log</returns>
        public async Task<IEnumerable<PlayerSuspensionLog>> GetPlayerSuspensionLogByPlayerIdAsync(int playerId)
            => await _dbContext.PlayerSuspensionLogs
                .AsNoTracking()
                .Include(p => p.Player)
                .Where(p => p.PlayerId == playerId)
                .ToListAsync();

        /// <summary>
        /// Adds new player suspension
        /// </summary>
        /// <param name="playerSuspensionLog">The player suspension log</param>
        /// <returns>Returns id of added player suspension log. If fails return 0</returns>
        public async Task<int> AddPlayersStatsAsync(PlayerSuspensionLog playerSuspensionLog)
        {
            await _dbContext.PlayerSuspensionLogs.AddAsync(playerSuspensionLog);
            if (await _dbContext.SaveChangesAsync() > 0)
                return playerSuspensionLog.Id;
            return 0;
        }

        /// <summary>
        /// Delete player suspension log
        /// </summary>
        /// <param name="playerSuspensionLogId">The player suspension log id </param>
        /// <returns>Returns true if player stats log was deleted</returns>
        public async Task<bool> DeletePlayerStatsLogAsync(int playerSuspensionLogId)
        {
            var playerSuspensionLog = await _dbContext.PlayerSuspensionLogs.FindAsync(playerSuspensionLogId);
            if (playerSuspensionLog == null)
                return false;

            _dbContext.PlayerSuspensionLogs.Remove(playerSuspensionLog);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
