using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Test_HelioOjeda.Models;
using Test_HelioOjeda.Services;

namespace Test_HelioOjeda.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Customer1", "Customer2" };
        }

        // GET: api/Customer/id
        [HttpGet("{id}", Name = "Get")]
        public Customer Get(int id)
        {
            Customer customer = new Customer();
            customer.Id = id;
            customer.Name = "Helio";
            customer.Surname = "Ojeda";
            customer.PhotoURL = "www.prueba.com";

            return customer;
        }
        
        // POST: api/Customer
        // I have to pass the Customer from the request in JSON (ID is "autoincrement" so I don't need to pass it)
        /* Example:
         * {
         * "name":"Helio"
         * "surname":"Ojeda"
         * "photoUrl":"www.prueba.com"
         * }
         */
        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            CustomerService customerService = new CustomerService();

            customerService.createCustomer(customer);
        }
        
        // PUT: api/Customer/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
