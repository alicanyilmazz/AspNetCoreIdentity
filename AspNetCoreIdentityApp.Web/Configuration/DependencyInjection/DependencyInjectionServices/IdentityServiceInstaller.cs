
using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Data.Context;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class IdentityServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
