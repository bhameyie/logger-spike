using Autofac;
using Heimdal.Transport.Interfaces;

namespace Heimdall.Transport.RabbitMQ
{
    public class RabbitModule : Module
    {
        private readonly IRouteRegistry _routeRegistry;

        public RabbitModule(IRouteRegistry routeRegistry)
        {
            _routeRegistry = routeRegistry;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitMqGateway>()
                .As<IHeimdallGateway>()
                .InstancePerLifetimeScope();

            if (_routeRegistry == null)
            {
                builder.RegisterType<DefaultRouteRegistry>().As<IRouteRegistry>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterInstance(_routeRegistry).As<IRouteRegistry>().InstancePerLifetimeScope();
            }
        }
    }
}