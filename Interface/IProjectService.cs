using dotnetproject.Models;
namespace dotnetproject.Services;
public interface IProjectService
    {
    
        List<Project> GetProjectList();

        Project GetProjectDetailsById(int projectId);

        ResponseModel SaveProject(ProjectDTO projectModel);

        ResponseModel DeleteProject(int ProjectId);

         ICollection<Issue> GetIssuesByProjectId(int projectId);
        ResponseModel UpdateProject(int projectId,string description);
    }