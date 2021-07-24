using Microsoft.EntityFrameworkCore;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public static class CustomerLibraryContextProvider
    {
        private static CustomerLibraryContext _context;

        private static DbContextOptions<CustomerLibraryContext> _contextOptions =
            new DbContextOptionsBuilder<CustomerLibraryContext>()
                .UseSqlServer("Server=Desktop;Database=CustomersDB;Trusted_Connection=True;")
                .Options;


        public static CustomerLibraryContext Current
        {
            get {
                if (_context is null) {
                    _context = new CustomerLibraryContext(_contextOptions);
                    _context.Database.EnsureCreated();
                }

                return _context;
            }
            set => _context = value;
        }
    }
}