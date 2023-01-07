using Application.Messages;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    protected readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;

    public AuthenticationController(IConfiguration configuration)
    {
        if (configuration != null)
        {
            _keycloakAuthenticationOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>()!;
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var client = new RestClient();
        var restRequest = new RestRequest(
            $"{_keycloakAuthenticationOptions.AuthServerUrl}/realms/{_keycloakAuthenticationOptions.Realm}/protocol/openid-connect/token",
            Method.Post
        ) { Timeout = -1 };

        restRequest.AddParameter("grant_type", "password");
        restRequest.AddParameter("client_id", _keycloakAuthenticationOptions.Resource);
        restRequest.AddParameter("username", request.UserName);
        restRequest.AddParameter("password", request.Password);
        restRequest.AddParameter("client_secret", _keycloakAuthenticationOptions.Credentials.Secret);

        var response = await client.ExecuteAsync(restRequest);

        if (!response.IsSuccessful || response.ContentLength.Equals(0) || response.Content == null)
            return BadRequest("User not found");

        var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content);

        return Ok(authenticationResponse);
    }
}