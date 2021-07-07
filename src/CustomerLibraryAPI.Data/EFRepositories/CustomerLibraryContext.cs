using CustomerLibraryAPI.BusinessEntities;
using Microsoft.EntityFrameworkCore;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public class CustomerLibraryContext : DbContext
    {
        public CustomerLibraryContext(DbContextOptions<CustomerLibraryContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Note> Notes { get; set; }
    }
}