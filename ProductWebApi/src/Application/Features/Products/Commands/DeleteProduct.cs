using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Infrastructure.Persistence;

namespace ProductWebApi.Application.Features.Products.Commands;

public class DeleteProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{productId}", async (IMediator mediator, int productId) =>
        {
            return await mediator.Send(new DeleteProductCommand(productId));
        })
        .WithName(nameof(DeleteProduct))
        .WithTags(nameof(Product))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    public class DeleteProductCommand(int productId) : IRequest<IResult>
    {
        public int ProductId { get; set; } = productId;
    }

    public class DeleteProductHandler(ApiDbContext context) : IRequestHandler<DeleteProductCommand, IResult>
    {
        public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.NotFound();
            }

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}