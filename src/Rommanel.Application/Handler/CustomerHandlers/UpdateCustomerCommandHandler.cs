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
    public class UpdateCustomerCommandHandler : HandleService, IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository,
                                            Notifiy.INotification notification)
            : base(notification)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!RunValidation(new UpdateCustomerCommandValidator(), request))
                return null!;

            var customer = await _customerRepository.GetById(request.Id);

            if (customer == null)
            {
                Notify("Customer not found.", MessageType.Success, NotificationType.NotFound);
                return null!;
            }

            // Atualiza os dados do cliente
            customer.Update(
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
                request.TaxExempt
            );

            await _customerRepository.UpdateAsync(customer);
            Notify("Client updated successfully", MessageType.Success);

            return customer;
        }
    }
}
