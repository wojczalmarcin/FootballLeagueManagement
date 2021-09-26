using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// The match score repository interface
    /// </summary>
    public interface IMatchRepository
    {
        /// <summary>
        /// Gets Match score by match Id
        /// </summary>
        /// <param name="matchId">match Id</param>
        /// <returns>Match score</returns>
        Task<Match> GetMatchByIdAsync(int matchId);

        /// <summary>
        /// Gets matches from given season
        /// </summary>
        /// <param name="seasonId">season id</param>
        /// <returns>Collection of matches</returns>
        Task<IEnumerable<Match>> GetMatchesBySeasonIdAsync(int seasonId);

        /// <summary>
        /// Gets matches witch given team ids
        /// </summary>
        /// <param name="teamIds">List of team ids</param>
        /// <returns>Collection of matches</returns>
        IEnumerable<Match> GetMatchesByTeamIds(List<int> teamIds);
    }
}
