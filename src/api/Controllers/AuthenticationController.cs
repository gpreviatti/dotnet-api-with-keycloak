using Application.Messages;
using Infrastructure.Http.Contracts;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    protected readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
    private readonly IKeycloakRepository _keycloakRepository;

    public AuthenticationController(IKeycloakRepository keycloakRepository)
    {
        _keycloakRepository = keycloakRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var response = await _keycloakRepository.LoginAsync(request.UserName, request.Password);

        if (response is null)
            return BadRequest("User not found");

        return Ok(new AuthenticationResponse
        {
            AccessToken = response.access_token,
            ExpiresIn= response.expires_in,
            NotBeforePolicy = response.notbeforepolicy,
            RefreshExpiresIn= response.refresh_expires_in,
            RefreshToken= response.refresh_token,
            Scope= response.scope,
            SessionState= response.session_state,
            TokenType = response.token_type
        });
    }
}