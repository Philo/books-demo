using Books.Service.Core.Interfaces;
using Books.Service.Infrastructure.Repositories;
using Books.Service.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Service.Infrastructure.Startup;

public static class RegisterServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddTransient<IBookRepository, MongoBookRepository>();
    }
}