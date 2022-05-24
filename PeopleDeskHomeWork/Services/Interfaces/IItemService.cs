using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.ViewModel.Item;

namespace PeopleDeskHomeWork.Services.Interfaces
{
    public interface IItemService
    {
        Task<MessageHelper> CreateItem(ItemCommonViewModel obj);
    }
}
