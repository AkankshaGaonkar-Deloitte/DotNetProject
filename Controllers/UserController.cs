using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotnetproject.Services;
using dotnetproject.Models;

namespace dotnetproject.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase{
    IUserService _userService;
    public UserController(IUserService service) {
        _userService = service;
    }
    
    [HttpPost]
    [Route("[action]")]
    public IActionResult SignUp(UserDTO userModel) {
        try {
            var model = _userService.SaveUser(userModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

}