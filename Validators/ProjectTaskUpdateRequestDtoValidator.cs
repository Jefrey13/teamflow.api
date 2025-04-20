using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class ProjectTaskUpdateRequestDtoValidator : AbstractValidator<ProjectTaskUpdateRequestDto>
    {
        private static readonly string[] AllowedPriorities = { "Low", "Normal", "High", "Critical" };
        private static readonly string[] AllowedStatuses = { "Pending", "In Progress", "Completed", "Cancelled" };

        public ProjectTaskUpdateRequestDtoValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("El identificador de la tarea es obligatorio.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título de la tarea es obligatorio.")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres para el título.");

            When(x => x.DueDate.HasValue, () => {
                RuleFor(x => x.DueDate.Value)
                    .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
                    .WithMessage("La fecha de vencimiento debe ser en el futuro.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Priority), () => {
                RuleFor(x => x.Priority)
                    .Must(p => AllowedPriorities.Contains(p!))
                    .WithMessage("Prioridad inválida.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Status), () => {
                RuleFor(x => x.Status)
                    .Must(s => AllowedStatuses.Contains(s!))
                    .WithMessage("Estado inválido.");
            });
        }
    }
}
