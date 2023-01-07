using Application.Messages;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Tests.Integration;

public abstract class IntegrationTestBaseFixture : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;
    protected readonly IConfiguration _configuration;

    public IntegrationTestBaseFixture(WebApplicationFactory<Program> factory)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .Build();

        _client = factory.CreateClient();
    }

    public async Task<AuthenticationResponse?> GetAuthorization(string username, string password)
    {
        _client.DefaultRequestHeaders.Clear();
        var request = new AuthenticationRequest
        {
            UserName = username,
            Password = password
        };
        var json = JsonConvert.SerializeObject(request);
        var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("authentication/login", contentRequest);

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<AuthenticationResponse>(content);
    }
}