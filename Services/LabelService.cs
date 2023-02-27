using dotnetproject.Models;
using Microsoft.EntityFrameworkCore;
// using DotnetAssignment.Services.ProjectService;
namespace dotnetproject.Services;
// namespace DotnetAssignment.Services.ProjectService;
public class LabelService:ILabelService
{
    private ProjectContext _context;
    // public ProjectService projService;

    public LabelService(ProjectContext context) {
        _context = context;   
    }
    public ResponseModel AttachLabeltoIssue(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
        // Retrieve the Issue and Label objects from the database
            Issue issue = _context.Issue.Include(i => i.Labels).FirstOrDefault(i => i.IssueId == issueId);
            Label label = _context.labels.Find(labelId);

        // Add the Label object to the Labels navigation property of the Issue object
            issue.Labels.Add(label);

        // Update the Issue object in the database and save changes
            _context.Update(issue);
            _context.SaveChanges();

        model.Messsage = "Label added successfully";
        } catch (Exception ex) {
            model.Messsage = "Error: " + ex.Message;
        }
    return model;
    }
    public ResponseModel AddLabel(Label label)
    {
        ResponseModel model = new ResponseModel();
        try {
                _context.Add < Label > (label);
                model.Messsage = "Label Inserted Successfully";
                _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
    public ResponseModel DeleteLabelFromIssue(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Issue.Include(i => i.Labels).FirstOrDefault(i => i.IssueId == issueId);
                Label label = _context.Find<Label>(labelId);
                issue.Labels.Remove(label);
                model.Messsage = "Label Deleted Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;        
    }
}