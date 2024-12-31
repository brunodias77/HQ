using HQ.Application.Dtos;
using HQ.Application.Exceptions;
using HQ.Domain.Repositories;
using HQ.Domain.Security.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace HQ.Api.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserRepository userRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userRepository = userRepository;
    }

    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserRepository _userRepository;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
            var exist = await _userRepository.ExistActiveUserWithIdentifier(userIdentifier);

            if (!exist)
                throw new UnauthorizedException("Usuário não tem permissão para acessar esse recurso.");
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token expirado !")
            {
                TokenExpired = true
            });
        }
        catch (ExceptionBase ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result =
                new UnauthorizedObjectResult(
                    new ResponseErrorJson("Usuário não tem permissão para acessar esse recurso."));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
            throw new UnauthorizedException("Voce nao tem permissao para acessar essa pagina");

        return authentication["Bearer ".Length..].Trim();
    }
}