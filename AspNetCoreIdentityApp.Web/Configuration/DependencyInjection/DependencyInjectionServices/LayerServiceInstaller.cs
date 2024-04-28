﻿
using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Core.Repositories;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.UnitOfWork;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Authentication;
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
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));    
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        }
    }
}
