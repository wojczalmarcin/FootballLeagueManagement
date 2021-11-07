using Application.DTO;
using Application.DTO.Edit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IMatchService
    {
        /// <summary>
        /// Gets match data by match id
        /// </summary>
        /// <param name="matchId">match id</param>
        /// <returns>Response with match data</returns>
        Task<ResponseData<MatchDto>> GetMatchDataById(int matchId);

        /// <summary>
        /// Gets matches by season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with collection of <see cref="MatchDto"/></returns>
        Task<ResponseData<IEnumerable<MatchDto>>> GetMatchesDataBySeasonId(int seasonId);

        /// <summary>
        /// Gets season table by season id
        /// </summary>
        /// <param name="seasonId">Season id</param>
        /// <returns>Response data with collection of <see cref="TeamStatisticsDto"/></returns>
        Task<ResponseData<IEnumerable<TeamStatisticsDto>>> GetSeasonTable(int seasonId);

        /// <summary>
        /// Creates new match
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Response data with created match</returns>
        Task<ResponseData<MatchDto>> CreateMatchAsync(CreateMatchDto match);

        /// <summary>
        /// Deletes match
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <returns>Response data with deleted match</returns>
        Task<ResponseData<MatchDto>> DeleteMatchAsync(int matchId);

        /// <summary>
        /// Creates new match
        /// </summary>
        /// <param name="match">The match</param>
        /// <returns>Response data with created match</returns>
        Task<ResponseData<MatchDto>> EditMatchAsync(EditMatchDto match);
    }
}
