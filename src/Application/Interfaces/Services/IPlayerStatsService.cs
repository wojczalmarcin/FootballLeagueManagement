using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// The player stats service interface
    /// </summary>
    public interface IPlayerStatsService
    {
        /// <summary>
        /// Gets players stats data by match id
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <returns>Response with players stats data</returns>
        Task<ResponseData<IEnumerable<PlayerStatsDto>>> GetPlayersStatsByMatchId(int matchId);

        /// <summary>
        /// Creates new player stats
        /// </summary>
        /// <param name="playerStatsToCreate">The player stats to create</param>
        /// <returns>Response data with created player stats</returns>
        Task<ResponseData<PlayerStatsDto>> CreatePlayerStats(CreatePlayerStatsDto playerStatsToCreate);

        /// <summary>
        /// Deletes player stats by od
        /// </summary>
        /// <param name="playerStatsId">The player stats id</param>
        /// <returns>Response data with deleted player stats</returns>
        Task<ResponseData<PlayerStatsDto>> DeletePlayerStats(int playerStatsId);
    }
}
