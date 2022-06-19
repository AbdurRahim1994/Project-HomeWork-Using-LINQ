using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.Data;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.MenuPermission;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.MenuPermission;

namespace PeopleDeskHomeWorkUsingSQL.Services.MenuPermission
{
    public class MenuPermissionService:IMenuPermissionServicecs
    {
        private readonly HomeWorkDbContext _context;
        MessageHelper res = new MessageHelper();
        public MenuPermissionService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<List<FirstLabelMenuViewModel>> GetMenuPermission()
        {
            try
            {
                List<FirstLabelMenuViewModel> menuList = await (from first in _context.TblAllMenus
                                                                where first.IsActive == true && first.IntMenuLabelId == 1 && first.IntParentMenuId == 0
                                                                select new FirstLabelMenuViewModel
                                                                {
                                                                    IntMenuId = first.IntMenuId,
                                                                    StrMenuName = first.StrMenuName,
                                                                    StrTo = first.StrTo,
                                                                    IntParentMenuId = first.IntParentMenuId,
                                                                    IntMenuLabelId = first.IntMenuLabelId,
                                                                    IsActive = first.IsActive,
                                                                    IsExpandable = first.IsExpandable,
                                                                    secondLabelMenu =(from second in _context.TblAllMenus
                                                                                       where second.IsActive == true && second.IntMenuLabelId == 2 && second.IntParentMenuId == first.IntMenuId
                                                                                       select new SecondLabelMenuViewModel
                                                                                       {
                                                                                           IntMenuId = second.IntMenuId,
                                                                                           StrMenuName = second.StrMenuName,
                                                                                           StrTo = second.StrTo,
                                                                                           IntParentMenuId = second.IntParentMenuId,
                                                                                           IntMenuLabelId = second.IntMenuLabelId,
                                                                                           IsActive = second.IsActive,
                                                                                           IsExpandable = second.IsExpandable,
                                                                                           thirdLabelMenu = (from third in _context.TblAllMenus
                                                                                                             where third.IsActive == true && third.IntMenuLabelId == 3 && third.IntParentMenuId == second.IntMenuId
                                                                                                             select new ThirdLabelMenuViewModel
                                                                                                             {
                                                                                                                 IntMenuId = third.IntMenuId,
                                                                                                                 StrMenuName = third.StrMenuName,
                                                                                                                 StrTo = third.StrTo,
                                                                                                                 IntParentMenuId = third.IntParentMenuId,
                                                                                                                 IntMenuLabelId = third.IntMenuLabelId,
                                                                                                                 IsActive = third.IsActive,
                                                                                                                 IsExpandable = third.IsExpandable
                                                                                                             }).ToList()
                                                                                       }).ToList()
                                                                }).ToListAsync();
                return menuList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
