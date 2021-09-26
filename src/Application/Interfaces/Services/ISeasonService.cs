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
        Task<ResponseData<SeasonDto>> GetSeasonById(int seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Response data with collection of <see cref="SeasonDto"/></returns>
        Task<ResponseData<IEnumerable<SeasonDto>>> GetAllSeasons();
    }
}
