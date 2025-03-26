using MediatR;
using Rommanel.Core.Entities;
using Rommanel.Core.ValueObject;

namespace Rommanel.Application.Queries
{
    public class GetCustomerByFilterQuery : IRequest<PagedResponse<Customer>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? Email { get; set; }
    }
}
