using dotnetproject.Models;
using Microsoft.EntityFrameworkCore;
namespace dotnetproject.Services;
public class ProjectService:IProjectService
{
    private ProjectContext _context;
    public ProjectService(ProjectContext context) {
        _context = context;
    }

    public ResponseModel DeleteProject(int projectId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectDetailsById(projectId);
            if (_temp != null) {
                _context.Remove < Project > (_temp);
                _context.SaveChanges();
               
                model.Messsage = "Project Deleted Successfully";
            } else {
              
                model.Messsage = "Project Not Found";
            }
        } catch (Exception ex) {
            
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Project GetProjectDetailsById(int projectId)
    {
        Project project;
        try {
            project = _context.Find < Project > (projectId);
        } catch (Exception) {
            throw;
        }
        return project;
    }

    public List<Project> GetProjectList()
    { 
         List < Project > projectList;
        try {
           projectList = _context.Project.Include(s=>s.issues).Include(s=>s.Creator).ToList();
        } catch (Exception) {
            throw;
        }
        return projectList;
    }

    public ResponseModel SaveProject(ProjectDTO projectModel)
    {
         ResponseModel model = new ResponseModel();
        try {
                User user = _context.Find<User>(projectModel.CreatorId);
                Project project = new Project(){
                    ProjectName = projectModel.ProjectName,
                    ProjectDescription = projectModel.ProjectDescription,
                    Creator = user
                };
                _context.Add < Project > (project);
                model.Messsage = "Project Inserted Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel UpdateProject(int projectId, string ProjectDescription)
    {
        ResponseModel model = new ResponseModel();
        try {
                Project project = _context.Find<Project>(projectId);
                model.Messsage = "Project Updated Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

     public ICollection<Issue> GetIssuesByProjectId(int projectId)
    {
        ICollection<Issue> issues;
        try{
            issues =_context.Project.Include(s=>s.issues).Include(s=>s.Creator).SingleOrDefault(s=>s.ProjectId==projectId).issues;
        } catch (Exception){
            throw;
        }
        return issues;
    }
}