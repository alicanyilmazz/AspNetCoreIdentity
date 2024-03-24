using System.Reflection;

namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallServices(this IServiceCollection services,IConfiguration configuration,params Assembly[] assemblies)
        {
            static bool IsAssignableToType<T>(TypeInfo typeInfo) => typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;

            IEnumerable<IServiceInstaller> serviceInstallers = assemblies.SelectMany(x => x.DefinedTypes)
                .Where(IsAssignableToType<IServiceInstaller>)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>();

            foreach (IServiceInstaller serviceInstaller in serviceInstallers)
            {
                serviceInstaller.Install(services, configuration);
            }

            return services;

      
        }
    }
}
