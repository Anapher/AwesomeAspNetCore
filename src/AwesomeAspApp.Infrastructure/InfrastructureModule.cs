using Autofac;
using AwesomeAspApp.Core.Interfaces.Gateways.Repositories;
using AwesomeAspApp.Core.Interfaces.Services;
using AwesomeAspApp.Infrastructure.Auth;
using AwesomeAspApp.Infrastructure.Identity.Repositories;
using AwesomeAspApp.Infrastructure.Interfaces;

namespace AwesomeAspApp.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
            builder.RegisterType<JwtHandler>().As<IJwtHandler>().SingleInstance();
            builder.RegisterType<TokenFactory>().As<ITokenFactory>().SingleInstance();
            builder.RegisterType<JwtValidator>().As<IJwtValidator>().SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces();
        }
    }
}
