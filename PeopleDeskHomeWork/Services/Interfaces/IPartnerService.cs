using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.ViewModel.Partner;

namespace PeopleDeskHomeWork.Services.Interfaces
{
    public interface IPartnerService
    {
        #region Partner Type
        Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj);
        #endregion
    }
}
