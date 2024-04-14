
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Configuration
{
    public static class ConfigurationManager
    {
        public static IServiceCollection InstallConfigurations(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            static bool IsAssignableToType<T>(TypeInfo typeInfo) => typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;

            IEnumerable<IConfigurationManager> applicationConfigurations = assemblies.SelectMany(x => x.DefinedTypes)
                .Where(IsAssignableToType<IConfigurationManager>)
                .Select(Activator.CreateInstance)
                .Cast<IConfigurationManager>();

            foreach (IConfigurationManager applicationConfiguration in applicationConfigurations)
            {
                applicationConfiguration.Configure(services, configuration);
            }

            return services;
        }
    }
}
