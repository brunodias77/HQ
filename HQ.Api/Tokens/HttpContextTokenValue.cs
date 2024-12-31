using HQ.Domain.Security.Token;

namespace HQ.Api.Tokens;

public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string Value()
    {
        // Obtém o valor do header Authorization
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        // Remove o prefixo "Bearer " e retorna o token
        return authentication["Bearer ".Length..].ToString();
    }
}