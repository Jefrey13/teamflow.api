using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class ProjectUpdateRequestDtoValidator : AbstractValidator<ProjectUpdateRequestDto>
    {
        private static readonly string[] AllowedStatuses =
            { "Active", "Paused", "Delayed", "Scheduled", "Cancelled", "Completed" };

        public ProjectUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título del proyecto es obligatorio.")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres para el título.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.");

            When(x => x.EndDate.HasValue, () => {
                RuleFor(x => x.EndDate.Value)
                    .GreaterThan(x => x.StartDate)
                    .WithMessage("La fecha de fin debe ser posterior a la de inicio.");
            });

            RuleFor(x => x.Budget)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Budget.HasValue)
                .WithMessage("El presupuesto debe ser un número positivo.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .Must(s => AllowedStatuses.Contains(s!))
                .WithMessage("Estado inválido.");
        }
    }
}
