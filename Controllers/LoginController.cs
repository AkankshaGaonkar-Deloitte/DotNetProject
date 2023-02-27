using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;  

namespace dotnetproject.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;  
        private ProjectContext _context;

        public LoginController(IConfiguration _config,ProjectContext context)
        {
            _config= _config;
            _context= context;
        }
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                User user = _context.user.SingleOrDefault(user=>user.UserName==loginDTO.UserName);
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (loginDTO.UserName.Equals(user.UserName) &&
                loginDTO.Password.Equals(user.Password))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7018",  
                        "https://localhost:7018", 
                        claims:new List<Claim>(){new Claim("roles","admin")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }