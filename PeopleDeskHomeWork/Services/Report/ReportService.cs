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
                                       where pd.IsActive == true && p.IsActive == true && p.DtePurchaseDate.Date == userDate.Date
                                       select pd.NumTotalPrice).Sum());

            var totalSales = await Task.FromResult((from s in _context.TblSales
                                                    join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                                    where s.IsActive == true && sd.IsActive == true && s.DteSalesDate.Date == userDate.Date
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
                                          join i in _context.TblItems on pd.IntItemId equals i.IntItemId
                                          where p.IsActive == true && pd.IsActive == true && p.DtePurchaseDate.Date == userDate.Date
                                          group new { p, pd, i } by new { pd.IntItemId, i.StrItemName, p.DtePurchaseDate.Date } into ab
                                          select new ItemWiseDailyPurchaseReportViewModel
                                          {
                                              IntItemId=ab.Key.IntItemId,
                                              StrItemName=ab.Key.StrItemName,
                                              DtePurchaseDate=ab.Key.Date,
                                              IntItemQuantity=ab.Sum(x=>x.pd.NumItemQuantity),
                                              NumTotalPrice=ab.Sum(x=>x.pd.NumTotalPrice)
                                          }).ToListAsync();

                var salesData = await (from s in _context.TblSales
                                       join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                       join i in _context.TblItems on sd.IntItemId equals i.IntItemId
                                       where s.IsActive == true && sd.IsActive == true && s.DteSalesDate.Date == userDate.Date
                                       group new { s, sd, i } by new { sd.IntItemId, i.StrItemName, s.DteSalesDate.Date } into ab
                                       select new ItemWiseMonthlySalesReportViewModel
                                       {
                                           IntItemId = ab.Key.IntItemId,
                                           StrItemName = ab.Key.StrItemName,
                                           DteSalesDate=ab.Key.Date,
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
        public async Task<ItemWiseDailySalesVsPurchaseViewModel> GetDailySalesVsPurchase()
        {
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var purchaseData = await (from i in _context.TblItems
                                          join pd in _context.TblPurchaseDetails on i.IntItemId equals pd.IntItemId
                                          join p in _context.TblPurchases on pd.IntPurchaseId equals p.IntPurchaseId
                                          where p.IsActive == true && pd.IsActive == true
                                          //group new {i} by new {i.IntItemId, i.StrItemName} into ab
                                          select new
                                          {
                                              itemName = i.StrItemName,
                                              purchaseDay = p.DtePurchaseDate.Day,
                                              purchaseQuantity=pd.NumItemQuantity,
                                              purchaseAmount = pd.NumTotalPrice
                                          }).ToListAsync();

                var salesData = await (from i in _context.TblItems
                                       join sd in _context.TblSalesDetails on i.IntItemId equals sd.IntItemId
                                       join s in _context.TblSales on sd.IntSalesId equals s.IntSalesId
                                       where sd.IsActive == true && s.IsActive == true
                                       select new
                                       {
                                           itemName = i.StrItemName,
                                           salesDay = s.DteSalesDate.Day,
                                           salesQuantity=sd.NumtemQuantity,
                                           salesAmount = sd.NumTotalPrice
                                       }).ToListAsync();

                List<string> itmName = new List<string>();
                List<decimal> slsQty = new List<decimal>();
                List<decimal> slsAmount = new List<decimal>();
                List<decimal> prchQty = new List<decimal>();
                List<decimal> prchAmount = new List<decimal>();
                List<string> dte = new List<string>();

                int currentDay = DateTime.Now.Day;
                string currentYearMonth = DateTime.Now.Year + "-" + DateTime.Now.Month;
                
                for (int day = 1; day <=currentDay; day++)
                {
                    string dateList = currentYearMonth + "-" + day.ToString();

                    var purchaseQuantity = await Task.FromResult((from p in purchaseData
                                                  where p.purchaseDay == day
                                                  select p.purchaseQuantity).Sum());

                    var salesQuantity = await Task.FromResult((from s in salesData
                                                               where s.salesDay == day
                                                               select s.salesQuantity).Sum());

                    dte.Add(dateList);
                    prchQty.Add(purchaseQuantity);
                    slsQty.Add(salesQuantity);
                }
                return new ItemWiseDailySalesVsPurchaseViewModel
                {
                    Date = dte,
                    PurchaseQuantity=prchQty,
                    SalesQuantity=slsQty
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ItemWiseSalesVsPurchaseWithGivenColumnViewModel>> GetSalesVsPurchaseWithGivenColumn(DateTime userDate)
        {
            try
            {
                var data = await (from i in _context.TblItems
                                  join pd in _context.TblPurchaseDetails on i.IntItemId equals pd.IntItemId
                                  join p in _context.TblPurchases on pd.IntPurchaseId equals p.IntPurchaseId
                                  join sd in _context.TblSalesDetails on i.IntItemId equals sd.IntItemId
                                  join s in _context.TblSales on sd.IntSalesId equals s.IntSalesId
                                  where pd.IsActive == true && sd.IsActive == true && p.IsActive == true && s.IsActive == true
                                  group new { i, pd, sd } by new { i.IntItemId, i.StrItemName } into ab
                                  select new ItemWiseSalesVsPurchaseWithGivenColumnViewModel
                                  {
                                      IntItemId = ab.Key.IntItemId,
                                      StrItemName = ab.Key.StrItemName,
                                      //DtePurchaseDate=userDate.Date,
                                      //DteSalesDate=userDate.Date,
                                      StrMonthName = userDate.ToString("MMMM"),
                                      IntYear = userDate.Year,
                                      NumTotalPurchase = ab.Sum(x => x.pd.NumItemQuantity * x.pd.NumUnitPrice),
                                      NumTotalSales = ab.Sum(x => x.sd.NumtemQuantity * x.sd.NumUnitPrice),
                                      Status= ab.Sum(x => x.sd.NumtemQuantity * x.sd.NumUnitPrice)> ab.Sum(x => x.pd.NumItemQuantity * x.pd.NumUnitPrice)?"Profit":"Loss"
                                  }).ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
