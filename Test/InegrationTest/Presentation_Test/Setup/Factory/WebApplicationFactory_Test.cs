using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Presentation_Test.Setup.Database;

namespace Presentation_Test.Setup.Factory
{
    public class WebApplicationFactory_Test : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //to avoid conflict between test and main app Database providers
            builder.UseEnvironment("IntegartionTest");

            builder.ConfigureServices(services =>
            {
                //add DatabaseConfiguration
                services.ConfigureDatabaseForSqlLite();
            });
        }
    }
}
