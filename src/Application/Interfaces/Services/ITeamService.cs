using Application.DTO;
using Application.DTO.Create;
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

        /// <summary>
        /// Gets all teams
        /// </summary>
        /// <returns>The response data</returns>
        Task<ResponseData<IEnumerable<TeamDto>>> GetAllTeams();

        /// <summary>
        /// Edits team
        /// </summary>
        /// <param name="team">Team with edited data</param>
        /// <returns>Response data with edited team</returns>
        Task<ResponseData<TeamDto>> EditTeamAsync(TeamDto team);

        /// <summary>
        /// Creates given team
        /// </summary>
        /// <param name="team">The team to create</param>
        /// <returns>Response data with created team</returns>
        Task<ResponseData<TeamDto>> CreateTeamAsync(CreateTeamDto team);
    }
}
