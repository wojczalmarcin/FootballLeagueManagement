using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// Player stat type service interface
    /// </summary>
    public interface IPlayerStatTypeService
    {

        /// <summary>
        /// Gets all player stats type
        /// </summary>
        /// <returns>Response data with collection of <see cref="PlayerStatTypeDto"/></returns>
        Task<ResponseData<IEnumerable<PlayerStatTypeDto>>> GetAllPlayerStatTypesAsync();
    }
}
