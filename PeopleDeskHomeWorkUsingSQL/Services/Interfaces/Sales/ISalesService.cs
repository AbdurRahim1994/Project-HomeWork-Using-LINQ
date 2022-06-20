using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Sales;

namespace PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Sales
{
    public interface ISalesService
    {
        public Task<MessageHelper> CreateSalesOrder(SalesOrderCommonViewModel obj);
    }
}
