using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test_HelioOjeda.Models;
using Test_HelioOjeda.Services;

namespace Test_HelioOjeda.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        //We need to allow anyone to get a new token
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();

            //Validate the User
            UserService userService = new UserService();
            var user = userService.ValidateUser(login.Username, login.Password);

            //if the user+password exists we give him a token
            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user.IsAdmin)
            {
                //Logic to give an "admin" credential to the user token in order to access to [Authorized(Roles = "admin")] methods
                claims[1] = new Claim(ClaimTypes.Role, "admin");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}