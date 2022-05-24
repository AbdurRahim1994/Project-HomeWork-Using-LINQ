using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.Data.Entity;
using PeopleDeskHomeWork.Models.ViewModel.Sales;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Services
{
    public class SalesService:ISalesService
    {
        MessageHelper res = new MessageHelper();
        private readonly HomeWorkDbContext _context;
        public SalesService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        
        public async Task<List<ItemWiseMonthlySalesReportViewModel>> GetItemWiseMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            List<ItemWiseMonthlySalesReportViewModel> objList = await (from s in _context.TblSales
                                                                       join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                                                       where s.IsActive == true
                                                                       && sd.IsActive == true
                                                                       && s.DteSalesDate.Date >= fromDate.Date && s.DteSalesDate.Date <= toDate.Date
                                                                       group new { s, sd } by new { sd.IntItemId, sd.StrItemName, s.DteSalesDate, s.IntCustomerId, s.StrCustomerName } into ab
                                                                       select new ItemWiseMonthlySalesReportViewModel
                                                                       {
                                                                           IntItemId=ab.Key.IntItemId,
                                                                           StrItemName=ab.Key.StrItemName,
                                                                           DteSalesDate=ab.Key.DteSalesDate,
                                                                           IntCustomerId=ab.Key.IntCustomerId,
                                                                           StrCustomerName=ab.Key.StrCustomerName,
                                                                           IntItemQuantity=ab.Sum(x=>x.sd.NumtemQuantity),
                                                                           NumUnitPrice=ab.Sum(x=>x.sd.NumUnitPrice),
                                                                           NumTotalPrice=ab.Sum(x=>x.sd.NumTotalPrice)
                                                                       }).ToListAsync();
            return objList;
        }
        public async Task<List<ItemWiseMonthlySalesReportViewModel>> GetCustomerWiseMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            List<ItemWiseMonthlySalesReportViewModel> objList = await(from s in _context.TblSales
                                                                      join sd in _context.TblSalesDetails on s.IntSalesId equals sd.IntSalesId
                                                                      where s.IsActive == true
                                                                      && sd.IsActive == true
                                                                      && s.DteSalesDate.Date >= fromDate.Date && s.DteSalesDate.Date <= toDate.Date
                                                                      group new { s, sd } by new { s.IntCustomerId, s.StrCustomerName, sd.IntItemId, sd.StrItemName, s.DteSalesDate } into ab
                                                                      select new ItemWiseMonthlySalesReportViewModel
                                                                      {
                                                                          IntCustomerId=ab.Key.IntCustomerId,
                                                                          StrCustomerName=ab.Key.StrCustomerName,
                                                                          IntItemId=ab.Key.IntItemId,
                                                                          StrItemName=ab.Key.StrItemName,
                                                                          DteSalesDate=ab.Key.DteSalesDate,
                                                                          IntItemQuantity=ab.Sum(x=>x.sd.NumtemQuantity),
                                                                          NumUnitPrice=ab.Sum(x=>x.sd.NumUnitPrice),
                                                                          NumTotalPrice=ab.Sum(x=>x.sd.NumTotalPrice)
                                                                      }).ToListAsync();
            return objList;
        }

        public async Task<MessageHelper> CreateSalesOrder(SalesOrderCommonViewModel obj)
        {
            try
            {
                if (obj.sales.IntSalesId > 0)
                {
                    var sales = await _context.TblSales.Where(x => x.IsActive == true && x.IntSalesId == obj.sales.IntSalesId).FirstOrDefaultAsync();
                    sales.IntCustomerId = obj.sales.IntCustomerId;
                    sales.StrCustomerName = obj.sales.StrCustomerName;
                    sales.DteSalesDate = obj.sales.DteSalesDate;
                    sales.IsActive = true;

                     _context.TblSales.Update(sales);
                    await _context.SaveChangesAsync();

                    List<TblSalesDetail> newRosList = new List<TblSalesDetail>();
                    List<TblSalesDetail> existingRowList = new List<TblSalesDetail>();
                    foreach (var item in obj.salesDetails)
                    {
                        if (item.IntSalesDetailsId == 0)
                        {
                            TblSalesDetail salesDetails = new TblSalesDetail
                            {
                                IntSalesId = sales.IntSalesId,
                                IntItemId = item.IntItemId,
                                StrItemName=item.StrItemName,
                                NumtemQuantity = item.IntItemQuantity,
                                NumUnitPrice = item.NumUnitPrice,
                                NumTotalPrice=item.IntItemQuantity*item.NumUnitPrice,
                                IsActive = true
                            };
                            newRosList.Add(salesDetails);
                        }
                        else
                        {
                            var salesDetailsNew = await _context.TblSalesDetails.Where(x => x.IsActive == true && x.IntSalesDetailsId == item.IntSalesDetailsId).FirstOrDefaultAsync();
                            //salesDetailsNew.IntSalesId = item.IntSalesId;
                            salesDetailsNew.IntItemId = item.IntItemId;
                            salesDetailsNew.StrItemName = item.StrItemName;
                            salesDetailsNew.NumtemQuantity = item.IntItemQuantity;
                            salesDetailsNew.NumUnitPrice = item.NumUnitPrice;
                            salesDetailsNew.NumTotalPrice = item.IntItemQuantity * item.NumUnitPrice;
                            salesDetailsNew.IsActive = item.IsActive;

                            existingRowList.Add(salesDetailsNew);
                        }
                    }
                    if (newRosList.Count > 0)
                    {
                        await _context.TblSalesDetails.AddRangeAsync(newRosList);
                        await _context.SaveChangesAsync();
                    }
                    else if (existingRowList.Count > 0)
                    {
                        _context.TblSalesDetails.UpdateRange(newRosList);
                        await _context.SaveChangesAsync();
                    }

                    res.Message = "Sales Order Updated Successfully";
                    return res;
                }
                else
                {
                    TblSale sales = new TblSale
                    {
                        IntCustomerId = obj.sales.IntCustomerId,
                        StrCustomerName=obj.sales.StrCustomerName,
                        DteSalesDate = obj.sales.DteSalesDate,
                        IsActive = true
                    };
                    await _context.TblSales.AddAsync(sales);
                    await _context.SaveChangesAsync();

                    List<TblSalesDetail> salesDetails = new List<TblSalesDetail>();
                    foreach (var item in obj.salesDetails)
                    {
                        var checkStock = await (from i in _context.TblItems where i.IntItemId == item.IntItemId select i.NumStockQuantity).FirstOrDefaultAsync();
                        if (checkStock - item.IntItemQuantity < 0)
                        {
                            res.Message = "Insufficient Stock of " + item.StrItemName;
                        }
                        TblSalesDetail details = new TblSalesDetail
                        {
                            IntSalesId = sales.IntSalesId,
                            IntItemId = item.IntItemId,
                            StrItemName=item.StrItemName,
                            NumtemQuantity = item.IntItemQuantity,
                            NumUnitPrice = item.NumUnitPrice,
                            NumTotalPrice=item.NumUnitPrice*item.IntItemQuantity,
                            IsActive = true
                        };
                        salesDetails.Add(details);
                    }

                    await _context.TblSalesDetails.AddRangeAsync(salesDetails);
                    await _context.SaveChangesAsync();

                    List<TblItem> stock = new List<TblItem>();
                    foreach (var stockItem in salesDetails)
                    {
                        TblItem itm = new TblItem
                        {
                            IntItemId = stockItem.IntItemId,
                            StrItemName = stockItem.StrItemName,
                            NumStockQuantity = stockItem.NumtemQuantity * (-1),
                            NumStockPrice = stockItem.NumUnitPrice * (-1),
                            NumTotalPrice=(stockItem.NumUnitPrice*stockItem.NumtemQuantity)*(-1),
                            IsActive = true
                        };
                        stock.Add(itm);
                    }
                    await _context.TblItems.AddRangeAsync(stock);
                    await _context.SaveChangesAsync();

                    res.Message = "Sales Order Created Successfully";
                    return res;

                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
