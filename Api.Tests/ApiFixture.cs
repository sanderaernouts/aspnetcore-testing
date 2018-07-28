using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Api.Tests
{
    public class ApiTestFixture : WebApplicationFactory<TestStartUp>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Development")
                .UseContentRoot(TestContext.CurrentContext.TestDirectory)
                .ConfigureServices(
                    services =>
                    {
                        services.Configure<MvcCompatibilityOptions>(
                            options => options.CompatibilityVersion = CompatibilityVersion.Version_2_1);
                    });
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartUp>();
        }
    }
}
