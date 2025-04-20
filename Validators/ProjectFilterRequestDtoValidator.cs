using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class ProjectFilterRequestDtoValidator : AbstractValidator<ProjectFilterRequestDto>
    {
        public ProjectFilterRequestDtoValidator()
        {
            RuleFor(x => x.Status)
                .Must(s => s == null || new[] { "Active", "Paused", "Delayed", "Scheduled", "Cancelled", "All" }
                    .Contains(s!))
                .WithMessage("Estado inválido.");

            When(x => x.StartDateFrom.HasValue && x.EndDateTo.HasValue, () => {
                RuleFor(x => x.EndDateTo)
                    .GreaterThan(x => x.StartDateFrom.Value)
                    .WithMessage("La fecha final debe ser posterior a la fecha inicial.");
            });
        }
    }
}
