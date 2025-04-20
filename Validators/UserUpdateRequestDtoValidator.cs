using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class UserUpdateRequestDtoValidator : AbstractValidator<UserUpdateRequestDto>
    {
        public UserUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres para el usuario.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Formato de email inválido.")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres para el email.");

            When(x => !string.IsNullOrWhiteSpace(x.NewPassword), () => {
                RuleFor(x => x.NewPassword)
                    .MinimumLength(6).WithMessage("Mínimo 6 caracteres para la nueva contraseña.");
            });
        }
    }
}
