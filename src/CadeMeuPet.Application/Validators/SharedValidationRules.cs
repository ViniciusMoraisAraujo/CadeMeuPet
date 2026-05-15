using FluentValidation;

namespace CadeMeuPet.Application.Validators;

internal static class SharedValidationRules
{
    private const string PhonePattern = @"^\+?(?=(?:\D*\d){10,11}\D*$)[0-9 ()-]{10,20}$";

    internal static IRuleBuilderOptions<T, string?> NomePessoa<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos 2 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

    internal static IRuleBuilderOptions<T, string?> Email<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Informe um e-mail válido.")
            .MaximumLength(254).WithMessage("O e-mail deve ter no máximo 254 caracteres.");

    internal static IRuleBuilderOptions<T, string?> Telefone<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(PhonePattern).WithMessage("Informe um telefone válido com DDD.");

    internal static IRuleBuilderOptions<T, string?> Senha<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.");

    internal static IRuleBuilderOptions<T, string?> NomePet<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome do pet é obrigatório.")
            .MinimumLength(2).WithMessage("O nome do pet deve ter pelo menos 2 caracteres.")
            .MaximumLength(80).WithMessage("O nome do pet deve ter no máximo 80 caracteres.");

    internal static IRuleBuilderOptions<T, string?> Especie<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A espécie do pet é obrigatória.")
            .MinimumLength(2).WithMessage("A espécie deve ter pelo menos 2 caracteres.")
            .MaximumLength(50).WithMessage("A espécie deve ter no máximo 50 caracteres.");

    internal static IRuleBuilderOptions<T, string?> Raca<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A raça do pet é obrigatória.")
            .MaximumLength(80).WithMessage("A raça deve ter no máximo 80 caracteres.");

    internal static IRuleBuilderOptions<T, int?> IdadePet<T>(this IRuleBuilderInitial<T, int?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A idade do pet é obrigatória.")
            .Must(idade => idade is >= 0 and <= 30).WithMessage("A idade do pet deve estar entre 0 e 30 anos.");

    internal static IRuleBuilderOptions<T, string?> FotoUrl<T>(this IRuleBuilderInitial<T, string?> ruleBuilder) =>
        ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A foto do pet é obrigatória.")
            .MaximumLength(2048).WithMessage("A URL da foto deve ter no máximo 2048 caracteres.")
            .Must(BeAValidAbsoluteUrl).WithMessage("Informe uma URL absoluta válida para a foto do pet.");

    internal static IRuleBuilderOptions<T, string?> Observacoes<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder
            .MaximumLength(500).WithMessage("As observações devem ter no máximo 500 caracteres.");

    private static bool BeAValidAbsoluteUrl(string? value) =>
        Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
        (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
