using dotnetproject.Models;
namespace dotnetproject.Services;
public interface IRoleService{
    ResponseModel AddRole(RoleDTO model);
    ResponseModel AssignRole(int UserId,int RoleId);
}