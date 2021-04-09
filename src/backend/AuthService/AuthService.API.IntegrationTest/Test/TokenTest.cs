using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AuthService.API.IntegrationTest.Test
{
    public class TokenTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;

        public TokenTests(TestFixture<Startup> fixture)
        {
            _client = fixture._client;
        }

        [Fact]
        public async Task TestTokenRefreshAsync()
        {
            // Arrange
            var uri = "/api/token/refresh";
            var token = "";

            var jsonRequest = JObject.FromObject(new
            {
                OldToken = token
            });

            var content = new StringContent(jsonRequest.ToString(), Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.PostAsync(uri, content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}