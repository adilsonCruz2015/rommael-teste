using MediatR;
using Rommanel.Application.Queries;
using Rommanel.Application.Services.Common;
using Rommanel.Application.Validators;
using Rommanel.Core.Entities;
using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Application.Handler.CustomerHandlers
{
    public class GetCustomerByIdHandler : HandleService, IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository,
                                      Notifiy.INotification notification)
            : base(notification) 
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            if (!RunValidation(new GetCustomerByIdQueryValidator(), request))
                return null!;

            var customer = await _customerRepository.GetById(request.Id);

            if(customer == null!)
            {
                Notify("Customer not found.", MessageType.Success, NotificationType.NotFound);
                return null!;
            }

            return customer;
        }
    }
}
