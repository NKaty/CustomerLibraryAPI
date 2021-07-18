using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomerLibraryAPI.Common;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public class CustomerRepository : IMainRepository<Customer>
    {
        private readonly CustomerLibraryContext _context;

        public CustomerRepository()
        {
            _context = CustomerLibraryContextProvider.Current;
        }

        public CustomerRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Customer customer)
        {
            var newCustomer = _context.Customers.Add(customer).Entity;

            _context.SaveChanges();

            return newCustomer.CustomerId;
        }

        public Customer Read(int customerId)
        {
            var customer = _context.Customers
                .Include("Addresses")
                .Include("Notes")
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer is null)
            {
                throw new NotFoundException("Customer is not found.");
            }

            return customer;
        }

        public int Count()
        {
            return _context.Customers.Count();
        }

        public (List<Customer>, int) ReadPage(int offset, int limit)
        {
            return (_context.Customers.OrderByDescending(c => c.CustomerId).Skip(offset).Take(limit).ToList(), Count());
        }

        public void Update(Customer customer)
        {
            var dbCustomer = _context
                .Customers
                .FirstOrDefault(c => c.CustomerId == customer.CustomerId);

            if (dbCustomer is null)
            {
                throw new NotFoundException("Customer is not found.");
            }

            _context.Entry<Customer>(dbCustomer).State = EntityState.Detached;

            _context.Customers.Update(customer);

            _context.SaveChanges();
        }

        public void Delete(int customerId)
        {
            var customer = _context
                .Customers
                .Include("Addresses")
                .Include("Notes")
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer is null)
            {
                throw new NotFoundException("Customer is not found.");
            }

            _context.Customers.Remove(customer);

            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var customers = _context
                .Customers
                .Include("Addresses")
                .Include("Notes")
                .ToList();

            foreach (var customer in customers)
            {
                _context.Customers.Remove(customer);
            }

            _context.SaveChanges();
        }
    }
}