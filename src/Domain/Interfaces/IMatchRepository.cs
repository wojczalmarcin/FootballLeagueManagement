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
    }
}
