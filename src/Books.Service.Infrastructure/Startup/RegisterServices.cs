using Books.Service.Core.Interfaces;
using Books.Service.Infrastructure.Mongo.Repositories;
using Books.Service.Infrastructure.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Service.Infrastructure.Startup;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddTransient<IBookRepository, MongoBookRepository>();

        return services;
    }
}