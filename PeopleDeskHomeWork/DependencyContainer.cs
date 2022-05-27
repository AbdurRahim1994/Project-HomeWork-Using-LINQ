using PeopleDeskHomeWork.Services;
using PeopleDeskHomeWork.Services.Interfaces;
using PeopleDeskHomeWork.Services.Interfaces.Report;
using PeopleDeskHomeWork.Services.Report;

namespace PeopleDeskHomeWork
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPartnerService, PartnerService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<IReportService, ReportService>();
        }
    }
}
