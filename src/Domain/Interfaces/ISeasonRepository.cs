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
        Task<Season> GetSeasonById(int seasonId);

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>Collection of seasons</returns>
        Task<IEnumerable<Season>> GetAllSeasons();
    }
}
