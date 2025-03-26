using Microsoft.EntityFrameworkCore;
using Rommanel.Core.Entities;
using System.Reflection;


namespace Rommanel.Infra
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
