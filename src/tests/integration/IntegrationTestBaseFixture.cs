using Application.Messages;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Tests.Integration;

public abstract class IntegrationTestBaseFixture : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;
    protected readonly IConfiguration _configuration;
    protected readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;

    public IntegrationTestBaseFixture(WebApplicationFactory<Program> factory)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .Build();

        if (_configuration != null)
        {
            _keycloakAuthenticationOptions = _configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>()!;
        }

        _client = factory.CreateClient();
    }

    public async Task<AuthenticationResponse?> GetAuthorization(string username, string password)
    {
        var client = new RestClient();
        var request = new RestRequest(
            $"{_keycloakAuthenticationOptions.AuthServerUrl}/realms/{_keycloakAuthenticationOptions.Realm}/protocol/openid-connect/token",
            Method.Post
        );
        request.Timeout = -1;

        request.AddParameter("grant_type", "password");
        request.AddParameter("client_id", _keycloakAuthenticationOptions.Resource);
        request.AddParameter("username", username);
        request.AddParameter("password", password);
        request.AddParameter("client_secret", _keycloakAuthenticationOptions.Credentials.Secret);

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful || response.ContentLength.Equals(0) || response.Content == null)
            return null;

        return JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content);
    }
}