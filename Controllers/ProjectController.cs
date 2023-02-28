using dotnetproject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dotnetproject.Models;
using Microsoft.AspNetCore.Authorization;

namespace dotnetproject.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class ProjectController:ControllerBase{
    IProjectService _projectService;
    public ProjectController(IProjectService service) {
        _projectService = service;
    }
    [Authorize(Roles="admin,projectManager,standard")]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllProjects() {
        try {
            var projects = _projectService.GetProjectList();
            if (projects == null) return NotFound();
            return Ok(projects);
        } catch (Exception) {
            return BadRequest();
        }
    }
     [Authorize(Roles="admin,projectManager,standard")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetProjectById(int id) {
        try {
            var projects = _projectService.GetProjectDetailsById(id);
            if (projects == null) return NotFound();
            return Ok(projects);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult SaveProjects(ProjectDTO projectModel) {
        try {
            var model = _projectService.SaveProject(projectModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager,standard")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetIssuesByProjectId(int id) {
        try {
            var issues = _projectService.GetIssuesByProjectId(id);
            if (issues.Count == 0) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateProject(int projectId,string description) {
        try {
            var model = _projectService.UpdateProject(projectId,description);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin")]
    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult DeleteProject(int id) {
        try {
            var model = _projectService.DeleteProject(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/dsql")]
    public IActionResult SearchProjectByDSQL([FromQuery]string dsql){
        try{
            if (string.IsNullOrWhiteSpace(dsql)) {
     return BadRequest("No DSQL query specified.");}
     var project = _projectService.SearchProjectByDSQL(dsql);
            if (project == null) return NotFound();
            return Ok(project);

        }
        catch (Exception ex) {

            Console.WriteLine(ex);
            return BadRequest();
        }

    }

}