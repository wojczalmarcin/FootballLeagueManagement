using Application.DTO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMatchService
    {
        /// <summary>
        /// Gets match data by match id
        /// </summary>
        /// <param name="matchId">match id</param>
        /// <returns>Response with match data</returns>
        Task<ResponseData<MatchDto>> GetMatchDataById(int matchId);
    }
}
