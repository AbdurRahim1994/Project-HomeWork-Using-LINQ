using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.Data.Entity;
using PeopleDeskHomeWork.Models.ViewModel.Purchase;
using PeopleDeskHomeWork.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PeopleDeskHomeWork.Services
{
    public class PurchaseService:IPurchaseService
    {
        MessageHelper res = new MessageHelper();
        private readonly HomeWorkDbContext _context;
        public PurchaseService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<List<ItemWiseDailyPurchaseReportViewModel>> GetItemWiseDailyPurchaseReport([Required] DateTime purchaseDate)
        {
            try
            {
                List<ItemWiseDailyPurchaseReportViewModel> objList = await (from p in _context.TblPurchases
                                                                            join pd in _context.TblPurchaseDetails on p.IntPurchaseId equals pd.IntPurchaseId
                                                                            where p.IsActive == true
                                                                            && pd.IsActive == true
                                                                            && p.DtePurchaseDate.Date == purchaseDate.Date
                                                                            group new { p, pd } by new { pd.IntItemId, pd.StrItemName, p.DtePurchaseDate, p.IntSupplierId, p.StrSupplierName } into ab
                                                                            select new ItemWiseDailyPurchaseReportViewModel
                                                                            {
                                                                                IntSupplierId = ab.Key.IntSupplierId,
                                                                                StrSupplierName=ab.Key.StrSupplierName,
                                                                                DtePurchaseDate = ab.Key.DtePurchaseDate.Date,
                                                                                IntItemId = ab.Key.IntItemId,
                                                                                StrItemName=ab.Key.StrItemName,
                                                                                IntItemQuantity = ab.Sum(x=>x.pd.NumItemQuantity),
                                                                                NumUnitPrice = ab.Sum(x=>x.pd.NumUnitPrice),
                                                                                NumTotalPrice=ab.Sum(x=>x.pd.NumTotalPrice)
                                                                            }).ToListAsync();
                return objList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ItemWiseDailyPurchaseReportViewModel>> GetSupplierWiseDailyPurchaseReport([Required] DateTime purchaseDate)
        {
            try
            {
                List<ItemWiseDailyPurchaseReportViewModel> objList = await (from p in _context.TblPurchases
                                                                            join pd in _context.TblPurchaseDetails on p.IntPurchaseId equals pd.IntPurchaseId
                                                                            where p.IsActive == true
                                                                            && pd.IsActive == true
                                                                            && p.DtePurchaseDate.Date == purchaseDate.Date
                                                                            group new { p, pd } by new { p.IntSupplierId, p.StrSupplierName, p.DtePurchaseDate, pd.IntItemId, pd.StrItemName } into ab
                                                                            select new ItemWiseDailyPurchaseReportViewModel
                                                                            {
                                                                                IntSupplierId=ab.Key.IntSupplierId,
                                                                                StrSupplierName=ab.Key.StrSupplierName,
                                                                                IntItemId=ab.Key.IntItemId,
                                                                                StrItemName=ab.Key.StrItemName,
                                                                                DtePurchaseDate=ab.Key.DtePurchaseDate.Date,
                                                                                IntItemQuantity=ab.Sum(x=>x.pd.NumItemQuantity),
                                                                                NumUnitPrice=ab.Sum(x=>x.pd.NumUnitPrice),
                                                                                NumTotalPrice=ab.Sum(x=>x.pd.NumTotalPrice)
                                                                            }).ToListAsync();
                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public async Task<MessageHelper> CreatePurchaseOrder(PurchaseOrderCommonViewModel obj)
        {
            try
            {
                if (obj.purchase.IntPurchaseId > 0)
                {
                    var purchase = await _context.TblPurchases.Where(x => x.IntPurchaseId == obj.purchase.IntPurchaseId).FirstOrDefaultAsync();
                    purchase.IntSupplierId = obj.purchase.IntSupplierId;
                    purchase.StrSupplierName = obj.purchase.StrSupplierName;
                    purchase.DtePurchaseDate = obj.purchase.DtePurchaseDate;
                    purchase.IsActive = obj.purchase.IsActive;

                    _context.TblPurchases.Update(purchase);
                    await _context.SaveChangesAsync();


                    List<TblPurchaseDetail> existingRowList = new List<TblPurchaseDetail>();
                    List<TblPurchaseDetail> newRowList = new List<TblPurchaseDetail>();
                    foreach (var item in obj.purchaseDetails)
                    {
                        if (item.IntPurchaseDetailsId == 0)
                        {
                            TblPurchaseDetail newRowData = new TblPurchaseDetail
                            {
                                IntPurchaseId = purchase.IntPurchaseId,
                                IntItemId = item.IntItemId,
                                StrItemName=item.StrItemName,
                                NumItemQuantity = item.IntItemQuantity,
                                NumUnitPrice = item.NumUnitPrice,
                                IsActive = true,
                                NumTotalPrice=item.NumUnitPrice*item.IntItemQuantity
                            };
                            newRowList.Add(newRowData);
                        }
                        else
                        {
                            var existingRowData = await _context.TblPurchaseDetails.Where(x => x.IsActive == true && x.IntPurchaseDetailsId == item.IntPurchaseDetailsId).FirstOrDefaultAsync();
                            //existingRowData.IntPurchaseId = purchase.IntPurchaseId;
                            existingRowData.IntItemId = item.IntItemId;
                            existingRowData.StrItemName = item.StrItemName;
                            existingRowData.NumItemQuantity = item.IntItemQuantity;
                            existingRowData.NumUnitPrice = item.NumUnitPrice;
                            existingRowData.IsActive = item.IsActive;
                            existingRowData.NumTotalPrice = item.NumUnitPrice * item.IntItemQuantity;

                            existingRowList.Add(existingRowData);
                        }
                    }
                    if (existingRowList.Count > 0)
                    {
                        _context.TblPurchaseDetails.UpdateRange(existingRowList);
                        await _context.SaveChangesAsync();
                    }
                    else if (newRowList.Count > 0)
                    {
                        await _context.TblPurchaseDetails.AddRangeAsync(newRowList);
                        await _context.SaveChangesAsync();
                    }

                    res.Message = "Updated Successfully";
                    return res;

                }
                else
                {
                    TblPurchase purchaseHead = new TblPurchase
                    {
                        IntSupplierId = obj.purchase.IntSupplierId,
                        StrSupplierName=obj.purchase.StrSupplierName,
                        DtePurchaseDate = obj.purchase.DtePurchaseDate,
                        IsActive = true
                    };
                    await _context.TblPurchases.AddAsync(purchaseHead);
                    await _context.SaveChangesAsync();

                    List<TblPurchaseDetail> createPurchaseDetails = new List<TblPurchaseDetail>();
                    foreach (var purchaseDatails in obj.purchaseDetails)
                    {
                        TblPurchaseDetail details = new TblPurchaseDetail
                        {
                            IntPurchaseId = purchaseHead.IntPurchaseId,
                            IntItemId = purchaseDatails.IntItemId,
                            StrItemName=purchaseDatails.StrItemName,
                            NumItemQuantity = purchaseDatails.IntItemQuantity,
                            NumUnitPrice = purchaseDatails.NumUnitPrice,
                            IsActive = true,
                            NumTotalPrice=purchaseDatails.IntItemQuantity*purchaseDatails.NumUnitPrice
                        };
                        createPurchaseDetails.Add(details);
                    }
                    await _context.TblPurchaseDetails.AddRangeAsync(createPurchaseDetails);
                    await _context.SaveChangesAsync();

                    //foreach (var itm in createPurchaseDetails)
                    //{
                    //    var check = await _context.TblItems.Where(x => x.IntItemId == itm.IntItemId && x.IsActive == true).FirstOrDefaultAsync();

                    //    check.NumStockQuantity += itm.NumItemQuantity;
                    //}

                    //List<TblItem> stock = new List<TblItem>();
                    //foreach (var stockItem in createPurchaseDetails)
                    //{
                    //    TblItem itm = new TblItem
                    //    {
                    //        //IntItemId = stockItem.IntItemId,
                    //        StrItemName = stockItem.StrItemName,
                    //        NumStockQuantity = stockItem.NumItemQuantity,
                    //        NumStockPrice = stockItem.NumUnitPrice,
                    //        NumTotalPrice=stockItem.NumUnitPrice*stockItem.NumItemQuantity,
                    //        IsActive = true
                    //    };
                    //    stock.Add(itm);
                    //}
                    //await _context.TblItems.AddRangeAsync(stock);
                    //await _context.SaveChangesAsync();
                }

                res.Message = "Purchase Order Created Successfully";
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
