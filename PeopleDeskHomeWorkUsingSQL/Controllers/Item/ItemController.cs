using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Item;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Item;

namespace PeopleDeskHomeWorkUsingSQL.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService _itemService)
        {
            this._itemService = _itemService;
        }

        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(ItemViewModel obj)
        {
            return Ok(await _itemService.CreateItem(obj));
        }
    }
}
