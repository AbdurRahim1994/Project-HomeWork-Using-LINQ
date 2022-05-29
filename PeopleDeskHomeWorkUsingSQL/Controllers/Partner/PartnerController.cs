using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner;

namespace PeopleDeskHomeWorkUsingSQL.Controllers.Partner
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        public PartnerController(IPartnerService _partnerService)
        {
            this._partnerService = _partnerService;
        }

        [HttpPost]
        [Route("CreatePartnerType")]
        public async Task<IActionResult> CreatePartnerType(PartnerTypeViewModel obj)
        {
            return Ok(await _partnerService.CreatePartnerType(obj));
        }
    }
}
