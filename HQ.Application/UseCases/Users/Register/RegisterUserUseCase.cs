using AutoMapper;
using FluentValidation;
using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Dtos.Users.Responses;
using HQ.Application.Exceptions;
using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Domain.Security.PasswordEncripter;
using HQ.Domain.Security.Token;

namespace HQ.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    public RegisterUserUseCase(IUserRepository userRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
    }

    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IMapper _mapper;

    
    public async  Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        var user = _mapper.Map<User>(request);
        throw new NotImplementedException();
        
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(request);
        if (_userRepository == null)
        {
            throw new InvalidOperationException("O UserRepository não foi inicializado.");
        }
        var emailExists = await _userRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                "Esse e-mail ja esta registrado !"));
        }
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMessages);
        }
    }
    
}