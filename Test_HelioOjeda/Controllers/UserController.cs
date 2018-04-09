using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_HelioOjeda.Models;
using Test_HelioOjeda.Services;

namespace Test_HelioOjeda.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    //Make sure that this class methods are only available for users who are admins
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        // GET: api/User
        [HttpGet]
        public ArrayList Get()
        {
            UserService userService = new UserService();
            return userService.GetUsers();
        }

        /* GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/
        
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserService userService = new UserService();
            //Save the ID to show it in the response
            user.Id = userService.CreateUser(user);

            if (user.Id == -1)
            {
                return BadRequest("Username already exists");
            }

            return CreatedAtAction("POST", user);

        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserService userService = new UserService();
            bool userUpdated = false;
            userUpdated = userService.UpdateUser(id, updatedUser);
            //Just to see the correct ID in the response
            updatedUser.Id = id;

            if (userUpdated)
            {
                return Ok(updatedUser);
            }
            else
            {
                return BadRequest("User with that ID does not exist or the new userName already exists");
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserService userService = new UserService();
            bool userDeleted = false;
            userDeleted = userService.DeleteUser(id);

            if (userDeleted)
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
