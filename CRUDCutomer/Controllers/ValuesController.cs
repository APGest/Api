using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDCustomer.Model;
using CRUDCutomer.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CRUDCutomer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        CustomerRepository customerRepository = new CustomerRepository();
        // GET api/values
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var result = customerRepository.ReadAll();
            return (result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            var result = customerRepository.Read(id);
            return (result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            customerRepository.Create(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody]Customer customer, int id)
        {
            customerRepository.Update(customer, id);
        }

        // DELETE api/values/5
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            customerRepository.Delete(id);
        }
    }
}
