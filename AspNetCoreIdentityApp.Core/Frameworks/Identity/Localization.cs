using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Frameworks.Identity
{
    public class Localization
    {
        public string Language { get; set; }
        public Localization() 
        {
            Language = "en";
        }
    }
}
