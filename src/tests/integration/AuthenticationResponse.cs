using Newtonsoft.Json;

namespace tests.integration;

public class AuthenticationResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("refresh_expires_in")]
    public int RefreshExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    [JsonProperty("notbeforepolicy")]
    public int NotBeforePolicy { get; set; }

    [JsonProperty("session_state")]
    public string? SessionState { get; set; }

    [JsonProperty("scope")]
    public string? Scope { get; set; }
}