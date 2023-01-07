using Newtonsoft.Json;

namespace Application.Messages;

public class AuthenticationRequest
{
    [JsonRequired]
    public string UserName { get; set; }

    [JsonRequired]
    public string Password { get; set; }
}