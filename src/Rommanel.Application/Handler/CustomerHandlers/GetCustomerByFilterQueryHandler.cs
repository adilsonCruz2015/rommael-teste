using MediatR;
using Rommanel.Application.Queries;
using Rommanel.Core.Entities;
using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;
using System.Linq.Expressions;
using Rommanel.Core.Helpers;
using Rommanel.Application.Services.Common;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Application.Handler.CustomerHandlers
{
    public class GetCustomerByFilterQueryHandler : HandleService, IRequestHandler<GetCustomerByFilterQuery, PagedResponse<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByFilterQueryHandler(ICustomerRepository customerRepository,
                                               Notifiy.INotification notification)
            : base(notification)
        {
            _customerRepository = customerRepository;
        }

        public async Task<PagedResponse<Customer>> Handle(GetCustomerByFilterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Customer, bool>> predicate = c => true;

            if (!string.IsNullOrEmpty(request.Name))
                predicate = predicate.Combine(c => c.Name != null && c.Name.Contains(request.Name));

            if (!string.IsNullOrEmpty(request.Document))
                predicate = predicate.Combine(c => c.Document != null && c.Document.Contains(request.Document));

            if (!string.IsNullOrEmpty(request.Email))
                predicate = predicate.Combine(c => c.Email != null && c.Email.Contains(request.Email));

            // Chamar o repositório para buscar os dados filtrados
            var result = await _customerRepository.Filter(request.PageNumber, request.PageSize, predicate);

            if (result.TotalRecords == 0)
                Notify("Customer not found.", MessageType.Success, NotificationType.NotFound);
            else
                Notify("Client(s) successfully found!", MessageType.Success);

            return result;
            
        }
    }
}
