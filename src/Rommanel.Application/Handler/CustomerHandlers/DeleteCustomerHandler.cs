using MediatR;
using Rommanel.Application.Commands.CreateCustomerCommand;
using Rommanel.Application.Services.Common;
using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Application.Handler.CustomerHandlers
{
    public class DeleteStudentHandler : HandleService, IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteStudentHandler(ICustomerRepository customerRepository,
                                    Notifiy.INotification notification)
            : base(notification)

        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.Id);

            if (customer == null)
            {
                Notify("Customer not found.", MessageType.Success, NotificationType.NotFound);
                return 0;
            }

            int result = await _customerRepository.DeleteAsync(customer);

            if (result > 0)
                Notify("Client successfully deleted.", MessageType.Success);
            else
                Notify("Unable to delete Client.", MessageType.Success, NotificationType.BadRequest);


            return result;
        }
    }
}
