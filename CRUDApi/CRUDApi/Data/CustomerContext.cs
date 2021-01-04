
using CRUDApi.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        { }
        public DbSet<Models.Customers.Customer> Customers { get; set; }
    }
}
