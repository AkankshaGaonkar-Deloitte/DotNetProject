using dotnetproject.models;
using dotnetproject.Models;
using Microsoft.AspNetCore.Mvc;
namespace dotnetproject.Services;
public interface IIssueService
    {
        List<Issue> GetIssueList();
        Issue GetIssueDetailsById(int issueId);
        ResponseModel SaveIssue(IssueDTO issueModel);
        ResponseModel DeleteIssue(int IssueId);
        ResponseModel UpdateIssue(int issueId,IssueUpdateDTO issue);
        ResponseModel UpdateStatus(int issueId,string status);
        ResponseModel AssignIssueToUser(int issueId,int userId);
        Issue SearchIssue(string issueTittle, string issueDescription);
        List<Issue> SearchByDSQL(string dsql);
    }