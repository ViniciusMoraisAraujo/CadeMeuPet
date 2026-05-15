using CadeMeuPet.Application.Requests.Auth;
using CadeMeuPet.Application.Validators;
using FluentValidation;

namespace CadeMeuPet.Application.Validators.Auth;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
