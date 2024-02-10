using ProductWebApi.Application.Domain.Events;
using ProductWebApi.Application.Features.Products.Commands;

namespace ProductWebApi.Application.Domain.Entities;

public class Product(int productId, string name, string statusName, double stock,string description, double price,double discount , double finalPrice,int categoryId)
    : IHasDomainEvent
{
    public int ProductId { get; set; } = productId;
    public string Name { get; private set; } = name;
    public string StatusName { get; private set; } = statusName;
    public double Stock { get; private set; } = stock;
    public string Description { get; private set; } = description;
    public double Price { get; private set; } = price;
    public double Discount { get; private set; } = discount;
    public double FinalPrice { get; private set; } = finalPrice;

    public int CategoryId { get; private set; } = categoryId;
    public Category? Category { get; private set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public void UpdateInfo(UpdateProduct.UpdateProductCommand command)
    {
        if (Price != command.Price)
        {
            DomainEvents.Add(new ProductUpdatePriceEvent(this));
        }

        Name = command.Name!;
        StatusName = command.StatusName!;
        Stock =  command.Stock;
        Description = command.Description!;
        Price = command.Price;
        Discount = command.Discount;
        FinalPrice = command.FinalPrice;
        CategoryId = command.CategoryId;
    }
}