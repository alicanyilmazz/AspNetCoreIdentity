using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Frameworks.Identity;
using AspNetCoreIdentityApp.Data.Context;
using Microsoft.AspNetCore.Identity;


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
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            // Configure the security stamp validation interval
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(30);
            });
        }     
    }
}
