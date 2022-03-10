using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// The player suspension service
    /// </summary>
    public interface IPlayerSuspensionService
    {
        /// <summary>
        /// Gets suspension by id
        /// </summary>
        /// <param name="suspensionId">suspension id</param>
        /// <returns>Response data with <see cref="PlayerSuspensionDto"/></returns>
        Task<ResponseData<PlayerSuspensionDto>> GetPlayerSusnepsionByIdAsync(int suspensionId);

        /// <summary>
        /// Gets suspension by player id
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>Response data with the collection of <see cref="PlayerSuspensionDto"/></returns>
        Task<ResponseData<IEnumerable<PlayerSuspensionDto>>> GetPlayerSusnepsionByPlayerIdAsync(int playerId);
    }
}
