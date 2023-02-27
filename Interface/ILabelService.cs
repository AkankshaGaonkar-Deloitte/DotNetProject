using dotnetproject.Models;
namespace dotnetproject.Services;

public interface ILabelService{
ResponseModel AddLabel(Label labelModel);
ResponseModel AttachLabeltoIssue(int issueId,int labelId);
ResponseModel DeleteLabelFromIssue(int issueId,int labelId);

}