using CustomerLibraryAPI.Data.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CustomerLibraryAPI.IntegrationTests.EFRepositoryTests
{
    public class CustomerLibraryContextProviderTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerLibraryContextProvider()
        {
            var context = CustomerLibraryContextProvider.Current;
            Assert.NotNull(context);
        }

        [Fact]
        public void ShouldBeAbleToPassContextIntoCustomerLibraryContextProvider()
        {
            var context = new CustomerLibraryContext(new DbContextOptions<CustomerLibraryContext>());
            CustomerLibraryContextProvider.Current = context;
            Assert.NotNull(CustomerLibraryContextProvider.Current);
        }
    }
}
