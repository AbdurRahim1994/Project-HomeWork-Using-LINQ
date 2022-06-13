using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Purchase;

namespace PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Purchase
{
    public interface IPurchaseService
    {
        public Task<MessageHelper> PurchaseOrder(PurchaseOrderCommonViewModel obj);
    }
}
