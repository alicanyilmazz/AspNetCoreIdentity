using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Services
{
    public interface IMemberService
    {
        public Task<IdentityResult> CreateAsync(AppUser user,string password);
        public Task<IEnumerable<UserViewModel>> GetUsersAsync();
    }
}
