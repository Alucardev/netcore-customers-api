using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
    }
}


