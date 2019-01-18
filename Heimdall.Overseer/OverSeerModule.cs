using Autofac;
using Heimdall.Overseer.Analyzers;

namespace Heimdall.Overseer
{
    public class OverSeerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SightingAnalyzerRepertoire>()
                .As<ISightingAnalyzerRepertoire>().SingleInstance();

            builder.RegisterType<ProtocolTranslator>()
                .As<IProtocolTranslator>().InstancePerLifetimeScope();
        }
    }
}