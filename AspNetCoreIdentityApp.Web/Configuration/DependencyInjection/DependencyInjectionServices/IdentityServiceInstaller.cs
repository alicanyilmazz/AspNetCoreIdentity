using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Frameworks.Identity;
using AspNetCoreIdentityApp.Data.Context;


namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class IdentityServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(IdentitySettings.ConfigureIdentitySettings())
                .AddPasswordValidator<PasswordValidator>()
                .AddUserValidator<UserValidator>()
                .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>();
        }     
    }
}
