using Autofac;
using Heimdal.Transport.Interfaces;

namespace Heimdall.Transport.RabbitMQ
{
    public class RabbitModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitMqGateway>().As<IHeimdallGateway>().InstancePerLifetimeScope();
        }
    }
}