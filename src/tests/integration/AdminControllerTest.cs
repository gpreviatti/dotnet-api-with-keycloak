using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace tests.integration;

public class AdminControllerTest : IntegrationTestBaseFixture
{
    public AdminControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Get_With_Unauthorized()
    {
        var response = await _client.GetAsync("admin");

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Get_With_Success()
    {
        var authorization = await GetAuthorization();
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.access_token);


        var response = await _client.GetAsync("admin");
        var content = await response.Content.ReadAsStringAsync();


        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.True(content.Contains("giovanni"));
    }
}