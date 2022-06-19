using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Item;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.MenuPermission;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Purchase;
using PeopleDeskHomeWorkUsingSQL.Services.Item;
using PeopleDeskHomeWorkUsingSQL.Services.MenuPermission;
using PeopleDeskHomeWorkUsingSQL.Services.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Purchase;

namespace PeopleDeskHomeWorkUsingSQL
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPartnerService, PartnerService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IMenuPermissionServicecs, MenuPermissionService>();
        }
    }
}
