using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Purchase;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Purchase;

namespace PeopleDeskHomeWorkUsingSQL.Controllers.Purchase
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService _purchaseService)
        {
            this._purchaseService = _purchaseService;
        }

        [HttpPost]
        [Route("PurchaseOrder")]
        public async Task<IActionResult> PurchaseOrder(PurchaseOrderCommonViewModel obj)
        {
            return Ok(await _purchaseService.PurchaseOrder(obj));
        }
    }
}
