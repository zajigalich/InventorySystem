using InventorySystem.API;

namespace InventorySystem.Tests
{
	public class TestFixture : CustomWebApplicationFactory<Program>, IDisposable
	{
		private readonly HttpClient _httpClient;

		public TestFixture()
		{
			_httpClient = CreateClient();
		}

		public HttpClient HttpClient => _httpClient;

		public void Dispose()
		{
			// Dispose of the HttpClient when the test class instance is disposed
			_httpClient.Dispose();
		}
	}
}