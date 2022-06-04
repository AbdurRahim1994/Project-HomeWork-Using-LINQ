using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Item;

namespace PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Item
{
    public interface IItemService
    {
        public Task<MessageHelper> CreateItem(ItemViewModel obj);
    }
}
