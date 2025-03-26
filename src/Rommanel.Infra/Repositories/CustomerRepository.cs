using Microsoft.EntityFrameworkCore;
using Rommanel.Core.Entities;
using Rommanel.Core.Interfaces;
using Rommanel.Infra.Repositories.Common;

namespace Rommanel.Infra.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContextClass dbContext) : base(dbContext)
        { }

        public async Task<Customer> GetCustomerByEmail(string document, string email)
        {
            var query = Query();

            return await query.FirstOrDefaultAsync(x => x.Document == document && x.Email == email);
        }
    }
}
