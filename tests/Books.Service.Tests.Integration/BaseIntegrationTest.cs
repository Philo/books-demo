using System;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Service.Tests.Integration;

public class BaseIntegrationTest : IDisposable
{
    private readonly Services _services;
    protected ServiceProvider? ServiceProvider {get; private set; }

    public BaseIntegrationTest()
    {
        _services = new Services();
        ServiceProvider = _services.Provider;
    }

    public void Dispose()
    {
        _services.Dispose();
        GC.SuppressFinalize(this);
    }
}