using Application.Interfaces.Services;
using Application.Services.Match;
using Application.Services.Member;
using Application.Services.PlayerStats;
using Application.Services.PlayerStatType;
using Application.Services.Season;
using Application.Services.Team;
using Autofac;

namespace WebAPI.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchService>().As<IMatchService>();
            builder.RegisterType<SeasonService>().As<ISeasonService>();
            builder.RegisterType<TeamService>().As<ITeamService>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<PlayerStatsService>().As<IPlayerStatsService>();
            builder.RegisterType<PlayerStatTypeService>().As<IPlayerStatTypeService>();
        }
    }
}
