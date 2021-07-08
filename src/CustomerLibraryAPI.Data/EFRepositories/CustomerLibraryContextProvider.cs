using Microsoft.EntityFrameworkCore;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public static class CustomerLibraryContextProvider
    {
        private static CustomerLibraryContext _context;

        private static DbContextOptions<CustomerLibraryContext> _contextOptions =
            new DbContextOptionsBuilder<CustomerLibraryContext>()
                .UseSqlServer("Server=Desktop;Database=CustomersTests;Trusted_Connection=True;")
                .Options;


        public static CustomerLibraryContext Current
        {
            get => _context = _context ?? new CustomerLibraryContext(_contextOptions);
            set => _context = value;
        }
    }
}