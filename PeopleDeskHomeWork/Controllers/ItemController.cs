using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWork.Models.ViewModel.Item;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Controllers
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
        public async Task<IActionResult> CreateItem(ItemCommonViewModel obj)
        {
            return Ok(await _itemService.CreateItem(obj));
        }
    }
}
