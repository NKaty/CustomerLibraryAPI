using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.WebApp.Models;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.ModelsTests
{
    public class CustomersPageModelTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomersPageModel()
        {
            var customersPageModel = new CustomersPageModel
            {
                Customers = new List<Customer>(),
                TotalCount = 10
            };

            Assert.NotNull(customersPageModel);
        }
    }
}
