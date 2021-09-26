using Application.Interfaces.Services;
using Application.Services.Match;
using Application.Services.Season;
using Autofac;

namespace WebAPI.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchService>().As<IMatchService>();
            builder.RegisterType<SeasonService>().As<ISeasonService>();
        }
    }
}
