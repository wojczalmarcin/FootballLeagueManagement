using Autofac;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace WebAPI.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddressRepository>().As<IAddressRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            builder.RegisterType<MatchRepository>().As<IMatchRepository>();
            builder.RegisterType<SeasonRepository>().As<ISeasonRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<PlayerStatsLogRepository>().As<IPlayerStatsLogRepository>();
        }
    }
}
