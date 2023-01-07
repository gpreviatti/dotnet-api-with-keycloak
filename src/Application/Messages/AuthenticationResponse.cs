using System.Runtime.Serialization;

namespace Application.Messages;

[Serializable]
public class AuthenticationResponse
{
    [DataMember]
    public string? AccessToken { get; set; }

    [DataMember]
    public int ExpiresIn { get; set; }

    [DataMember]
    public int RefreshExpiresIn { get; set; }

    [DataMember]
    public string? RefreshToken { get; set; }

    [DataMember]
    public string? TokenType { get; set; }

    [DataMember]
    public int NotBeforePolicy { get; set; }

    [DataMember]
    public string? SessionState { get; set; }

    [DataMember]
    public string? Scope { get; set; }
}