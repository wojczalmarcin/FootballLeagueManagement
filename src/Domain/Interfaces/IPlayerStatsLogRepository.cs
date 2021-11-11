using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Player stats log repository interface
    /// </summary>
    public interface IPlayerStatsLogRepository
    {
        /// <summary>
        /// Gets player stats log by id
        /// </summary>
        /// <param name="playerStatsLogId">The player stats log id</param>
        /// <returns>Player stats log</returns>
        Task<PlayerStatsLog> GetPlayerStatsLogByIdAsync(int playerStatsLogId);

        /// <summary>
        /// Gets player stats log by player id
        /// </summary>
        /// <param name="playerId">The player Id</param>
        /// <returns>Player stats log</returns>
        Task<IEnumerable<PlayerStatsLog>> GetPlayerStatsLogByPlayerIdAsync(int playerId);


        /// <summary>
        /// Gets players stats by match id
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Collection of player stats log</returns>
        Task<IEnumerable<PlayerStatsLog>> GetPlayersStatsLogByMatchIdAsync(int matchId);

        /// <summary>
        /// Adds new player stats
        /// </summary>
        /// <param name="playerStatsLog">The player stats log</param>
        /// <returns>Returns id of added player stats log. If fails return 0</returns>
        Task<int> AddPlayersStatsAsync(PlayerStatsLog playerStatsLog);

        /// <summary>
        /// Delete player stats log
        /// </summary>
        /// <param name="playerStatsLogId">The player stats log id </param>
        /// <returns>Returns true if player stats log was deleted</returns>
        Task<bool> DeletePlayerStatsLogAsync(int playerStatsLogId);
    }
}
