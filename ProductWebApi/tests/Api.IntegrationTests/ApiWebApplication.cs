using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductWebApi.Application.Infrastructure.Persistence;

namespace ProductWebApi.Api.IntegrationTests;

public class ApiWebApplication : WebApplicationFactory<Api>
{
    public const string TestConnectionString = "Server=LAPTOP-H5SP2A1L;Database=ProductWebApi;User=sa;Password='123456';Integrated security=False;TrustServerCertificate=true";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                // Usamos una LocalDB para pruebas de integración
                return new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(TestConnectionString)
                .UseApplicationServiceProvider(sp)
                .Options;
            });
        });

        return base.CreateHost(builder);
    }
}
