using Autofac;
using AwesomeAspApp.Core.Interfaces;

namespace AwesomeAspApp.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(IUseCaseRequestHandler<,>)).AsImplementedInterfaces();
        }
    }
}
