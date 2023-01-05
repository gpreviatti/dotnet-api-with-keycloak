using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;

namespace tests.integration
{
    public abstract class IntegrationTestBaseFixture : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBaseFixture(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        public async Task<AuthorizationResponse?> GetAuthorization()
        {
            var client = new RestClient();
            var request = new RestRequest(
                "http://localhost:8080/realms/hello-world-authz/protocol/openid-connect/token",
                Method.Post
            );
            request.Timeout = -1;

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("client_id", "weather-api");
            request.AddParameter("username", "giovanni");
            request.AddParameter("password", "Z4jeakbm");
            request.AddParameter("client_secret", "Wx3pjZ3U71oiTu6AE1TLC4Fx0wVqsPvN");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful || response.ContentLength.Equals(0) || response.Content == null)
                return null;

            return JsonSerializer.Deserialize<AuthorizationResponse>(response.Content);
        }
    }
}