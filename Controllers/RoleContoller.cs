using dotnetproject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotnetproject.Models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class RoleController:ControllerBase{
    IRoleService _roleService;
    public RoleController(IRoleService service) {
        _roleService = service;
    }
    // [Authorize(Roles="admin")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateRole(RoleDTO roleModel) {
        try {
            var model = _roleService.AddRole(roleModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    // [Authorize(Roles="admin")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult AssignRoleToUser(int UserId, int RoleId) {
        try {
            var model = _roleService.AssignRole(UserId,RoleId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}