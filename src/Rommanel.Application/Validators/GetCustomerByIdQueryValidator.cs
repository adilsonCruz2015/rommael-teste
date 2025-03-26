using FluentValidation;
using Rommanel.Application.Queries;

namespace Rommanel.Application.Validators
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Customer ID must be provided and cannot be empty.");
        }
    }
}
