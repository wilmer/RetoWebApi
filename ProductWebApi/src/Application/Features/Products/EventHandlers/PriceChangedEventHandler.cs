using MediatR;
using Microsoft.Extensions.Logging;
using ProductWebApi.Application.Domain.Events;

namespace ProductWebApi.Application.Features.Products.EventHandlers
{
    public class PriceChangedEventHandler(ILogger<PriceChangedEventHandler> logger)
        : INotificationHandler<ProductUpdatePriceEvent>
    {
        public Task Handle(ProductUpdatePriceEvent notification, CancellationToken cancellationToken)
        {
            logger.LogWarning("Product Web APIs Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
