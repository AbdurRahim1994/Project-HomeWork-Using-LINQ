using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.ViewModel.Purchase;
using PeopleDeskHomeWork.Models.ViewModel.Report;
using PeopleDeskHomeWork.Models.ViewModel.Sales;
using PeopleDeskHomeWork.Services.Interfaces.Report;

namespace PeopleDeskHomeWork.Services.Report
{
    public class ReportService:IReportService
    {
        private readonly HomeWorkDbContext _context;
        public ReportService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<DailyTotalPurchaseVsDailyTotalSalesViewModel> DailyTotalPurchaseVsDailyTotalSales(DateTime userDate)
        {
            var totalPurchase = await Task.FromResult((from p in _context.TblPurchases
                                       join pd in _context.TblPurchaseDetails on p.IntPurchaseId equals pd.IntPurchaseId
                                       where pd.IsActive == true && p.IsActive == true && p.DtePurchaseDate == userDate
                                       select pd.NumTotalPrice).Sum());

            var totalSales = await Task.FromResult((from s in _context.TblSales
                                                    join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                                    where s.IsActive == true && sd.IsActive == true && s.DteSalesDate == userDate
                                                    select sd.NumTotalPrice).Sum());

            return new DailyTotalPurchaseVsDailyTotalSalesViewModel
            {
                TotalPurchase = totalPurchase,
                TotalSales = totalSales
            };
        }

        public async Task<ItemWiseDailySalesVsPurchaseReport> ItemWiseDailyPurchaseVsSales(DateTime userDate)
        {
            try
            {
                var purchaseData = await (from p in _context.TblPurchases
                                          join pd in _context.TblPurchaseDetails on p.IntPurchaseId equals pd.IntPurchaseId
                                          where p.IsActive == true && pd.IsActive == true && p.DtePurchaseDate.Date == userDate.Date
                                          group new { p, pd } by new { pd.IntItemId, pd.StrItemName, p.IntSupplierId, p.StrSupplierName, p.DtePurchaseDate } into ab
                                          select new ItemWiseDailyPurchaseReportViewModel
                                          {
                                              IntItemId=ab.Key.IntItemId,
                                              StrItemName=ab.Key.StrItemName,
                                              IntSupplierId=ab.Key.IntSupplierId,
                                              StrSupplierName=ab.Key.StrSupplierName,
                                              DtePurchaseDate=ab.Key.DtePurchaseDate,
                                              IntItemQuantity=ab.Sum(x=>x.pd.NumItemQuantity),
                                              NumTotalPrice=ab.Sum(x=>x.pd.NumTotalPrice)
                                          }).ToListAsync();

                var salesData = await (from s in _context.TblSales
                                       join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                       where s.IsActive == true && sd.IsActive == true && s.DteSalesDate.Date == userDate.Date
                                       group new { s, sd } by new { sd.IntItemId, sd.StrItemName, s.IntCustomerId, s.StrCustomerName, s.DteSalesDate } into ab
                                       select new ItemWiseMonthlySalesReportViewModel
                                       {
                                           IntItemId = ab.Key.IntItemId,
                                           StrItemName = ab.Key.StrItemName,
                                           IntCustomerId = ab.Key.IntCustomerId,
                                           StrCustomerName = ab.Key.StrCustomerName,
                                           DteSalesDate=ab.Key.DteSalesDate,
                                           IntItemQuantity = ab.Sum(x => x.sd.NumtemQuantity),
                                           NumTotalPrice = ab.Sum(x => x.sd.NumTotalPrice)
                                       }).ToListAsync();

                return new ItemWiseDailySalesVsPurchaseReport
                {
                    DailyPurchase = purchaseData,
                    DailySales=salesData
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SalesVsPurchaseViewModel> SalesVsPurchase()
        {
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var purchaseData = await (from p in _context.TblPurchases
                                          join pd in _context.TblPurchaseDetails on p.IntPurchaseId equals pd.IntPurchaseId
                                          where p.IsActive == true && pd.IsActive == true
                                          && p.DtePurchaseDate.Date >= firstDayOfMonth.Date && p.DtePurchaseDate.Date <= lastDayOfMonth.Date
                                          select new 
                                          {
                                              purchaseDay=p.DtePurchaseDate.Day,
                                              purchaseAmount=pd.NumTotalPrice
                                          }).ToListAsync();

                var salesData = await (from s in _context.TblSales
                                       join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                       where s.IsActive == true && sd.IsActive == true
                                       && s.DteSalesDate.Date >= firstDayOfMonth.Date && s.DteSalesDate.Date <= lastDayOfMonth.Date
                                       select new
                                       {
                                           salesDay=s.DteSalesDate.Day,
                                           salesAmount=sd.NumTotalPrice
                                       }).ToListAsync();

                List<decimal> prch = new List<decimal>();
                List<decimal> sls = new List<decimal>();
                List<string> dte = new List<string>();

                int currentDay = DateTime.Now.Day;
                string currentYearMonth = DateTime.Now.Year + "-" + DateTime.Now.Month;

                for (int day = 1; day <=currentDay; day++)
                {
                    string dateList = currentYearMonth + "-" + day.ToString();
                    
                    var purchase = await Task.FromResult((from i in purchaseData
                                                          where i.purchaseDay == day
                                                          select i.purchaseAmount).Sum());

                    var sales = await Task.FromResult((from j in salesData
                                                       where j.salesDay == day
                                                       select j.salesAmount).Sum());

                    prch.Add(purchase);
                    sls.Add(sales);
                    dte.Add(dateList);
                }

                return new SalesVsPurchaseViewModel
                {
                    purchase = prch,
                    sales = sls,
                    date=dte
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
