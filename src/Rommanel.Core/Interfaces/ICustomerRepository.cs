

using Rommanel.Core.Entities;
using Rommanel.Core.Interfaces.Common;

namespace Rommanel.Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomerByEmail(string document, string email);
    }
}
