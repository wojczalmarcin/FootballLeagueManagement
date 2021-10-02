using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISeasonRepository
    {
        /// <summary>
        /// Gets season by id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns>Season</returns>
        Task<Season> GetSeasonByIdAsync(int seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Collection of seasons</returns>
        Task<IEnumerable<Season>> GetAllSeasonsAsync();


        /// <summary>
        /// Adds new season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Returns true if season added</returns>
        Task<int> AddSeasonAsync(Season season);

        /// <summary>
        /// Edits season
        /// </summary>
        /// <param name="season">The season</param>
        /// <returns>Returns true if season edited</returns>
        Task<bool> EditSeasonAsync(Season season);
    }
}
