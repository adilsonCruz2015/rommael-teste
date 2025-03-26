using FluentValidation;
using Rommanel.Application.Commands.CreateCustomerCommand;


namespace Rommanel.Application.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("Document is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth Date is required.")
                .Must(BeAtLeast18YearsOld).WithMessage("Customer must be at least 18 years old.");

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
        }

        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
