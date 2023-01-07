using Api.Extensions;
using Infrastructure.Http.Contracts;
using Infrastructure.Http.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

services.AddScoped<IKeycloakRepository, KeycloakRepository>();

services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddKeycloakAuthenticationExtension(configuration);

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
