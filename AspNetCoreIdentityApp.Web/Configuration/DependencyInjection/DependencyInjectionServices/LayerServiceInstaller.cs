
using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Core.Repositories;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.UnitOfWork;
using AspNetCoreIdentityApp.Data.Repositories;
using AspNetCoreIdentityApp.Data.UnitOfWork;
using AspNetCoreIdentityApp.Service.Services;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class LayerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            services.AddScoped(typeof(IMemberService), typeof(MemberService));
        }
    }
}
