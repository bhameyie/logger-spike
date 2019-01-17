namespace Heimdal.Transport.Interfaces
{
    public interface IConfigurationAgent
    {
        void Configure(TransportConfigurator configurator);
        void Release(IConfiguredTransport transport);
    }
}