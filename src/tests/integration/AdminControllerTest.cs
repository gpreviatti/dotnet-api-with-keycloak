using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace tests.integration;

public class AdminControllerTest : IntegrationTestBaseFixture
{
    private const string RESOURCE_URL = "admin";

    public AdminControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Get_With_Unauthorized()
    {
        _client.DefaultRequestHeaders.Clear();


        var response = await _client.GetAsync(RESOURCE_URL);


        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Get_With_Success()
    {
        var authorization = await GetAuthorization("giovanni", "Change@Me");
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.AccessToken);


        var response = await _client.GetAsync(RESOURCE_URL);
        var content = await response.Content.ReadAsStringAsync();


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content.Contains("giovanni"));
    }

    [Fact]
    public async Task Should_Not_Get_With_Success()
    {
        var authorization = await GetAuthorization("joao", "Change@Me");
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.AccessToken);


        var response = await _client.GetAsync(RESOURCE_URL);


        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}