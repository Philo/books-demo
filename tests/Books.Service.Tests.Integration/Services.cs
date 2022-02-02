using System;
using AutoMapper;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Startup;
using Books.Service.Infrastructure.Startup;
using Books.Service.Web.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Service.Tests.Integration;

public class Services : IDisposable
{
    public ServiceProvider? Provider { get; private set; }
    private bool _disposedValue;

    public Services()
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile("app-settings-test.json")
                .Build();

        var services = new ServiceCollection();

        services
            .AddLogging()
            .AddCoreServices()
            .AddInfrastructureServices(config);

        var mapperConfig = new MapperConfiguration(c => {
            c.AddProfile<MappingProfile>();
        });
        services.AddSingleton(s => mapperConfig.CreateMapper());

        Provider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Provider?.Dispose();
                Provider = null;
            }

            _disposedValue = true;
        }
    }
}