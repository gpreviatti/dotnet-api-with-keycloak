using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Integration;

public class AdminControllerTest : IntegrationTestBaseFixture
{
    private const string RESOURCE_URL = "admin";

    public AdminControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact(DisplayName = nameof(Should_Get_With_Unauthorized))]
    public async Task Should_Get_With_Unauthorized()
    {
        _client.DefaultRequestHeaders.Clear();


        var response = await _client.GetAsync(RESOURCE_URL);


        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact(DisplayName = nameof(Should_Get_With_Success))]
    public async Task Should_Get_With_Success()
    {
        var authorization = await GetAuthorization("giovanni", "Change@Me");
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.AccessToken);


        var response = await _client.GetAsync(RESOURCE_URL);
        var content = await response.Content.ReadAsStringAsync();


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content.Contains("giovanni"));
    }

    [Fact(DisplayName = nameof(Should_Not_Get_With_Success))]
    public async Task Should_Not_Get_With_Success()
    {
        var authorization = await GetAuthorization("joao", "Change@Me");
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.AccessToken);


        var response = await _client.GetAsync(RESOURCE_URL);


        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}