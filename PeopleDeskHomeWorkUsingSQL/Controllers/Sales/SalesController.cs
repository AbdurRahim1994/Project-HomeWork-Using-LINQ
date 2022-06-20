using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Sales;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Sales;

namespace PeopleDeskHomeWorkUsingSQL.Controllers.Sales
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

        [HttpPost]
        [Route("CreateSalesOrder")]
        public async Task<IActionResult> CreateSalesOrder(SalesOrderCommonViewModel obj)
        {
            return Ok(await _salesService.CreateSalesOrder(obj));
        }
    }
}
