using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

var builder = WebApplication.CreateBuilder(args);

var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer().AddSwaggerGen();

host.ConfigureKeycloakConfigurationSource();
// conventional registration from keycloak.json
services.AddKeycloakAuthentication(configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization(configuration);

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (ClaimsPrincipal user) => app.Logger.LogInformation(user.Identity!.Name))
   .RequireAuthorization();

app.Run();
