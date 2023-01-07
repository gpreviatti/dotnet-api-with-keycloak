using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Infrastructure.Http.Messages;
[Serializable]
public class KeycloakAuthenticationResponse
{
    [JsonProperty("access_token")]
    [DataMember]
    public string? access_token { get; set; }

    [JsonProperty("expires_in")]
    [DataMember]
    public int expires_in { get; set; }

    [JsonProperty("refresh_expires_in")]
    [DataMember]
    public int refresh_expires_in { get; set; }

    [JsonProperty("refresh_token")]
    [DataMember]
    public string? refresh_token { get; set; }

    [JsonProperty("token_type")]
    [DataMember]
    public string? token_type { get; set; }

    [JsonProperty("notbeforepolicy")]
    [DataMember]
    public int notbeforepolicy { get; set; }

    [JsonProperty("session_state")]
    [DataMember]
    public string? session_state { get; set; }

    [JsonProperty("scope")]
    [DataMember]
    public string? scope { get; set; }
}
