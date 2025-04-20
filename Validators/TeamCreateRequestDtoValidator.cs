using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class TeamCreateRequestDtoValidator : AbstractValidator<TeamCreateRequestDto>
    {
        public TeamCreateRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del equipo es obligatorio.")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres para el nombre.");
        }
    }
}
