using Carter;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProductWebApi.Application.Domain.Entities;
using ProductWebApi.Application.Infrastructure.Persistence;

namespace ProductWebApi.Application.Features.Products.Commands;

public class CreateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async (HttpRequest req, IMediator mediator, CreateProductCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(CreateProduct))
        .WithTags(nameof(Product))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }

    public class CreateProductCommand : IRequest<IResult>
    {
        public string Name { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public double Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductHandler(ApiDbContext context, IValidator<CreateProductCommand> validator)
        : IRequestHandler<CreateProductCommand, IResult>
    {
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.GetValidationProblems());
            }

            var newProduct = new Product(0, request.Name, request.StatusName , request.Stock,request.Description, request.Price,request.Discount, request.FinalPrice , request.CategoryId);

            context.Products.Add(newProduct);

            await context.SaveChangesAsync();

            return Results.Created($"api/products/{newProduct.ProductId}", null);
        }
    }

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.StatusName).NotEmpty();
            RuleFor(r => r.Stock).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
            RuleFor(r => r.Discount).NotEmpty();
            RuleFor(r => r.FinalPrice).NotEmpty();
        }
    }
}