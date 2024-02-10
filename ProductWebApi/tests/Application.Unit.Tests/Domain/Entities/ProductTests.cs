using FluentAssertions;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Features.Products.Commands;
using NUnit.Framework;

namespace Application.Unit.Tests.Domain.Entities
{
    public class ProductTests
    {
        [TestCase(1, 0)]
        [TestCase(999, 1)]
        public void ProductPriceChanged(double price, int eventsCount)
        {
            // Arrenge
            var product = new Product(1, "Name 1", "1", 5,"Description 1", 1, 1,1,1);
            var command = new UpdateProduct.UpdateProductCommand
            {
                CategoryId = 1,
                Description = "New description",
                Name = "New name",
                StatusName="1",
                Stock=5,
                Price = price,
                Discount=1,
                FinalPrice=1
            };

            // Act
            product.UpdateInfo(command);

            // Assert
            product.Price.Should().Be(price);
            product.DomainEvents.Count.Should().Be(eventsCount);
        }
    }
}
