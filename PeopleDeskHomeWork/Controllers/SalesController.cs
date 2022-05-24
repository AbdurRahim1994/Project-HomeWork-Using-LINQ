using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWork.Models.ViewModel.Sales;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService _salesService)
        {
            this._salesService = _salesService;
        }
        [HttpGet]
        [Route("GetItemWiseMonthlySalesReport")]
        public async Task<IActionResult> GetItemWiseMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            return Ok(await _salesService.GetItemWiseMonthlySalesReport(fromDate, toDate));
        }
        [HttpGet]
        [Route("GetCustomerWiseMonthlySalesReport")]
        public async Task<IActionResult> GetCustomerWiseMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            return Ok(await _salesService.GetCustomerWiseMonthlySalesReport(fromDate, toDate));
        }
        [HttpPost]
        [Route("CreateSalesOrder")]
        public async Task<IActionResult> CreateSalesOrder(SalesOrderCommonViewModel obj)
        {
            return Ok(await _salesService.CreateSalesOrder(obj));
        }
    }
}
