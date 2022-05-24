using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.Data.Entity;
using PeopleDeskHomeWork.Models.ViewModel.Item;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Services
{
    public class ItemService:IItemService
    {
        MessageHelper res = new MessageHelper();
        private readonly HomeWorkDbContext _context;
        public ItemService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<MessageHelper> CreateItem(ItemCommonViewModel obj)
        {
            try
            {
                //if(from itm in _context.TblItems where obj.items)
                List<TblItem> editList = new List<TblItem>();
                List<TblItem> objList = new List<TblItem>();
                foreach (var item in obj.items)
                {
                    if (item.IntItemId > 0)
                    {
                        var isExists = await _context.TblItems.Where(x => x.IntItemId != item.IntItemId && x.StrItemName == item.StrItemName).FirstOrDefaultAsync();
                        if (isExists != null)
                        {
                            res.Message = "Item Name Already Exists";
                        }
                        else
                        {
                            var data = await _context.TblItems.Where(x => x.IntItemId == item.IntItemId && x.IsActive == true).FirstOrDefaultAsync();
                            if (data != null)
                            {
                                data.StrItemName = item.StrItemName;
                                data.NumStockQuantity = item.NumStockQuantity;
                                data.NumStockPrice = item.NumUnitPrice;
                                data.NumTotalPrice = item.NumStockQuantity * item.NumUnitPrice;

                                editList.Add(data);
                            }
                            _context.TblItems.UpdateRange(editList);
                            await _context.SaveChangesAsync();
                        }
                        
                    }
                    else
                    {
                        var isExists = await _context.TblItems.Where(x => x.StrItemName == item.StrItemName).FirstOrDefaultAsync();
                        if (isExists != null)
                        {
                            res.Message = "Item Name Already Exists";
                        }
                        else
                        {
                            TblItem data = new TblItem
                            {
                                StrItemName = item.StrItemName,
                                NumStockQuantity = item.NumStockQuantity,
                                IsActive = true
                            };
                            objList.Add(data);
                        }

                    }
                }
                await _context.TblItems.AddRangeAsync(objList);
                await _context.SaveChangesAsync();

                res.Message = "Created Successfully";
                return res;
            }
            catch (Exception ex)
            {

                res.Message = ex.Message;
                return res;
            }
        }
    }
}
