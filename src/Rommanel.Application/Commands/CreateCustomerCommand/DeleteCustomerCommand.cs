using MediatR;

namespace Rommanel.Application.Commands.CreateCustomerCommand
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
