using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.MenuPermission;

namespace PeopleDeskHomeWorkUsingSQL.Services.Interfaces.MenuPermission
{
    public interface IMenuPermissionServicecs
    {
        public Task<List<FirstLabelMenuViewModel>> GetMenuPermission();
    }
}
