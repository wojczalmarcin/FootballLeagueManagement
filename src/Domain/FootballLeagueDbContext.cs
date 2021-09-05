using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<CardsLog> CardsLogs { get; set; }

        public DbSet<FootballerPitchTime> FootballerPitchTimes { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<MemberRole> MembersRoles { get; set; }

        public DbSet<PlayerStats> PlayersStats { get; set; }

        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
