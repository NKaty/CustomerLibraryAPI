using System;
using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using CustomerLibraryAPI.Common;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public class AddressRepository : IDependentRepository<Address>
    {
        private readonly CustomerLibraryContext _context;

        public AddressRepository()
        {
            _context = CustomerLibraryContextProvider.Current;
        }

        public AddressRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Address address)
        {
            var newAddress = _context.Addresses.Add(address).Entity;

            _context.SaveChanges();

            return newAddress.AddressId;
        }

        public Address Read(int addressId)
        {
            var address = _context.Addresses.Find(addressId);

            if (address is null)
            {
                throw new NotFoundException("Address is not found.");
            }

            return address;
        }

        public List<Address> ReadByCustomerId(int customerId)
        {
            return _context.Addresses.Where(a => a.CustomerId == customerId).ToList();
        }

        public int CountByCustomerId(int customerId)
        {
            return _context.Addresses.Count(a => a.CustomerId == customerId);
        }

        public void Update(Address address)
        {
            var dbAddress = _context.Addresses.Find(address.AddressId);

            if (address is null)
            {
                throw new NotFoundException("Address is not found.");
            }

            _context.Entry(dbAddress).CurrentValues.SetValues(address);

                _context.SaveChanges();
            
        }

        public void Delete(int addressId)
        {
            var address = _context.Addresses.Find(addressId);

            if (address is null)
            {
                throw new NotFoundException("Address is not found.");
            }

            _context.Addresses.Remove(address);

            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var addresses = _context.Addresses.ToList();

            foreach (var address in addresses)
            {
                _context.Addresses.Remove(address);
            }

            _context.SaveChanges();
        }
    }
}