using Application.Interfaces.Validators;
using Application.Services.Match;
using Application.Services.Season;
using Autofac;

namespace WebAPI.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchValidator>().As<IMatchValidator>();
            builder.RegisterType<SeasonValidator>().As<ISeasonValidator>();
        }
    }
}
