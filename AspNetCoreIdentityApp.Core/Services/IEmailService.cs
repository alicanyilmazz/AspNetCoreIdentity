using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Services
{
    public interface IEmailService
    {
        public Task SendPasswordResetEmail(string link, string toEmail);
    }
}
