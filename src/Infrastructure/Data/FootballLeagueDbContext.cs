using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<FootballerPitchTime> FootballerPitchTimes { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchesScore> MatchesScores { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<MemberRole> MembersRoles { get; set; }

        public DbSet<PlayerStatsLog> PlayersStatsLogs { get; set; }

        public DbSet<PlayerStatType> PlayerStatTypes { get; set; }

        public DbSet<PlayerSuspensionLog> PlayerSuspensionLogs { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<SeasonTeam> SeasonsTeams { get; set; }

        public DbSet<Stadium> Stadiums { get; set; }

        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
