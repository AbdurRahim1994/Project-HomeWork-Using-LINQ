using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.MenuPermission;

namespace PeopleDeskHomeWorkUsingSQL.Controllers.MenuPermission
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPermissionController : ControllerBase
    {
        private readonly IMenuPermissionServicecs _menuPermission;
        public MenuPermissionController(IMenuPermissionServicecs _menuPermission)
        {
            this._menuPermission = _menuPermission;
        }

        [HttpGet]
        [Route("GetMenuPermission")]
        public async Task<IActionResult> GetMenuPermission()
        {
            return Ok(await _menuPermission.GetMenuPermission());
        }
    }
}
