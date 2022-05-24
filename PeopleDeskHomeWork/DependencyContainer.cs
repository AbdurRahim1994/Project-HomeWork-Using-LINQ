using PeopleDeskHomeWork.Services;
using PeopleDeskHomeWork.Services.Interfaces;

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
        }
    }
}
