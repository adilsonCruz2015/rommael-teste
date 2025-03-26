using FluentValidation;
using Rommanel.Application.Commands.CreateCustomerCommand;

namespace Rommanel.Application.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("Document is required.")
                .Must(BeValidDocument).WithMessage("Invalid CPF or CNPJ.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth Date is required.")
                .Must(BeAtLeast18YearsOld).WithMessage("Customer must be at least 18 years old.")
                .When(x => IsCpf(x.Document)); // Aplica só se for CPF (pessoa física)

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip Code is required.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.");

            RuleFor(x => x.Neighborhood)
                .NotEmpty().WithMessage("Neighborhood is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.");

            // Para pessoa jurídica, valida a Inscrição Estadual ou Isenção
            RuleFor(x => x.StateRegistration)
                .NotEmpty()
                .When(x => IsCnpj(x.Document) && !x.TaxExempt) // Se for CNPJ e não isento
                .WithMessage("State Registration is required for companies.");

            RuleFor(x => x.TaxExempt)
                .Equal(true)
                .When(x => IsCnpj(x.Document) && string.IsNullOrEmpty(x.StateRegistration))  // Se não tiver IE, tem que ser isento
                .WithMessage("If the company does not provide a State Registration, tax exemption must be true.");
        }

        // Valida se o CPF ou CNPJ é válido
        private bool BeValidDocument(string document)
        {
            return IsCpf(document) || IsCnpj(document);
        }

        // Verifica se é CPF
        private bool IsCpf(string document)
        {
            return document.Length == 11;
        }

        // Verifica se é CNPJ
        private bool IsCnpj(string document)
        {
            return document.Length == 14;
        }

        // Verifica se a pessoa tem 18 anos ou mais
        private static bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
