

using MediatR;
using Rommanel.Core.Entities;

namespace Rommanel.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
