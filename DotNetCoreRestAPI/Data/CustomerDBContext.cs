using DotNetCoreRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreRestAPI.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> option) : base(option)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
