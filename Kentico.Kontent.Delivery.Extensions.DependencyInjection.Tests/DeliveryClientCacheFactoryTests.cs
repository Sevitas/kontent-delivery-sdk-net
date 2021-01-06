using FakeItEasy;
using FluentAssertions;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Caching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kentico.Kontent.Delivery.Extensions.DependencyInjection.Tests
{
    public class DeliveryClientCacheFactoryTests
    {
        private readonly IOptionsMonitor<DeliveryCacheOptions> _deliveryCacheOptionsMock;
        private readonly IDeliveryClientFactory _innerDeliveryClientFactoryMock;
        private readonly IServiceProvider _serviceProvider;

        private const string _clientName = "ClientName";

        public DeliveryClientCacheFactoryTests()
        {
            _deliveryCacheOptionsMock = A.Fake<IOptionsMonitor<DeliveryCacheOptions>>();
            _innerDeliveryClientFactoryMock = A.Fake<IDeliveryClientFactory>();
            _serviceProvider = new ServiceCollection().BuildServiceProvider();
        }

        [Fact]
        public void GetNamedCacheClient_WithCorrectName_GetClient()
        {
            var deliveryCacheOptions = new DeliveryCacheOptions();
            A.CallTo(() => _deliveryCacheOptionsMock.Get(_clientName))
                .Returns(deliveryCacheOptions);

            var deliveryClientFactory = new DeliveryClientCacheFactory(_innerDeliveryClientFactoryMock, _deliveryCacheOptionsMock, _serviceProvider);

            var result = deliveryClientFactory.Get(_clientName);

            result.Should().NotBeNull();
        }

        [Fact]
        public void GetNamedCacheClient_WithWrongName_GetNull()
        {
            var deliveryCacheOptions = new DeliveryCacheOptions();
            A.CallTo(() => _deliveryCacheOptionsMock.Get(_clientName))
                .Returns(deliveryCacheOptions);

            var deliveryClientFactory = new DeliveryClientCacheFactory(_innerDeliveryClientFactoryMock, _deliveryCacheOptionsMock, _serviceProvider);

            var result = deliveryClientFactory.Get("WrongName");

            result.Should().NotBeNull();
        }
    }
}
