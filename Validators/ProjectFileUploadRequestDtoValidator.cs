using FluentValidation;
using teamflow.API.Dtos.RequestDtos;

namespace teamflow.API.Validators
{
    public class ProjectFileUploadRequestDtoValidator
        : AbstractValidator<ProjectFileUploadRequestDto>
    {
        public ProjectFileUploadRequestDtoValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("El identificador de proyecto es obligatorio.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("El archivo es obligatorio.")
                .Must(f => f.Length > 0)
                .WithMessage("El archivo no puede estar vacío.");

            RuleFor(x => x.FileName)
                .MaximumLength(255)
                .WithMessage("El nombre de archivo debe tener como máximo 255 caracteres.");

            RuleFor(x => x.FileType)
                .MaximumLength(100)
                .WithMessage("El tipo de archivo debe tener como máximo 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("La descripción debe tener como máximo 500 caracteres.");
        }
    }
}
