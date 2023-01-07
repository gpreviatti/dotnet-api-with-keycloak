using Infrastructure.Http.Messages;

namespace Infrastructure.Http.Contracts;
public interface IKeycloakRepository
{
    public Task<KeycloakAuthenticationResponse> LoginAsync(string userName, string password);
}
