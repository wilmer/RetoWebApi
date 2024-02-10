﻿using Carter;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Infrastructure.Persistence;

namespace ProductWebApi.Application.Features.Products.Commands;

public class UpdateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products", async (IMediator mediator, UpdateProductCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(UpdateProduct))
        .WithTags(nameof(Product))
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
    }

    public class UpdateProductCommand : IRequest<IResult>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductHandler(ApiDbContext context, IValidator<UpdateProductCommand> validator)
        : IRequestHandler<UpdateProductCommand, IResult>
    {
        public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.GetValidationProblems());
            }

            var product = await context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.NotFound();
            }

            product.UpdateInfo(request);

            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(r => r.ProductId).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
        }
    }
}