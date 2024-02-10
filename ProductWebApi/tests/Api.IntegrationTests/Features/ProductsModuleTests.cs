using FluentAssertions;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Features.Products.Commands;
using ProductWebApi.Application.Features.Products.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProductWebApi.Api.IntegrationTests.Features;


public class ProductsModuleTests : TestBase
{
    [Test]
    public async Task GetProducts()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        await AddAsync(new Product(0, "Test 01", "1", 20,"Desc 01", 1,2.5,17.5, testCategory.CategoryId));
        await AddAsync(new Product(0, "Test 02", "1",45,"Desc 02", 2, 0.5,44.5,testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var products = await client.GetFromJsonAsync<List<GetProducts.GetProductsResponse>>("/api/products");

        // Assert
        products.Should().NotBeNullOrEmpty();
        products.Count.Should().Be(2);
    }

    [Test]
    public async Task CreateProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));

        var client = Application.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("api/products", new CreateProduct.CreateProductCommand
        {
            Description = $"Test product description",
            Name = "Test name",
            StatusName = "1",
            Stock=123,
            Price = 123456,
            Discount=23.45,
            FinalPrice=23.55,
            CategoryId = testCategory.CategoryId,
        });

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task UpdateProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        var product1 = await AddAsync(new Product(0, "Test 01","1",20,"Desc 01", 1, 0,20 ,testCategory.CategoryId));
        await AddAsync(new Product(0,"Test 02", "0",23,"Desc 02", 2, 0, 46, testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync("api/products", new UpdateProduct.UpdateProductCommand
        {
            Description = "Updated Desc for ID 1",
            Name = "Updated name for ID 1",
            StatusName="0",
            Stock=23,
            Price = 999,
            ProductId = product1.ProductId,
            Discount=23.44,
            FinalPrice=456,
            CategoryId = product1.CategoryId
        });

        // Assert
        response.EnsureSuccessStatusCode();

        var updated = await FindAsync<Product>(product1.ProductId);

        updated.Name.Should().Be("Updated name for ID 1");
        updated.Description.Should().Be("Updated Desc for ID 1");
        updated.Price.Should().Be(999);
    }

    [Test]
    public async Task DeleteProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        var product1 = await AddAsync(new Product(0, "Test 01", "1",23,"Desc 01", 1, 0,1,testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"api/products/{product1.ProductId}");


        // Assert
        response.EnsureSuccessStatusCode();

        var deleted = await FindAsync<Product>(product1.ProductId);

        deleted.Should().BeNull();
    }

    [Test]
    public async Task DeleteProduct_Should_Fail()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"api/products/0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}