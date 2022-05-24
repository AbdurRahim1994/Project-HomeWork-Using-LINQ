using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleDeskHomeWork.Models.ViewModel.Partner;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Controllers
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
