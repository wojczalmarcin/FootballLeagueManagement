using Domain.Entities;
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
    }
}
