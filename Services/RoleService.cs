using dotnetproject.Models;

namespace dotnetproject.Services;
public class RoleService : IRoleService
{
    ProjectContext _context;
    public RoleService(ProjectContext context){
        _context=context;
    }
    public ResponseModel AddRole(RoleDTO roleModel)
    {
        ResponseModel model = new ResponseModel();
        try {
                Roles role = new Roles(){
                    title = roleModel.title
                };
                _context.Add < Roles > (role);
                model.Messsage = "Role Inserted Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel AssignRole(int UserId,int RoleId)
    {
        ResponseModel model = new ResponseModel();
        try {
                User user = _context.Find<User>(UserId);
                Roles role = _context.Find<Roles>(RoleId);
                if (user == null){
                    model.Messsage = "User Not Found";
                }
                else if (role == null){
                    model.Messsage = "Role Not Found";
                }
                else{
                    user.Roles.Add(role);
                    model.Messsage = "Role Assigned Successfully";
                    _context.SaveChanges();
                }
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}