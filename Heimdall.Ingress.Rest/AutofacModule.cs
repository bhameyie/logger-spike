using Autofac;
using Heimdall.Ingress.Services;

namespace Heimdall.Ingress
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestValidator>().As<IRequestValidator>().InstancePerLifetimeScope();
            builder.RegisterType<RequestTranslator>().As<IRequestTranslator>().InstancePerLifetimeScope();
            builder.RegisterType<SightingService>().As<ISightingService>().InstancePerLifetimeScope();
        }
    }
}