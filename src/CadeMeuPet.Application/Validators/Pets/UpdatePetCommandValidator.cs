using CadeMeuPet.Application.Commands.Pets;
using CadeMeuPet.Application.Validators;
using FluentValidation;

namespace CadeMeuPet.Application.Validators.Pets;

public sealed class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(command => command.PetId)
            .NotEmpty().WithMessage("O pet é obrigatório.");

        RuleFor(command => command)
            .Must(HaveAtLeastOneFieldToUpdate)
            .WithMessage("Informe ao menos um campo para atualização do pet.");

        RuleFor(command => command.Nome)
            .NomePet()
            .When(command => command.Nome is not null);

        RuleFor(command => command.Especie)
            .Especie()
            .When(command => command.Especie is not null);

        RuleFor(command => command.Raca)
            .Raca()
            .When(command => command.Raca is not null);

        RuleFor(command => command.Idade)
            .IdadePet()
            .When(command => command.Idade is not null);

        RuleFor(command => command.FotoUrl)
            .FotoUrl()
            .When(command => command.FotoUrl is not null);

        RuleFor(command => command.Observacoes)
            .Observacoes()
            .When(command => command.Observacoes is not null);
    }

    private static bool HaveAtLeastOneFieldToUpdate(UpdatePetCommand command) =>
        command.Nome is not null ||
        command.Especie is not null ||
        command.Raca is not null ||
        command.Idade is not null ||
        command.FotoUrl is not null ||
        command.Observacoes is not null;
}
