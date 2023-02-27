using dotnetproject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using dotnetproject.Models;

namespace dotnetproject.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class LabelController:ControllerBase{
    ILabelService _labelService;
    public LabelController(ILabelService service) {
        _labelService = service;
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult AttachLabelsToIssue(int issueId,int labelId) {
        try {
            var model = _labelService.AttachLabeltoIssue(issueId,labelId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [HttpPost]
    [Route("[action]")]
    public IActionResult AddLablels(Label label) {
        try {
            var model = _labelService.AddLabel(label);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteLabelFromIssue(int issueId, int labelId) {
        try {
            var model = _labelService.DeleteLabelFromIssue( issueId, labelId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}