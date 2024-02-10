using Microsoft.Extensions.Logging;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Domain.Events;
using ProductWebApi.Application.Features.Products.EventHandlers;
using Moq;
using NUnit.Framework;
using System.Threading;

namespace Application.Unit.Tests.Features.Products.EventHandlers
{
    public class PriceChangedEventHandlerTests
    {
        [Test]
        public void PriceChangedEvent_LoggerCalled()
        {
            // Arrenge
            var domainEvent = new ProductUpdatePriceEvent(It.IsAny<Product>());
            var handler = new PriceChangedEventHandler(Mock.Of<ILogger<PriceChangedEventHandler>>());

            // Act
            handler.Handle(domainEvent, CancellationToken.None);

            // Assert            
            // TODO: Do something first in the event
        }
    }
}
