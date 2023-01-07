using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

namespace Api.Extensions;

public static class KeycloakExtensions
{
    public static IServiceCollection AddKeycloakAuthenticationExtension(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddKeycloakAuthentication(configuration);
        services.AddAuthorization(
            o => o.AddPolicy("IsAdmin", b => b.RequireRealmRoles(GetRoles()))
        );
        services.AddKeycloakAuthorization(configuration);

        return services;
    }

    // Insert your realm roles here
    public static string[] GetRoles() => new string[] { 
        "admin"
    };
}