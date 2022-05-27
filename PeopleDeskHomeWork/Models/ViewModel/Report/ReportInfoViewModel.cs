using PeopleDeskHomeWork.Models.ViewModel.Purchase;
using PeopleDeskHomeWork.Models.ViewModel.Sales;

namespace PeopleDeskHomeWork.Models.ViewModel.Report
{
    public class ReportInfoViewModel
    {
    }
    public class DailyTotalPurchaseVsDailyTotalSalesViewModel
    {
        public decimal? TotalPurchase { get; set; }
        public decimal? TotalSales { get; set; }
    }
    
    public class ItemWiseDailySalesVsPurchaseReport
    {
        public List<ItemWiseDailyPurchaseReportViewModel>? DailyPurchase { get; set; }
        public List<ItemWiseMonthlySalesReportViewModel>? DailySales { get; set; }
    }

    public class SalesVsPurchaseViewModel
    {
        public List<decimal>? purchase { get; set; }
        public List<decimal>? sales { get; set; }
        public List<string>? date { get; set; }
    }
}
