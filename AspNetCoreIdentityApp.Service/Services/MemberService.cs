using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly UserManager<AppUser> _userManager;

        public MemberService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
             return _userManager.CreateAsync(user,password);
        }
    }
}
