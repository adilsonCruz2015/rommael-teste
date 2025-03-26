

using MediatR;
using Rommanel.Application.Commands.CreateCustomerCommand;
using Rommanel.Application.Services.Common;
using Rommanel.Application.Validators;
using Rommanel.Core.Entities;
using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Application.Handler.CustomerHandlers
{
    public class CreateCustomerCommandHandler : HandleService, IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,
                                            Notifiy.INotification notification)
            : base(notification)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!RunValidation(new CreateCustomerCommandValidator(), request))
                return null!;

            var customerBd = await _customerRepository.GetCustomerByEmail(request.Document!, request.Email!);

            if(customerBd != null)
            {
                Notify("Customer already registered!", MessageType.Success);
                return customerBd;
            }

            var customer = new Customer(
            request.Name!,
            request.Document!,
            request.BirthDate,
            request.Phone!,
            request.Email!,
            new Address(
                request.ZipCode!,
                request.Street!,
                request.Number!,
                request.Neighborhood!,
                request.City!,
                request.State!
            ),
            request.StateRegistration,
            request.TaxExempt);

            await _customerRepository.AddAsync(customer);
            Notify("Customer registered successfully!", MessageType.Success);

            return customer;
        }
    }
}
