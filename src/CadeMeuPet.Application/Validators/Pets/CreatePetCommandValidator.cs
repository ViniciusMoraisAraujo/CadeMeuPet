using CadeMeuPet.Application.Commands.Pets;
using CadeMeuPet.Application.Validators;
using FluentValidation;

namespace CadeMeuPet.Application.Validators.Pets;

public sealed class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(command => command.TutorId)
            .NotEmpty().WithMessage("O tutor do pet é obrigatório.");

        RuleFor(command => command.Nome).NomePet();
        RuleFor(command => command.Especie).Especie();
        RuleFor(command => command.Raca).Raca();
        RuleFor(command => command.Idade).IdadePet();
        RuleFor(command => command.FotoUrl).FotoUrl();
        RuleFor(command => command.Observacoes).Observacoes();
    }
}
