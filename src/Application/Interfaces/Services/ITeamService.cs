using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// The team service interface
    /// </summary>
    public interface ITeamService
    {
        /// <summary>
        /// Gets team by id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>The response data</returns>
        Task<ResponseData<TeamDto>> GetTeamByIdAsync(int teamId);

        /// <summary>
        /// Gets teams by season Id
        /// </summary>
        /// <param name="seasonId">Seson Id</param>
        /// <returns>The response data</returns>
        Task<ResponseData<IEnumerable<TeamDto>>> GetTeamsBySeasonIdAsync(int seasonId);
    }
}
