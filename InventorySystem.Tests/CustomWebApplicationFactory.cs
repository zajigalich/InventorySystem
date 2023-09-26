using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InventorySystem.API.Data;
using InventorySystem.API.Models;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Tests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		public string UniqueDbName { get; }

		public CustomWebApplicationFactory()
		{
			UniqueDbName = $"InMemoryTestingDatabase-{Guid.NewGuid()}";
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				// Remove the app's AppDbContext registration
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				// Add a new AppDbContext registration using an in-memory database for testing
				services.AddDbContext<AppDbContext>(options =>
				{
					options.UseInMemoryDatabase(UniqueDbName);
				});

				// Build the service provider and create a scope
				var sp = services.BuildServiceProvider();
				using var scope = sp.CreateScope();

				// Get the AppDbContext instance
				var scopedServices = scope.ServiceProvider;
				var db = scopedServices.GetRequiredService<AppDbContext>();

				// Ensure the database is created
				db.Database.EnsureCreated();

				try
				{
					// Seed initial data
					InitializeDb(db);
				}
				catch (System.Exception ex)
				{
					// Log any error occurred during data initialization
					var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
					logger.LogError(ex, "An error occurred seeding the database with test data.");
				}
			});
		}

		private void InitializeDb(AppDbContext db)
		{
			// Add initial data for testing
			var testProducts = new List<Product>()
			{
				new Product
				{
					Id = 1,
					Name = "Test Product",
					Description = "Test Description",
					Price = 10,
					Quantity = 5
				},
				new Product
				{
					Id = 2,
					Name = "Test Product 2",
					Description = "Test Description 2",
					Price = 5,
					Quantity = 10
				}
			};

			db.Products.AddRange(testProducts);
			db.SaveChanges();
		}
	}
}