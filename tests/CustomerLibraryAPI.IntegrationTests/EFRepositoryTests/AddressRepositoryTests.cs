﻿using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Common;
using CustomerLibraryAPI.Data.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CustomerLibraryAPI.IntegrationTests.EFRepositoryTests
{
    public class AddressRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            var addressRepository = new AddressRepository();
            Assert.NotNull(addressRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddressRepositoryWithContext()
        {
            var context = new CustomerLibraryContext(new DbContextOptions<CustomerLibraryContext>());
            var addressRepository = new AddressRepository(context);
            Assert.NotNull(addressRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var fixture = new AddressRepositoryFixture();
            var mockAddressId = fixture.CreateMockAddress();
            Assert.NotEqual(0, mockAddressId);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressRepository.Read(addressId);

            Assert.NotNull(createdAddress);
            Assert.Equal(fixture.MockAddress.AddressId, createdAddress.AddressId);
            Assert.Equal(fixture.MockAddress.CustomerId, createdAddress.CustomerId);
            Assert.Equal(fixture.MockAddress.AddressLine, createdAddress.AddressLine);
            Assert.Equal(fixture.MockAddress.AddressLine2, createdAddress.AddressLine2);
            Assert.Equal(fixture.MockAddress.AddressType, createdAddress.AddressType);
            Assert.Equal(fixture.MockAddress.Country, createdAddress.Country);
            Assert.Equal(fixture.MockAddress.City, createdAddress.City);
            Assert.Equal(fixture.MockAddress.State, createdAddress.State);
            Assert.Equal(fixture.MockAddress.PostalCode, createdAddress.PostalCode);
        }

        [Fact]
        public void ShouldBeAbleToReadAddressesByCustomerId()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var customerId = fixture.CreateMockAddresses();
            var createdAddresses = addressRepository.ReadByCustomerId(customerId);

            Assert.Equal(2, createdAddresses.Count);
            Assert.Equal(customerId, createdAddresses[0].CustomerId);
            Assert.Equal(customerId, createdAddresses[1].CustomerId);
        }

        [Fact]
        public void ShouldBeAbleToCountAddressesByCustomerId()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var customerId = fixture.CreateMockAddresses();
            var count = addressRepository.CountByCustomerId(customerId);

            Assert.Equal(2, count);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileReadingAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            fixture.CreateMockAddresses();

            Assert.Throws<NotFoundException>(() => addressRepository.Read(0));
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();

            fixture.MockAddress.AddressLine = "Test";
            addressRepository.Update(fixture.MockAddress);
            var updatedAddress = addressRepository.Read(addressId);

            Assert.NotNull(updatedAddress);
            Assert.Equal(fixture.MockAddress.AddressId, updatedAddress.AddressId);
            Assert.Equal(fixture.MockAddress.CustomerId, updatedAddress.CustomerId);
            Assert.Equal("Test", updatedAddress.AddressLine);
            Assert.Equal(fixture.MockAddress.AddressLine2, updatedAddress.AddressLine2);
            Assert.Equal(fixture.MockAddress.AddressType, updatedAddress.AddressType);
            Assert.Equal(fixture.MockAddress.Country, updatedAddress.Country);
            Assert.Equal(fixture.MockAddress.City, updatedAddress.City);
            Assert.Equal(fixture.MockAddress.State, updatedAddress.State);
            Assert.Equal(fixture.MockAddress.PostalCode, updatedAddress.PostalCode);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileUpdatingAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            fixture.CreateMockAddresses();

            Assert.Throws<NotFoundException>(() => addressRepository.Update(new Address {AddressId = 0}));
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var customerId = fixture.CreateMockAddresses();
            var addresses = addressRepository.ReadByCustomerId(customerId);

            addressRepository.Delete(addresses[0].AddressId);

            Assert.Throws<NotFoundException>(() => addressRepository.Read(addresses[0].AddressId));
        }

        [Fact]
        public void ShouldThrowNotDeletedException()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressRepository.Read(addressId);

            Assert.NotNull(createdAddress);

            Assert.Throws<NotDeletedException>(() => addressRepository.Delete(addressId));
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileDeletingAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            fixture.CreateMockAddresses();

            Assert.Throws<NotFoundException>(() => addressRepository.Delete(0));
        }
    }

    public class AddressRepositoryFixture
    {
        public Address MockAddress { get; set; } = new Address
        {
            AddressLine = "75 PARK PLACE",
            AddressLine2 = "45 BROADWAY",
            AddressType = AddressTypes.Shipping,
            City = "New York",
            Country = "United States",
            State = "New York",
            PostalCode = "123456"
        };

        public Address MockAddress2 { get; set; } = new Address
        {
            AddressLine = "75 PARK PLACE",
            AddressLine2 = "45 BROADWAY",
            AddressType = AddressTypes.Shipping,
            City = "New York",
            Country = "United States",
            State = "New York",
            PostalCode = "123456"
        };


        public int CreateMockAddress(int customerId = 0)
        {
            var addressRepository = new AddressRepository();

            if (customerId == 0)
            {
                var customerFixture = new CustomerRepositoryFixture();
                customerId = customerFixture.CreateMockCustomer();
            }

            addressRepository.DeleteAll();

            MockAddress.CustomerId = customerId;
            var newAddressId = addressRepository.Create(MockAddress);
            return newAddressId;
        }

        public int CreateMockAddresses()
        {
            var addressRepository = new AddressRepository();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            addressRepository.DeleteAll();

            MockAddress.CustomerId = customerId;
            MockAddress2.CustomerId = customerId;
            addressRepository.Create(MockAddress);
            addressRepository.Create(MockAddress2);
            return customerId;
        }
    }
}
