using AspNetCoreIdentityApp.Core.Repositories;
using AspNetCoreIdentityApp.Core.UnitOfWork;
using AspNetCoreIdentityApp.Data.Context;
using AspNetCoreIdentityApp.Data.Repositories;
using AspNetCoreIdentityApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection.DependencyInjectionServices
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>)); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("SqlServer"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
        }
    }
}
