using PeopleDeskHomeWork.Models.ViewModel.Report;

namespace PeopleDeskHomeWork.Services.Interfaces.Report
{
    public interface IReportService
    {
        public Task<DailyTotalPurchaseVsDailyTotalSalesViewModel> DailyTotalPurchaseVsDailyTotalSales(DateTime userDate);
        public Task<ItemWiseDailySalesVsPurchaseReport> ItemWiseDailyPurchaseVsSales(DateTime userDate);
        public Task<SalesVsPurchaseViewModel> SalesVsPurchase();
    }
}
