using dotnetproject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dotnetproject.Models;
using Microsoft.AspNetCore.Authorization;
using dotnetproject.models;

namespace dotnetproject.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class IssueController:ControllerBase{
    IIssueService _issueService;
    public IssueController(IIssueService service) {
        _issueService = service;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllIssues() {
        try {
            var issues = _issueService.GetIssueList();
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetIssueById(int id) {
        try {
            var issues = _issueService.GetIssueDetailsById(id);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpPost]
    [Route("[action]")]
    public IActionResult SaveIssue(IssueDTO issueModel) {
        try {
            var model = _issueService.SaveIssue(issueModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateIssue(int issueId,IssueUpdateDTO issueModel) {
        try {
            var model = _issueService.UpdateIssue(issueId,issueModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
     [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateStatus(int issueId,string status) {
        try {
            var model = _issueService.UpdateStatus(issueId,status);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult AssignIssueToUser(int issueId,int userId) {
        try {
            var model = _issueService.AssignIssueToUser(issueId,userId);
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
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception ex) {

            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    

    // [HttpGet]
    // [Route("[action]/projectid")]
    // public IActionResult GetListOfIssuesBy()

    // [HttpPut]
    // [Route("[action]")]
    // public IActionResult UpdateIssue(int id){
    //     try {
    //         var model = _issueService.SaveIssue(issueModel);
    //         return Ok(model);
    //     } catch (Exception) {
    //         return BadRequest();
    //     }

    // }
}