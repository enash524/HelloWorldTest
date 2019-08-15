using System.Net.Http;
using System.Threading.Tasks;
using HelloWorldTest.Infrastructure.Messages.Queries.GetHelloWorld;
using HelloWorldTest.WebApi.Tests.Common;
using Xunit;

namespace HelloWorldTest.WebApi.Tests.Controllers.Messages
{
    public class GetHelloWorld : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetHelloWorld(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsString()
        {
            // Arrange
            HttpResponseMessage response = await _client.GetAsync("/api/messages/gethelloworld");

            // Act
            response.EnsureSuccessStatusCode();
            HelloWorldViewModel result = await Utilities.GetResponseContent<HelloWorldViewModel>(response);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.MessageText));
            Assert.Equal("Hello World", result.MessageText);
        }
    }
}
