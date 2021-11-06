using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPlayerStatsLogRepository
    {
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
    }
}
