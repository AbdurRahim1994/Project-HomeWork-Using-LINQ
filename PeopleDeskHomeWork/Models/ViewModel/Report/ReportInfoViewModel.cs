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


    public class ItemWiseDailySalesVsPurchaseViewModel
    {
        public List<string>? StrItemName { get; set; }
        public List<decimal>? SalesQuantity { get; set; }
        public List<decimal>? SalesAmount { get; set; }
        public List<decimal>? PurchaseQuantity { get; set; }
        public List<decimal>? PurchaseAmount { get; set; }
        public List<string>? Date { get; set; }
    }
    public class ItemWiseSalesVsPurchaseWithGivenColumnViewModel
    {
        public long IntItemId { get; set; }
        public string StrItemName { get; set; }
        public string StrMonthName { get; set; }
        public int IntYear { get; set; }
        public DateTime DtePurchaseDate { get; set; }
        public DateTime DteSalesDate { get; set; }
        public decimal NumTotalPurchase { get; set; }
        public decimal NumTotalSales { get; set; }
        public string Status { get; set; }
    }
}
