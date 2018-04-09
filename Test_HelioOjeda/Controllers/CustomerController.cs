using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_HelioOjeda.Models;
using Test_HelioOjeda.Services;

namespace Test_HelioOjeda.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    //Make sure that this class methods are only available for users who received a token
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: api/Customer
        [HttpGet]
        public ArrayList Get()
        {
            CustomerService customerService = new CustomerService();
            return customerService.GetCustomers();
        }

        // GET: api/Customer/id
        [HttpGet("{id}", Name = "Get")]
        public Customer Get(int id)
        {
            CustomerService customerService = new CustomerService();
            return customerService.GetCustomer(id);
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
            var currentUser = User.Identity.Name;

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerService customerService = new CustomerService();
            //Save the ID and CreatedBy to show it in the response
            customer.Id = customerService.CreateCustomer(customer, currentUser);
            customer.CreatedBy = currentUser;

            return CreatedAtAction("POST", customer);
        }
        
        // PUT: api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer updatedCustomer)
        {
            var currentUser = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerService customerService = new CustomerService();
            bool customerUpdated = false;
            customerUpdated = customerService.UpdateCustomer(id, updatedCustomer, currentUser);
            //Just to see the correct ID and ModifiedBy in the response
            updatedCustomer.Id = id;
            updatedCustomer.ModifiedBy = currentUser;

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
            customerDeleted = customerService.DeleteCustomer(id);

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
