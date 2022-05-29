using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWork.Services.Interfaces.Report;

namespace PeopleDeskHomeWork.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService _reportService)
        {
            this._reportService = _reportService;
        }

        [HttpGet]
        [Route("DailyTotalPurchaseVsDailyTotalSales")]
        public async Task<IActionResult> DailyTotalPurchaseVsDailyTotalSales(DateTime userDate)
        {
            return Ok(await _reportService.DailyTotalPurchaseVsDailyTotalSales(userDate));
        }
        
        [HttpGet]
        [Route("ItemWiseDailyPurchaseVsSales")]
        public async Task<IActionResult> ItemWiseDailyPurchaseVsSales(DateTime userDate)
        {
            return Ok(await _reportService.ItemWiseDailyPurchaseVsSales(userDate));
        }

        [HttpGet]
        [Route("SalesVsPurchase")]
        public async Task<IActionResult> SalesVsPurchase()
        {
            return Ok(await _reportService.SalesVsPurchase());
        }

        [HttpGet]
        [Route("GetDailySalesVsPurchase")]
        public async Task<IActionResult> GetDailySalesVsPurchase()
        {
            return Ok(await _reportService.GetDailySalesVsPurchase());
        }

        [HttpGet]
        [Route("GetSalesVsPurchaseWithGivenColumn")]
        public async Task<IActionResult> GetSalesVsPurchaseWithGivenColumn(DateTime userDate)
        {
            return Ok(await _reportService.GetSalesVsPurchaseWithGivenColumn(userDate));
        }
    }
}
