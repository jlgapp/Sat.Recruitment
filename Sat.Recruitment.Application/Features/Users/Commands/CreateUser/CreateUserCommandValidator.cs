using FluentValidation;

namespace Sat.Recruitment.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} The name is required")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{Email} The email is required")
                .NotNull();

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{Address} The address is required")
                .NotNull();

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{Phone} The phone is required")
                .NotNull();
            

            /*RuleFor(p => p.Estado)
                .NotEmpty().WithMessage("{Estado} no puede estar en blanco")
                .MaximumLength(3).WithMessage("{Estado} no puede exceder 3 caracteres")
                .Must(estado => EstadosGenerales.EstadosGeneralesActivoInactivo().Contains(estado))
                .WithMessage("{Estado} debe estar entre lo establecido para la empresa")
                ;*/
        }
    }
}
