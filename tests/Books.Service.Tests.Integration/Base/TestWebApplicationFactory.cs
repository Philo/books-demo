using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Books.Service.Tests.Integration.Base;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureAppConfiguration(config => 
            config.AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build()
        );

        return base.CreateHost(builder);
    }
}
