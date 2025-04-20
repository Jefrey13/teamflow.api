using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class UserLoginRequestDtoValidator: AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginRequestDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El usuario es obligatorio.")
                .MaximumLength(6).WithMessage("Máximo 50 caracteres para el usuario.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("Mínimo 6 caracteres para la contraseña.");
        }
    }
}
