using CadeMeuPet.Application.Commands.Auth;
using CadeMeuPet.Application.Validators;
using FluentValidation;

namespace CadeMeuPet.Application.Validators.Auth;

public sealed class RegisterTutorCommandValidator : AbstractValidator<RegisterTutorCommand>
{
    public RegisterTutorCommandValidator()
    {
        RuleFor(command => command.Nome).NomePessoa();
        RuleFor(command => command.Email).Email();
        RuleFor(command => command.Telefone).Telefone();
        RuleFor(command => command.Senha).Senha();
    }
}
