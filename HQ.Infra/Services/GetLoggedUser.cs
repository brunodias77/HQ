using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HQ.Domain.Entities;
using HQ.Domain.Security.Token;
using HQ.Domain.Services;
using HQ.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace HQ.Infra.Services;

public class GetLoggedUser(ApplicationDbContext dbContext, ITokenProvider token) : IGetLoggedUser
{
    public async Task<User> User()
    {
        // Obtém o valor do token atual.
        var token1 = token.Value();
        // Cria um manipulador para o token JWT.
        var tokenHandler = new JwtSecurityTokenHandler();
        // Lê e valida o token JWT.
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token1);
        // Obtém o valor do claim de identificação (SID) do token JWT.
        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        // Converte o identificador do usuário para um Guid.
        var userIdentifier = Guid.Parse(identifier);
        // Busca o usuário no banco de dados que está ativo e corresponde ao identificador obtido do token.
        return await dbContext.Users
            .AsNoTracking()
            .FirstAsync(user =>
                user.Active &&
                user.Id ==
                userIdentifier);
    }
}