using FluentValidation;
using HQ.Application.Dtos.Users.Requests;

namespace HQ.Application.UseCases.Users.Login;

public class LoginUserValidator : AbstractValidator<RequestLoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(user => user).Must(user =>
                !string.IsNullOrWhiteSpace(user.Email) || !string.IsNullOrWhiteSpace(user.Password))
            .WithMessage("Email ou senha em branco");
        RuleFor(user => user.Email).EmailAddress().WithMessage("Email ou senha invalidas !");
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6)
            .WithMessage("Email ou senha invalidas !");
    }
}