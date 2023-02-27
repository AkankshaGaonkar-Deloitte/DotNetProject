using Microsoft.EntityFrameworkCore;
using dotnetproject.models;
using dotnetproject.Models;

namespace dotnetproject.Services;
public class UserService : IUserService
{
    private ProjectContext _context;
    public UserService(ProjectContext context){
        _context=context;
    }
    public ResponseModel DeleteUser(int UserId)
    {
        ResponseModel model = new ResponseModel();
        try {
            User _temp = GetUserById(UserId);
            if (_temp != null) {
                _context.Remove < User > (_temp);
                _context.SaveChanges();
                model.Messsage = "User Deleted Successfully";
            } else {
                model.Messsage = "User Not Found";
            }
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }


    public User GetUserById(int UserId)
    {
        User User;
        try {
            User = _context.Find < User > (UserId);
        } catch (Exception) {
            throw;
        }
        return User;
    }

    public ResponseModel SaveUser(UserDTO User)
    {
        ResponseModel model = new ResponseModel();
        try {
                User user = new User(){
                    Name = User.Name,
                    UserName = User.UserName,
                    Email = User.Email,
                    Password = User.Password
                };
                _context.Add < User > (user);
                model.Messsage = "User Inserted Successfully";
            _context.SaveChanges();
        } catch (Exception ex) {
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}
