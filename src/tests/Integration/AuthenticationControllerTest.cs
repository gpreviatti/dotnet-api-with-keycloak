using System.Net;
using System.Text;
using Application.Messages;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Integration;

public class AuthenticationControllerTest : IntegrationTestBaseFixture
{
    private const string RESOURCE_URL = "authentication/";

    public AuthenticationControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact(DisplayName = nameof(Should_Get_With_Success))]
    public async Task Should_Get_With_Success()
    {
        _client.DefaultRequestHeaders.Clear();
        var request = new AuthenticationRequest
        {
            UserName = "giovanni",
            Password = "Change@Me"
        };
        var json = JsonConvert.SerializeObject(request);
        var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await _client.PostAsync(RESOURCE_URL + "login", contentRequest);
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<AuthenticationResponse>(content);


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(data!.AccessToken);
        Assert.NotEqual(0, data!.ExpiresIn);
        Assert.NotEqual(0, data!.RefreshExpiresIn);
        Assert.NotNull(data!.RefreshToken);
        Assert.Equal("Bearer", data!.TokenType);
        Assert.NotNull(data!.Scope);
    }

    [Fact(DisplayName = nameof(Should_Get_With_Bad_Request))]
    public async Task Should_Get_With_Bad_Request()
    {
        _client.DefaultRequestHeaders.Clear();
        var request = new AuthenticationRequest
        {
            UserName = "giovanni",
            Password = "NotChangeMe"
        };
        var json = JsonConvert.SerializeObject(request);
        var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await _client.PostAsync(RESOURCE_URL + "login", contentRequest);
        var content = await response.Content.ReadAsStringAsync();


        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("User not found", content);
    }
}