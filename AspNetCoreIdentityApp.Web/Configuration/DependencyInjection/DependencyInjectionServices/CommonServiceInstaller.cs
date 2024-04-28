using AspNetCoreIdentityApp.Core.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class CommonServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
        }
    }
}
