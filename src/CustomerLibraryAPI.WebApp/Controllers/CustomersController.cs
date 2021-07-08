using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using CustomerLibraryAPI.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLibraryAPI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMainRepository<Customer> _customerRepository;

        public CustomersController(IMainRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/<ValuesController>?offset=10&limit=10
        [HttpGet]
        public CustomersPageModel Get(int offset, int limit)
        {
            var (customers, count) = _customerRepository.ReadPage(offset, limit);

            return new CustomersPageModel{ Customers = customers, TotalCount = count };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{customerId}")]
        public Customer Get(int customerId)
        {
            return _customerRepository.Read(customerId);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _customerRepository.Create(customer);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{customerId}")]
        public void Put([FromBody] Customer customer)
        {
            _customerRepository.Update(customer);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{customerId}")]
        public void Delete(int customerId)
        {
            _customerRepository.Delete(customerId);
        }
    }
}