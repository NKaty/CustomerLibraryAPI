using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerLibraryAPI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IDependentRepository<Address> _addressRepository;

        public AddressesController(IDependentRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        // GET: api/<AddressController>?customerId=10
        [HttpGet]
        public IEnumerable<Address> GetByCustomerId(int customerId)
        {
            return _addressRepository.ReadByCustomerId(customerId);
        }

        // GET api/<AddressController>/5
        [HttpGet("{addressId}")]
        public Address Get(int addressId)
        {
            return _addressRepository.Read(addressId);
        }

        // POST api/<AddressController>
        [HttpPost]
        public void Post([FromBody] Address address)
        {
            _addressRepository.Create(address);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{addressId}")]
        public void Put([FromBody] Address address)
        {
            _addressRepository.Update(address);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{addressId}")]
        public void Delete(int addressId)
        {
            _addressRepository.Delete(addressId);
        }
    }
}
