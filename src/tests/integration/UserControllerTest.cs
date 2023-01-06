using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace tests.integration;

public class UserControllerTest : IntegrationTestBaseFixture
{
    private const string RESOURCE_URL = "user";

    public UserControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Get_With_Unauthorized()
    {
        _client.DefaultRequestHeaders.Clear();


        var response = await _client.GetAsync(RESOURCE_URL);


        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("giovanni", "Change@Me")]
    [InlineData("joao", "Change@Me")]
    public async Task Should_Get_With_Success(string userName, string password)
    {
        var authorization = await GetAuthorization(userName, password);
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization!.AccessToken);


        var response = await _client.GetAsync(RESOURCE_URL);
        var content = await response.Content.ReadAsStringAsync();


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(content.Contains(userName));
    }
}