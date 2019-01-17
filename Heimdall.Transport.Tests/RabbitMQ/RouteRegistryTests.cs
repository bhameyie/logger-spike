using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FluentAssertions;
using Heimdal.Transport;
using Heimdal.Transport.Interfaces;
using Heimdall.Transport.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;

namespace Heimdall.Transport.Tests.RabbitMQ
{
    public class RouteRegistryTests
    {
        private DefaultRouteRegistry _sut;

        [SetUp]
        public void Init()
        {
            _sut = new DefaultRouteRegistry(new StubConfig());
        }

        [Test]
        public void Should_Throw_GatewayException_CannotQuery_Route()
        {
            _sut.Invoking(e => e.For<UnknownMessage>())
                .Should()
                .Throw<GatewayException>()
                .Which.Message
                .Should()
                .Be($"Unable to detect route for {typeof(UnknownMessage).FullName}");
        }

        [Test]
        public void Should_Return_Full_Uri()
        {
            _sut.For<KnownMessage>()
                .Should()
                .BeEquivalentTo(new Uri("rabbitmq://host/hello/hey"));
        }



        private class KnownMessage : IMessage
        {
            public Guid CorrelationId { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private class UnknownMessage : IMessage
        {
            public Guid CorrelationId { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private class StubConfig : IConfiguration
        {
            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public string this[string key]
            {
                get
                {
                    if (key == "RabbitMQ:Host")
                    {
                        return "host";
                    }

                    if (key == "RabbitMQ:VHost")
                    {
                        return "/hello";
                    }

                    if (key == "Heimdall:Routes:" + typeof(KnownMessage).FullName)
                    {
                        return "hey";
                    }
                    throw new AbandonedMutexException("The mutex is feeling lonely");
                }
                set => throw new NotImplementedException();
            }
        }
    }
}
