
using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Data.Context;
using AspNetCoreIdentityApp.Web.Configuration.Settings.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class IdentityServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(IdentitySettings.ConfigureIdentitySettings()).AddEntityFrameworkStores<AppDbContext>();
        }

      
    }
}
