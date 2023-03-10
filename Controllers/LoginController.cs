using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                User user =  _context.user.Include(s=>s.Roles).SingleOrDefault(user=>user.UserName==loginDTO.UserName);
                if (user == null || string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password)){
                    return BadRequest("Invalid username or password");
                }
                if (loginDTO.UserName.Equals(user.UserName) &&
                loginDTO.Password.Equals(user.Password))
                {   
                    var claims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role.title));
                    List<Claim> Claims=new List<Claim>();
                    foreach (var i in user.Roles){
                        Claims.Add(new Claim(ClaimTypes.Role,i.title.ToString()));
                    }
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                       "https://localhost:7018",  
                        "https://localhost:7018", 
                        claims:Claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
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