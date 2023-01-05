using api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer().AddSwaggerGen();

services.AddKeycloakAuthenticationExtension(configuration);

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
