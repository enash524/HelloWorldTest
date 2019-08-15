using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorldTest.WebApi.Tests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                ServiceProvider serviceProvider = new ServiceCollection()
                    //.AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.
                //services.AddDbContext<INorthwindDbContext, NorthwindDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryDbForTesting");
                //    options.UseInternalServiceProvider(serviceProvider);
                //});

                // Build the service provider.
                ServiceProvider sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (NorthwindDbContext)
                //using (var scope = sp.CreateScope())
                //{
                //    var scopedServices = scope.ServiceProvider;
                //    var context = scopedServices.GetRequiredService<INorthwindDbContext>();
                //    var logger = scopedServices
                //        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                //    var concreteContext = (NorthwindDbContext)context;

                //    // Ensure the database is created.
                //    concreteContext.Database.EnsureCreated();

                //    try
                //    {
                //        // Seed the database with test data.
                //        Utilities.InitializeDbForTests(concreteContext);
                //    }
                //    catch (Exception ex)
                //    {
                //        logger.LogError(ex, $"An error occurred seeding the " +
                //                            "database with test messages. Error: {ex.Message}");
                //    }
                //}
            });
        }
    }
}
