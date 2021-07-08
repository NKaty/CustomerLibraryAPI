using CustomerLibraryAPI.BusinessEntities;
using System.Collections.Generic;

namespace CustomerLibraryAPI.WebApp.Models
{
    public class CustomersPageModel
    {
        public List<Customer> Customers { get; set; }

        public int TotalCount { get; set; }
    }
}
