using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using CustomerLibraryAPI.WebApp.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.ControllersTests
{
    public class AddressesControllerTests
    {
        [Fact]
        public void ShouldGetAddressesByCustomerId()
        {
            var addressRepositoryMock = new Mock<IDependentRepository<Address>>();
            var address = new Address {CustomerId = 1};
            addressRepositoryMock.Setup(r => r.ReadByCustomerId(1)).Returns(new List<Address> {address});

            var controller = new AddressesController(addressRepositoryMock.Object);
            var addresses = controller.GetByCustomerId(1);

            Assert.NotNull(addresses);
            Assert.Single(addresses);
        }

        [Fact]
        public void ShouldGetAddress()
        {
            var addressRepositoryMock = new Mock<IDependentRepository<Address>>();
            var address = new Address {AddressId = 1};
            addressRepositoryMock.Setup(r => r.Read(1)).Returns(address);

            var controller = new AddressesController(addressRepositoryMock.Object);
            var data = controller.Get(1);

            Assert.NotNull(data);
            Assert.Equal(1, data.AddressId);
        }

        [Fact]
        public void ShouldCreateAddress()
        {
            var addressRepositoryMock = new Mock<IDependentRepository<Address>>();
            var address = new Address();
            addressRepositoryMock.Setup(r => r.Create(address)).Returns(1);


            var controller = new AddressesController(addressRepositoryMock.Object);
            controller.Post(address);

            addressRepositoryMock.Verify(r => r.Create(address), Times.Exactly(1));
        }

        [Fact]
        public void ShouldUpdateAddress()
        {
            var addressRepositoryMock = new Mock<IDependentRepository<Address>>();
            var address = new Address {AddressId = 1};
            addressRepositoryMock.Setup(r => r.Update(address));


            var controller = new AddressesController(addressRepositoryMock.Object);
            controller.Put(address);

            addressRepositoryMock.Verify(r => r.Update(address), Times.Exactly(1));
        }

        [Fact]
        public void ShouldDeleteAddress()
        {
            var addressRepositoryMock = new Mock<IDependentRepository<Address>>();
            var address = new Address { AddressId = 1 };
            addressRepositoryMock.Setup(r => r.Delete(1));


            var controller = new AddressesController(addressRepositoryMock.Object);
            controller.Delete(1);

            addressRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }
    }
}