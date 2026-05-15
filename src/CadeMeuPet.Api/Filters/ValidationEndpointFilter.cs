using FluentValidation;
using FluentValidation.Results;

namespace CadeMeuPet.Api.Filters;

public sealed class ValidationEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var failures = new List<ValidationFailure>();

        foreach (var argument in context.Arguments.Where(argument => argument is not null))
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(argument!.GetType());

            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
            {
                continue;
            }

            var validationContext = new ValidationContext<object>(argument);
            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            failures.AddRange(validationResult.Errors.Where(error => error is not null));
        }

        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }

        return await next(context);
    }
}
