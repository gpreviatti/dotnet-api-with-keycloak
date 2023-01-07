using Infrastructure.Http.Contracts;
using Infrastructure.Http.Messages;
using Keycloak.AuthServices.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Http.Repositories;
public class KeycloakRepository : IKeycloakRepository
{
    private readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;

    public KeycloakRepository(IConfiguration configuration)
    {
        if (configuration != null)
        {
            _keycloakAuthenticationOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>()!;
        }
    }

    public async Task<KeycloakAuthenticationResponse> LoginAsync(string userName, string password)
    {
        var client = new RestClient();
        var restRequest = new RestRequest(
            $"{_keycloakAuthenticationOptions.AuthServerUrl}/realms/{_keycloakAuthenticationOptions.Realm}/protocol/openid-connect/token",
            Method.Post
        )
        { Timeout = -1 };

        restRequest.AddParameter("grant_type", "password");
        restRequest.AddParameter("client_id", _keycloakAuthenticationOptions.Resource);
        restRequest.AddParameter("username", userName);
        restRequest.AddParameter("password", password);
        restRequest.AddParameter("client_secret", _keycloakAuthenticationOptions.Credentials.Secret);

        var response = await client.ExecuteAsync(restRequest);

        if (!response.IsSuccessful || response.ContentLength.Equals(0) || response.Content == null)
            return null!;

        return JsonConvert.DeserializeObject<KeycloakAuthenticationResponse>(response.Content);
    }
}
