using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.ViewModel.Purchase;
using System.ComponentModel.DataAnnotations;

namespace PeopleDeskHomeWork.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<List<ItemWiseDailyPurchaseReportViewModel>> GetItemWiseDailyPurchaseReport([Required]DateTime purchaseDate);
        Task<List<ItemWiseDailyPurchaseReportViewModel>> GetSupplierWiseDailyPurchaseReport([Required] DateTime purchaseDate);
        Task<MessageHelper> CreatePurchaseOrder(PurchaseOrderCommonViewModel obj);
    }
}
