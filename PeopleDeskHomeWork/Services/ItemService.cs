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
                var exist = await (from itm in _context.TblItems
                                   where obj.items.Select(x => x.StrItemName.ToLower()).ToList().Equals(itm.StrItemName)
                                   select itm.StrItemName).ToListAsync();

                if (exist != null)
                {
                    //throw new Exception($"duplicate these items");
                    res.Message += $"duplicate these items {exist}";
                }
                
                List<TblItem> editList = new List<TblItem>();
                List<TblItem> objList = new List<TblItem>();
                foreach (var item in obj.items)
                {
                    if (item.IntItemId > 0)
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
                        
                        //var isExists = await _context.TblItems.Where(x => x.IntItemId != item.IntItemId && x.StrItemName == item.StrItemName).FirstOrDefaultAsync();
                        //if (isExists != null)
                        //{
                        //    res.Message = "Item Name Already Exists";
                        //}
                        //else
                        //{

                        //}

                    }
                    else
                    {
                        TblItem data = new TblItem
                        {
                            StrItemName = item.StrItemName,
                            NumStockQuantity = item.NumStockQuantity,
                            NumStockPrice=item.NumUnitPrice,
                            NumTotalPrice=item.NumTotalPrice,
                            IsActive = true
                        };
                        objList.Add(data);
                        
                        //var isExists = await _context.TblItems.Where(x => x.StrItemName == item.StrItemName).FirstOrDefaultAsync();
                        //if (isExists != null)
                        //{
                        //    res.Message = "Item Name Already Exists";
                        //}
                        //else
                        //{

                        //}

                    }
                }
                if (objList.Count > 0)
                {
                    await _context.TblItems.AddRangeAsync(objList);
                    await _context.SaveChangesAsync();

                    res.Message += $"Created Successfully with item names {objList.Select(x => x.StrItemName)}";
                }
                else if (editList.Count > 0)
                {
                    _context.TblItems.UpdateRange(editList);
                    await _context.SaveChangesAsync();

                    res.Message += $"Updated Successfully with item names {editList.Select(x => x.StrItemName)}";
                }
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
