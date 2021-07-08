using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using CustomerLibraryAPI.WebApp.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.ControllersTests
{
    public class CustomersControllerTests
    {
        [Fact]
        public void ShouldGetCustomersPage()
        {
            var customerRepositoryMock = new Mock<IMainRepository<Customer>>();
            var customer = new Customer();
            customerRepositoryMock.Setup(r => r.ReadPage(0, 20)).Returns((new List<Customer> {customer}, 1));

            var controller = new CustomersController(customerRepositoryMock.Object);
            var customersPageModel = controller.Get(0, 20);

            Assert.NotNull(customersPageModel);
            Assert.Equal(1, customersPageModel.TotalCount);
            Assert.Equal(customer, customersPageModel.Customers[0]);
        }

        [Fact]
        public void ShouldGetCustomer()
        {
            var customerRepositoryMock = new Mock<IMainRepository<Customer>>();
            var customer = new Customer {CustomerId = 1};
            customerRepositoryMock.Setup(r => r.Read(1)).Returns(customer);


            var controller = new CustomersController(customerRepositoryMock.Object);
            var data = controller.Get(1);

            Assert.NotNull(data);
            Assert.Equal(1, data.CustomerId);
        }

        [Fact]
        public void ShouldCreateCustomer()
        {
            var customerRepositoryMock = new Mock<IMainRepository<Customer>>();
            var customer = new Customer();
            customerRepositoryMock.Setup(r => r.Create(customer)).Returns(1);


            var controller = new CustomersController(customerRepositoryMock.Object);
            controller.Post(customer);

            customerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
        }

        [Fact]
        public void ShouldUpdateCustomer()
        {
            var customerRepositoryMock = new Mock<IMainRepository<Customer>>();
            var customer = new Customer { CustomerId = 1 };
            customerRepositoryMock.Setup(r => r.Update(customer));


            var controller = new CustomersController(customerRepositoryMock.Object);
            controller.Put(customer);

            customerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(1));
        }

        [Fact]
        public void ShouldDeleteCustomer()
        {
            var customerRepositoryMock = new Mock<IMainRepository<Customer>>();
            var customer = new Customer { CustomerId = 1 };
            customerRepositoryMock.Setup(r => r.Delete(1));


            var controller = new CustomersController(customerRepositoryMock.Object);
            controller.Delete(1);

            customerRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }
    }
}
