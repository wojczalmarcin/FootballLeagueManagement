using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// The player suspension log repository interface
    /// </summary>
    public interface IPlayerSuspensionLogRepository
    {
        /// <summary>
        /// Gets player suspension log by id
        /// </summary>
        /// <param name="playerSuspensionLogId">The player suspension log id</param>
        /// <returns>Player suspension log</returns>
        Task<PlayerSuspensionLog> GetPlayerSuspensionLogByIdAsync(int playerSuspensionLogId);

        /// <summary>
        /// Gets player suspension log by player id
        /// </summary>
        /// <param name="playerId">The player Id</param>
        /// <returns>Player suspension log</returns>
        Task<IEnumerable<PlayerSuspensionLog>> GetPlayerSuspensionLogByPlayerIdAsync(int playerId);

        /// <summary>
        /// Adds new player suspension
        /// </summary>
        /// <param name="playerSuspensionLog">The player suspension log</param>
        /// <returns>Returns id of added player suspension log. If fails return 0</returns>
        Task<int> AddPlayersStatsAsync(PlayerSuspensionLog playerSuspensionLog);

        /// <summary>
        /// Delete player suspension log
        /// </summary>
        /// <param name="playerSuspensionLogId">The player suspension log id </param>
        /// <returns>Returns true if player stats log was deleted</returns>
        Task<bool> DeletePlayerStatsLogAsync(int playerSuspensionLogId);
    }
}
