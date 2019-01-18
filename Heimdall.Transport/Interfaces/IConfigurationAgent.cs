namespace Heimdall.Transport.Interfaces
{
    public interface IConfigurationAgent
    {
        void Configure(TransportConfigurator configurator);
        void OnRelease(IConfiguredTransport transport);
        void OnStart(IConfiguredTransport transport);
    }
}