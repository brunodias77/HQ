using HQ.Application.Abstractions;
using HQ.Application.Dtos.Tokens;
using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Dtos.Users.Responses;
using HQ.Application.Exceptions;
using HQ.Domain.Repositories;
using HQ.Domain.Security.PasswordEncripter;
using HQ.Domain.Security.Token;

namespace HQ.Application.UseCases.Users.Login;

public class LoginUserUseCase : IUseCase<RequestLoginUser, ResponseLoginUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserUseCase(IUserRepository userRepository, IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseLoginUser> Execute(RequestLoginUser request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);
        if (user is null || !passwordMatch)
        {
            throw new InvalidLoginException("usuario ou senha invalidas");
        }

        var response = new ResponseLoginUser()
        {
            Name = user.Name,
            Token = new ResponseTokenJson()
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id)
            }
        };
        return response;
    }
}