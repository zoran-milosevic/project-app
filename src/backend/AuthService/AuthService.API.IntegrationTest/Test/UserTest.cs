using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AuthService.API.IntegrationTest.Test
{
    public class UserTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;

        public UserTest(TestFixture<Startup> fixture)
        {
            _client = fixture._client;
        }

        [Fact]
        public async Task TestUserRegistrationAsync()
        {
            // Arrange
            var uri = "/api/user/register";

            var jsonRequest = JObject.FromObject(new
            {
                email = "john.doe@test.test",
                firstName = "John",
                lastName = "Doe",
                password = "P@ssw0rd",
                confirmPassword = "P@ssw0rd"
            });

            var content = new StringContent(jsonRequest.ToString(), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(uri, content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestTokenGenerateAsync()
        {
            // Arrange
            var uri = "/api/token/generate";

            var jsonRequest = JObject.FromObject(new
            {
                email = "john.doe@test.test",
                password = "P@ssw0rd"
            });

            var content = new StringContent(jsonRequest.ToString(), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                var token = json["accessToken"];
            }

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetUserProfileAsync()
        {
            // Arrange
            var token = string.Empty;
            var uri1 = "/api/token/generate";

            var jsonRequest = JObject.FromObject(new
            {
                email = "john.doe@test.test",
                password = "P@ssw0rd"
            });

            var content = new StringContent(jsonRequest.ToString(), Encoding.UTF8, "application/json");

            // Act
            var response1 = await _client.PostAsync(uri1, content);

            if (response1.IsSuccessStatusCode)
            {
                var jsonResponse = await response1.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                token = json["accessToken"];
            }

            var uri2 = "/api/user/profile";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

            // Act
            var response2 = await _client.GetAsync(uri2);

            if (response2.IsSuccessStatusCode)
            {
                var jsonResponse = await response2.Content.ReadAsStringAsync();
            }

            // Assert
            response2.EnsureSuccessStatusCode();
        }
    }
}
