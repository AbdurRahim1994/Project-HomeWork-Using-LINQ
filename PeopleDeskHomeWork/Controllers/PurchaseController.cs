using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWork.Models.ViewModel.Purchase;
using PeopleDeskHomeWork.Models.ViewModel.Sales;
using PeopleDeskHomeWork.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PeopleDeskHomeWork.Controllers
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
        [HttpGet]
        [Route("GetItemWiseDailyPurchaseReport")]
        public async Task<IActionResult> GetItemWiseDailyPurchaseReport([Required]DateTime purchaseDate)
        {
            return Ok(await _purchaseService.GetItemWiseDailyPurchaseReport(purchaseDate));
        }
        [HttpGet]
        [Route("GetSupplierWiseDailyPurchaseReport")]
        public async Task<IActionResult> GetSupplierWiseDailyPurchaseReport([Required] DateTime purchaseDate)
        {
            return Ok(await _purchaseService.GetSupplierWiseDailyPurchaseReport(purchaseDate));
        }
        [HttpPost]
        [Route("CreatePurchaseOrder")]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCommonViewModel obj)
        {
            return Ok(await _purchaseService.CreatePurchaseOrder(obj));
        }
    }
}
