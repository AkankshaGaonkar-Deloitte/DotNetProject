using dotnetproject.models;
using dotnetproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetproject.Services;

public class IssueService:IIssueService
{
    private ProjectContext _context;
    public IssueService(ProjectContext context) {
        _context = context;
    }


    public ResponseModel AssignIssueToUser(int issueId, int userId)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Find<Issue>(issueId);
                User user = _context.Find<User>(userId);
                issue.Assignee = user;
                model.Messsage = "Issue Assigned Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;        
    }

    public ResponseModel DeleteIssue(int issueId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueDetailsById(issueId);
            if (_temp != null) {
                _context.Remove < Issue > (_temp);
                _context.SaveChanges();
                
                model.Messsage = "Issue Deleted Successfully";
            } else {
               
                model.Messsage = "Issue Not Found";
            }
        } catch (Exception ex) {
             model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Issue GetIssueDetailsById(int issueId)
    {
        Issue issue;
        try {
            issue = _context.Issue.Include(s=>s.project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).SingleOrDefault(s=>s.IssueId==issueId);
        } catch (Exception) {
            throw;
        }
        return issue;
    }


    public List<Issue> GetIssueList()
    { 
         List < Issue > issueList;
        try {
            issueList = _context.Issue.Include(s=>s.project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).ToList();
        } catch (Exception) {
            throw;
        }
        return issueList;
    }

    // public ResponseModel SaveIssue(IssueDTO issueModel)
    // {
    //     ResponseModel model = new ResponseModel();
    //     try {
    //             Issue issue = new Issue();
    //             issue.IssueDescription = issueModel.IssueDescription;
    //             issue.projectId = issueModel.projectId;
    //             issue.IssueTittle = issueModel.IssueTittle;
    //             _context.Add < Issue > (issue);
    //             model.Messsage = "Issue Inserted Successfully";

    //         // }
    //         _context.SaveChanges();
           
    //     } catch (Exception ex) {
            
    //         model.Messsage = "Error : " + ex.Message;
    //     }
    //     return model;
    // }

     public ResponseModel SaveIssue(IssueDTO Issue)
    {
        ResponseModel model = new ResponseModel();
        try {
            bool exists; 
            exists = Enum.IsDefined(typeof(IssueType), Issue.IssueType); 
            if(exists){
                Project project = _context.Find<Project>(Issue.projectId);
                User reporter = _context.Find<User>(Issue.ReporterId);
                Issue issue = new Issue(){
                    projectId = Issue.projectId,
                    IssueType = Issue.IssueType,
                    IssueTittle = Issue.IssueTittle,
                    IssueDescription = Issue.IssueDescription,
                    Reporter = reporter,
                    Status = Issue.Status,
                    project= project
                };
                _context.Add < Issue > (issue);
                model.Messsage = "Issue Inserted Successfully";
            _context.SaveChanges();
        }
        else{
            model.Messsage = "Invalid Issue Type" ; 

        } }catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel UpdateStatus(int issueId, string status)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Find<Issue>(issueId);
                issue.Status = status;
                model.Messsage = "Status Updated Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
    public ResponseModel UpdateIssue(int issueId, IssueUpdateDTO tempIssue)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Find<Issue>(issueId);
                issue.IssueType = tempIssue.IssueType;
                issue.IssueDescription = tempIssue.IssueDescription;
                issue.IssueTittle = tempIssue.IssueTittle;
                model.Messsage = "Issue Inserted Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Issue SearchIssue(string issueTittle, string issueDescription){
            Issue issue = _context.Issue.FirstOrDefault(i=> i.IssueTittle == issueTittle && i.IssueDescription == issueDescription);
            // var issueList = _context.Issue.Where(a=> a.IssueType == );
         return issue;
    }

    public Issue SearchByDSQL(){
        
    }
}