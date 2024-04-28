
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;

namespace AspNetCoreIdentityApp.Web.Configuration.Settings.Cookie
{
    public class CookieSettings : Core.Configuration.IConfigurationManager
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureApplicationCookie(options =>
            {
                var cookie = new CookieBuilder();
                cookie.Name = configuration["CookieSettings:Name"] ?? "CustomCookie";
                options.LoginPath = configuration["CookieSettings:LoginPath"] ?? new PathString("/Authentication/Home/SignIn");
                options.LogoutPath = configuration["CookieSettings:LogoutPath"] ?? new PathString("/Authentication/Home/SignOut");
                options.Cookie = cookie;
                options.ExpireTimeSpan = TimeSpan.FromDays(Convert.ToInt16(configuration["CookieSettings:Expiration"]));
                options.SlidingExpiration = Convert.ToBoolean(configuration["CookieSettings:SlidingExpiration"]);
            });
        }
    }
}
