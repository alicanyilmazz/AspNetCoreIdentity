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
        public Task<IdentityResult> SignUpAsync(AppUser user, string password);
        public Task<IdentityResult> SignInAsync(string email, string password, bool rememberMe);
        public Task SignOutAsync();
        public Task<IEnumerable<UserViewModel>> GetUsersAsync();
    }
}
