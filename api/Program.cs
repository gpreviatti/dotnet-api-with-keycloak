using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;

var builder = WebApplication.CreateBuilder(args);

var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer().AddSwaggerGen();

services.AddKeycloakAuthentication(configuration);
services.AddAuthorization(o => o.AddPolicy("IsAdmin", b =>
{
    b.RequireRealmRoles("admin");
}));
services.AddKeycloakAuthorization(configuration);

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

#region Routes
app.MapGet("/health", () => "Healthy! :)");

app.MapGet("/admin", (ClaimsPrincipal user) =>
{
    return $"Authenticated: {user.Identity.IsAuthenticated}, User: {user.Identity!.Name}";
}).RequireAuthorization("IsAdmin");

app.MapGet("/", (ClaimsPrincipal user) =>
{
    return $"Authenticated: {user.Identity.IsAuthenticated}, User: {user.Identity!.Name}";
});

#endregion


app.Run();
