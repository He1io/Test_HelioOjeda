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
        public HttpResponseMessage Post([FromBody]Customer customer)
        {
            CustomerService customerService = new CustomerService();
            customerService.createCustomer(customer);

            //Return a "Created" status code
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
        
        // PUT: api/Customer/id
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Customer updatedCustomer)
        {
            CustomerService customerService = new CustomerService();
            bool customerUpdated = false;
            customerUpdated = customerService.updateCustomer(id, updatedCustomer);

            if (customerUpdated)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

        }
        
        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            CustomerService customerService = new CustomerService();
            bool customerDeleted = false;
            customerDeleted = customerService.deleteCustomer(id);

            if (customerDeleted)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
