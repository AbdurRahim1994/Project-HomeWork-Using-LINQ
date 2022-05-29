using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Partner;

namespace PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner
{
    public interface IPartnerService
    {
        #region Partner Type
        public Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj);
        #endregion
    }
}
