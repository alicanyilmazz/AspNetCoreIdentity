using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Configuration
{
    public interface IConfigurationManager
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
