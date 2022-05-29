using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Partner;

namespace PeopleDeskHomeWorkUsingSQL
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPartnerService, PartnerService>();
        }
    }
}
