using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPlayerStatTypeRepository
    {
        /// <summary>
        /// Gets player stat type by id
        /// </summary>
        /// <param name="statTypeId">The player stat type Id</param>
        /// <returns>The player stat type</returns>
        Task<PlayerStatType> GetPlayerStatTypeByIdAsync(int statTypeId);

        /// <summary>
        /// Gets all player stat types
        /// </summary>
        /// <returns>The collection of player stat types</returns>
        Task<IEnumerable<PlayerStatType>> GetAllPlayerStatTypesAsync();
    }
}
