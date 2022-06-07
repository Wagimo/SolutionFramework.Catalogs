using FluentValidation;

namespace SolutionFramework.Application.Features.Activities.Command.Create
{
    public class CreateActivityValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("No puede estar nulo")
                .NotNull().WithMessage("No puede ser nulo")
                .MaximumLength(50).WithMessage("{No puede exceder los 50 caracteres}");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("No puede ser nulo")
                .NotNull().WithMessage("No puede ser nulo");
        }
    }
}
