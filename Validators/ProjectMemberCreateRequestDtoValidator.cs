using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class ProjectMemberCreateRequestDtoValidator
        : AbstractValidator<ProjectMemberCreateRequestDto>
    {
        public ProjectMemberCreateRequestDtoValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("El identificador de proyecto es obligatorio.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("El identificador de usuario es obligatorio.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .MaximumLength(50).WithMessage("El rol debe tener como máximo 50 caracteres.");
        }
    }
}
