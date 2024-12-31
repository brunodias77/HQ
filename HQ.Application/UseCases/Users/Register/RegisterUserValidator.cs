using FluentValidation;
using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Exceptions;
using HQ.Application.Exceptions;

namespace HQ.Application.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        // Regra para garantir que o campo "Name" não seja vazio.
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("O campo nome esta vazio !"); // Mensagem de erro caso o nome esteja vazio.

        // Regra para garantir que o campo "Email" não seja vazio.
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O campo e-mail esta vazio !"); // Mensagem de erro caso o email esteja vazio.

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("A senha deve ter no minimo 6 caracteres !");

        // Regra condicional para validar o formato do email se o campo não estiver vazio.
        When(user => !string.IsNullOrEmpty(user.Email), // Verifica se o email não está vazio.
            () =>
            {
                RuleFor(user => user.Email)
                    .EmailAddress() // Valida se o email tem um formato válido.
                    .WithMessage("O e-mail esta no formato invalido !"); // Mensagem de erro caso o email seja inválido.
            });
    }
}