using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPlayerStatsService
    {
        /// <summary>
        /// Gets players stats data by match id
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <returns>Response with players stats data</returns>
        Task<ResponseData<IEnumerable<PlayerStatsDto>>> GetPlayersStatsByMatchId(int matchId);
    }
}
