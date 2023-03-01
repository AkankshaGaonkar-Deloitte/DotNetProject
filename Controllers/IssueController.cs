using dotnetproject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dotnetproject.Models;
using Microsoft.AspNetCore.Authorization;
using dotnetproject.models;

namespace dotnetproject.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class IssueController:ControllerBase{
    ILogger<IssueController> _issuelogger;
    IIssueService _issueService;
    public IssueController(IIssueService service,ILogger<IssueController> issuelogger) {
        _issueService = service;
        _issuelogger = issuelogger;
    }

    [Authorize(Roles="admin,projectManager,standard")]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllIssues() {
        try {
            var issues = _issueService.GetIssueList();
            _issuelogger.LogInformation("Getting all the list of issues");
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager,standard")]
    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetIssueById(int id) {
        try {
            var issues = _issueService.GetIssueDetailsById(id);
            _issuelogger.LogInformation("Getting the issue By id");
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager")]
    [HttpPost]
    [Route("[action]")]
    public IActionResult SaveIssue(IssueDTO issueModel) {
        try {
            var model = _issueService.SaveIssue(issueModel);
            _issuelogger.LogInformation("Add Issue");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager")]
    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            _issuelogger.LogInformation("Delete Issues");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateIssue(int issueId,IssueUpdateDTO issueModel) {
        try {
            var model = _issueService.UpdateIssue(issueId,issueModel);
            _issuelogger.LogInformation("Update Issues");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateStatus(int issueId,string status) {
        try {
            var model = _issueService.UpdateStatus(issueId,status);
            _issuelogger.LogInformation("Update Status");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [Authorize(Roles="admin,projectManager")]
    [HttpPut]
    [Route("[action]")]
    public IActionResult AssignIssueToUser(int issueId,int userId) {
        try {
            var model = _issueService.AssignIssueToUser(issueId,userId);
            _issuelogger.LogInformation("Assign issue to user");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpGet]
    [Route("[action]/issueTittle/issueDescription")]
    public IActionResult SearchIssue(string issueTittle, string issueDescription) {
        try {
            var issues = _issueService.SearchIssue(issueTittle,issueDescription);
            _issuelogger.LogInformation("Search Issues");
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception ex) {

            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/dsql")]
    public IActionResult SearchByDSQL([FromQuery]string dsql){
        try{
            if (string.IsNullOrWhiteSpace(dsql)) {
     return BadRequest("No DSQL query specified.");}
     var issues = _issueService.SearchByDSQL(dsql);
            if (issues == null) return NotFound();
            return Ok(issues);

        }
        catch (Exception ex) {

            Console.WriteLine(ex);
            return BadRequest();
        }

    }
}