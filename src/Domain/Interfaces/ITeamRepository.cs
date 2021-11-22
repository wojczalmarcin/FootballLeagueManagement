using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// The team repository interface
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Gets team by it's Id
        /// </summary>
        /// <param name="teamId">team Id</param>
        /// <returns>Teams</returns>
        Task<Team> GetTeamByIdAsync(int teamId);

        /// <summary>
        /// Gets teams by season id
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Collection of teams</returns>
        Task<IEnumerable<Team>> GetTeamsBySeasonIdAsync(int seasonId);

        /// <summary>
        /// Adds new team
        /// </summary>
        /// <param name="team">The team</param>
        /// <returns>Returns id of added team. If fails return 0</returns>
        Task<int> AddTeamAsync(Team team);

        /// <summary>
        /// Edits team
        /// </summary>
        /// <param name="team">The team</param>
        /// <returns>Returns true if team edited</returns>
        Task<bool> EditTeamAsync(Team team);

        /// <summary>
        /// Gets all teams
        /// </summary>
        /// <returns>Collection of teams</returns>
        Task<IEnumerable<Team>> GetAllTeamsAsync();
    }
}
