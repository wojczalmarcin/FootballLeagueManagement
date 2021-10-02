using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISeasonService
    {
        /// <summary>
        /// Gets season by id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Response data with <see cref="SeasonDto"/></returns>
        Task<ResponseData<SeasonDto>> GetSeasonByIdAsync(int seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data with collection of <see cref="SeasonDto"/></returns>
        Task<ResponseData<IEnumerable<SeasonDto>>> GetAllSeasonsAsync();

        /// <summary>
        /// Edits season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Response data witch edited season</returns>
        Task<ResponseData<SeasonDto>> EditSeasonAsync(SeasonDto season);

        /// <summary>
        /// Creates new season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Response data with created season</returns>
        Task<ResponseData<SeasonDto>> CreateSeasonAsync(CreateSeasonDto season);
    }
}
