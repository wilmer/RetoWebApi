using ProductWebApi.Application.Domain.Entities;

namespace ProductWebApi.Application.Domain.Events
{
    public class ProductUpdatePriceEvent(Product product) : DomainEvent
    {
        public Product Product { get; set; } = product;
    }
}
