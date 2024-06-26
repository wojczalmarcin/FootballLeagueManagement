﻿using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// The member repository interface
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// Gets league member by id
        /// </summary>
        /// <param name="memberId">The member Id</param>
        /// <returns>The member of the league</returns>
        Task<Member> GetMemberByIdAsync(int memberId);

        /// <summary>
        /// Gets league player by id
        /// </summary>
        /// <param name="playerId">The player Id</param>
        /// <returns>The member of the league</returns>
        Task<Member> GetPlayerByIdAsync(int playerId);

        /// <summary>
        /// Gets league members by role id
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>The member of the league</returns>
        Task<IEnumerable<Member>> GetMembersByRoleIdAsync(int roleId);

        /// <summary>
        /// Gets league members by role id and team id
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <param name="teamId">The team Id</param>
        /// <returns>The member of the league</returns>
        Task<IEnumerable<Member>> GetMembersByRoleIdAsync(int roleId, int teamId);

        /// <summary>
        /// Adds new member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns id of added member. If fails return 0</returns>
        Task<int> AddMemberAsync(Member member);

        /// <summary>
        /// Edits member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns true if member was edited</returns>
        Task<bool> EditMemberAsync(Member member);

        /// <summary>
        /// Delete member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns true if member was deleted</returns>
        Task<bool> DeleteMemberAsync(int memberId);

        /// <summary>
        /// Gets members by match id and role id
        /// </summary>
        /// <param name="matchId">The match id</param>
        /// <param name="roleId">The role id</param>
        /// <returns>Collection of members</returns>
        Task<IEnumerable<Member>> GetMembersByMatchIdAndRoleIdAsync(int matchId, int roleId);

        /// <summary>
        /// Gets members by team id, page size and page number
        /// </summary>
        /// <param name="page">The page</param>
        /// <param name="teamId">The team id</param>
        /// <returns>The members of the league</returns>
        Task<IEnumerable<Member>> GetMembersByTeamIdAsync((int size, int number) page, int teamId);

        /// <summary>
        /// Gets the number of members in given team
        /// </summary>
        /// <param name="teamId">The team id</param>
        /// <returns>Number of members</returns>
        Task<int> CountMembersByTeamIdAsync(int teamId);

        /// <summary>
        /// Gets the number of members
        /// </summary>
        /// <returns>Number of members</returns>
        Task<int> CountMembersAsync();
    }
}
