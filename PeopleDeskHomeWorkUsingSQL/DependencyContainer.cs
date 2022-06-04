using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Item;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Item;
using PeopleDeskHomeWorkUsingSQL.Services.Partner;

namespace PeopleDeskHomeWorkUsingSQL
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPartnerService, PartnerService>();
            services.AddTransient<IItemService, ItemService>();
        }
    }
}
