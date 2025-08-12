using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    //reglas de validacion
    public class CreateClienteCommandValidator:AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");

            RuleFor(p => p.FechaNacimiento)
                .NotEmpty().WithMessage("Fecha de nacimiento no puede ser vacioa.");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .Matches(@"^\d{10}$").WithMessage("{PropertyName} debe tener 10 dígitos numéricos")
                .MaximumLength(10).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida.")
                .MaximumLength(100).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");

            RuleFor(p => p.Direccion)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");
        }
    }
}
