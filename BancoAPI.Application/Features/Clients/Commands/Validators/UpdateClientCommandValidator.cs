using FluentValidation;

namespace BancoAPI.Application.Features.Clients.Commands.Validators
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} --> El Nombre no puede ser vacío")
                .MaximumLength(80).WithMessage("{PropertyName} No se debe exceder los {MaxLength} caracteres.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} --> El Apellido no puede ser vacío")
                .MaximumLength(80).WithMessage("{PropertyName} No se debe exceder los {MaxLength} caracteres.");
            RuleFor(p => p.Birthdate).NotEmpty().WithMessage("{PropertyName} --> no puede ser vacío");
            RuleFor(p => p.PhoneNumber)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir el formato 0000-0000 (Update)")
               .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} --> no puede ser vacío")
                .EmailAddress().WithMessage("debe ser una dirección de Email válida")
                .MaximumLength(100).WithMessage("{PropertyName} No se debe exceder los {MaxLength} caracteres.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("{PropertyName} --> no puede ser vacío")
                .MaximumLength(120).WithMessage("{PropertyName} No se debe exceder los {MaxLength} caracteres.");
        }
    }
}