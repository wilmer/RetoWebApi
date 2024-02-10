using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductWebApi.Application.Infrastructure.Persistence;
using Moq;

namespace Application.Unit.Tests;

public class DbContextInMemoryFactory
{
    public static ApiDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(nameof(ApiDbContext))
            .Options;


        return new ApiDbContext(options, Mock.Of<IMediator>(), Mock.Of<ILogger<ApiDbContext>>());
    }
}
