using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Service.Core.Startup;

public static class RegisterServices
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}