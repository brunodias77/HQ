using HQ.Domain.Security.Token;

namespace HQ.Infra.Security.Token;

public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeInMinutes;
    private readonly string _signingKey;
    public JwtTokenGenerator(uint expirationTimeInMinutes, string signingKey)
    {
        _expirationTimeInMinutes = expirationTimeInMinutes;
        _signingKey = signingKey;
    }
    public string Generate(Guid userId)
    {
        throw new NotImplementedException();
    }
}