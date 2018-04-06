using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public ArrayList Get()
        {
            CustomerService customerService = new CustomerService();
            return customerService.getCustomers();
        }

        // GET: api/Customer/id
        [HttpGet("{id}", Name = "Get")]
        public Customer Get(int id)
        {
            CustomerService customerService = new CustomerService();
            return customerService.getCustomer(id);
        }


        /* In POST and PUT I have to pass the Customer from the client request in JSON (ID is "autoincrement" so I don't need to pass it)
         * Example:
         * {
         * "name":"Helio"
         * "surname":"Ojeda"
         * "photoUrl":"www.prueba.com"
         * }
         */

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerService customerService = new CustomerService();
            //Save the ID to show it in the response
            customer.Id = customerService.createCustomer(customer);

            return CreatedAtAction("POST", customer);
        }
        
        // PUT: api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer updatedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerService customerService = new CustomerService();
            bool customerUpdated = false;
            customerUpdated = customerService.updateCustomer(id, updatedCustomer);
            //Just to see the correct ID in the response
            updatedCustomer.Id = id;

            if (customerUpdated)
            {
                return Ok(updatedCustomer);
            }
            else
            {
                return NotFound();
            }

        }
        
        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerService customerService = new CustomerService();
            bool customerDeleted = false;
            customerDeleted = customerService.deleteCustomer(id);

            if (customerDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
