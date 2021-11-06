using Application.Interfaces.Validators;
using Application.Services.Match;
using Application.Services.Member;
using Application.Services.PlayerStats;
using Application.Services.Season;
using Application.Services.Team;
using Autofac;

namespace WebAPI.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchValidator>().As<IMatchValidator>();
            builder.RegisterType<SeasonValidator>().As<ISeasonValidator>();
            builder.RegisterType<TeamValidator>().As<ITeamValidator>();
            builder.RegisterType<MemberValidator>().As<IMemberValidator>();
            builder.RegisterType<PlayerStatsValidator>().As<IPlayerStatsValidator>();
        }
    }
} 
