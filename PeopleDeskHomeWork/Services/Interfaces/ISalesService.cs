using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.ViewModel.Sales;

namespace PeopleDeskHomeWork.Services.Interfaces
{
    public interface ISalesService
    {
        public Task<List<ItemWiseMonthlySalesReportViewModel>> GetItemWiseMonthlySalesReport(DateTime fromDate, DateTime toDate);
        public Task<List<ItemWiseMonthlySalesReportViewModel>> GetCustomerWiseMonthlySalesReport(DateTime fromDate, DateTime toDate);
        public Task<MessageHelper> CreateSalesOrder(SalesOrderCommonViewModel obj);
    }
}
