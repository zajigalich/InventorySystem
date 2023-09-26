using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using InventorySystem.API.Models;

namespace InventorySystem.Tests
{
	public class ProductsControllerTests : IClassFixture<TestFixture>
	{
		private readonly TestFixture _fixture;

		public ProductsControllerTests(TestFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public async Task GetAllProducts_ReturnsOk()
		{
			// Arrange
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/Products");

			// Act
			var response = await _fixture.HttpClient.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task GetProductById_ReturnsProductWhenIdExists()
		{
			// Arrange
			int productId = 1;
			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Products/{productId}");

			// Act
			var response = await _fixture.HttpClient.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var product = await response.Content.ReadFromJsonAsync<Product>();
			Assert.NotNull(product);
			Assert.Equal(productId, product.Id);
		}

		[Fact]
		public async Task CreateProduct_ReturnsCreated()
		{
			// Arrange
			var newProduct = new Product { Name = "Test Product", Description = "Test Description", Price = 10, Quantity = 5 };
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/Products");
			request.Content = JsonContent.Create(newProduct);

			// Act
			var response = await _fixture.HttpClient.SendAsync(request);

			// Assert
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
			var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
			Assert.NotNull(createdProduct);
			Assert.Equal(newProduct.Name, createdProduct.Name);
			Assert.Equal(newProduct.Description, createdProduct.Description);
			Assert.Equal(newProduct.Price, createdProduct.Price);
			Assert.Equal(newProduct.Quantity, createdProduct.Quantity);
		}

		[Fact]
		public async Task CreateProduct_WithEmptyName_ReturnsBadRequest()
		{
			// Arrange
			var newProduct = new Product
			{
				Name = "",
				Description = "Sample Description",
				Price = 10,
				Quantity = 5
			};

			// Act
			var response = await _fixture.HttpClient.PostAsJsonAsync("/api/products", newProduct);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task CreateProduct_WithNegativePrice_ReturnsBadRequest()
		{
			// Arrange
			var newProduct = new Product
			{
				Name = "Test Product",
				Description = "Sample Description",
				Price = -1,
				Quantity = 5
			};

			// Act
			var response = await _fixture.HttpClient.PostAsJsonAsync("/api/products", newProduct);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task CreateProduct_WithNegativeQuantity_ReturnsBadRequest()
		{
			// Arrange
			var newProduct = new Product
			{
				Name = "Test Product",
				Description = "Sample Description",
				Price = 10,
				Quantity = -1
			};

			// Act
			var response = await _fixture.HttpClient.PostAsJsonAsync("/api/products", newProduct);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task UpdateProduct_ReturnsNoContentWhenIdExists()
		{
			// Arrange
			int productId = 1;
			var updatedProduct = new Product { Id = productId, Name = "Updated Name", Description = "Updated Description", Price = 15, Quantity = 8 };
			var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Products/{productId}");
			request.Content = JsonContent.Create(updatedProduct);

			// Act
			var response = await _fixture.HttpClient.SendAsync(request);

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

		[Fact]
		public async Task UpdateProduct_NonExistingId_ReturnsNotFound()
		{
			// Arrange
			var updateProduct = new Product
			{
				Id = 99999,
				Name = "Updated Product Name",
				Description = "Updated Description",
				Price = 20,
				Quantity = 10
			};

			// Act
			var response = await _fixture.HttpClient.PutAsJsonAsync($"/api/products/{updateProduct.Id}", updateProduct);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task DeleteProduct_ReturnsProductWhenIdExists()
		{
			// Arrange
			int productId = 2;
			var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/Products/{productId}");

			// Act
			var response = await _fixture.HttpClient.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var deletedProduct = await response.Content.ReadFromJsonAsync<Product>();
			Assert.NotNull(deletedProduct);
			Assert.Equal(productId, deletedProduct.Id);
		}

		[Fact]
		public async Task DeleteProduct_NonExistingId_ReturnsNotFound()
		{
			// Arrange
			int nonExistingId = 99999;

			// Act
			var response = await _fixture.HttpClient.DeleteAsync($"/api/products/{nonExistingId}");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}