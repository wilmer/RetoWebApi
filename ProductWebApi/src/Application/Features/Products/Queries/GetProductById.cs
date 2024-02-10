using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Infrastructure.Persistence;
using System;
using static ProductWebApi.Application.Features.Products.Commands.DeleteProduct;
using static ProductWebApi.Application.Features.Products.Queries.GetProducts;

namespace ProductWebApi.Application.Features.Products.Queries;

public class GetProductsById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{productId}", async (IMediator mediator, int productId) =>
        {
            return await mediator.Send(new GetProductsQueryById(productId));
        })
        .WithName(nameof(GetProductsById))
        .WithTags(nameof(Product));

    }

    public class GetProductsQueryById(int productId) : IRequest<List<GetProductsResponseById>>
    {
        public int ProductId { get; set; } = productId;
    }

    public class GetProductsHandlerById(ApiDbContext context, IMapper mapper)
        : IRequestHandler<GetProductsQueryById, List<GetProductsResponseById>>
    {
        public Task<List<GetProductsResponseById>> Handle(GetProductsQueryById request, CancellationToken cancellationToken) =>
            context.Products.ProjectTo<GetProductsResponseById>(mapper.ConfigurationProvider).ToListAsync();
    }

    public class GetProductsMappingProfile : Profile
    {
        public GetProductsMappingProfile() => CreateMap<Product, GetProductsResponseById>()
            .ForMember(
                d => d.CategoryName,
                opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
            );
    }

    public record GetProductsResponseById(int ProductId, string Name, string StatusName, double Stock, string Description, double Price, double Discount, double FinalPrice, string CategoryName);
}