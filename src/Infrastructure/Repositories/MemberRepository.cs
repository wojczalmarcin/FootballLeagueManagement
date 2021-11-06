﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// The member repository
    /// </summary>
    public class MemberRepository : IMemberRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public MemberRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets league member by id
        /// </summary>
        /// <param name="memberId">The member Id</param>
        /// <returns>The member of the league</returns>
        public async Task<Member> GetMemberByIdAsync(int memberId)
            => await _dbContext.Members
                    .AsNoTracking()
                    .Include(m => m.MemberRole)
                    .Include(m => m.Team)
                    .FirstOrDefaultAsync(m => m.Id == memberId);

        /// <summary>
        /// Gets league members by role id
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>The member of the league</returns>
        public async Task<IEnumerable<Member>> GetMembersByRoleIdAsync(int roleId)
            => await _dbContext.Members
                .AsNoTracking()
                .Include(m => m.MemberRole)
                .Include(m => m.Team)
                .Where(m => m.MemberRoleId == roleId)
                .ToListAsync();

        /// <summary>
        /// Gets league members by role id and team id
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <param name="teamId">The team Id</param>
        /// <returns>The member of the league</returns>
        public async Task<IEnumerable<Member>> GetMembersByRoleIdAsync(int roleId, int teamId)
            => await _dbContext.Members
                .AsNoTracking()
                .Include(m => m.MemberRole)
                .Where(m => m.MemberRoleId == roleId && m.TeamId == teamId)
                .ToListAsync();

        /// <summary>
        /// Adds new member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns id of added member. If fails return 0</returns>
        public async Task<int> AddMemberAsync(Member member)
        {
            await _dbContext.Members.AddAsync(member);
            if (await _dbContext.SaveChangesAsync() > 0)
                return member.Id;
            return 0;
        }

        /// <summary>
        /// Edits member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns true if member was edited</returns>
        public async Task<bool> EditMemberAsync(Member member)
        {
            _dbContext.Members.Update(member);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete member
        /// </summary>
        /// <param name="season">The member</param>
        /// <returns>Returns true if member was deleted</returns>
        public async Task<bool> DeleteMemberAsync(int memberId)
        {
            var member = await _dbContext.Members.FindAsync(memberId);
            if (member == null)
                return false;

            _dbContext.Members.Remove(member);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}