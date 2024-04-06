using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Admin;
using AspNetCoreIdentityApp.Service.DtoMappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
             return await _userManager.CreateAsync(user,password);
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<UserViewModel>>(await _userManager.Users.ToListAsync());
        }
    }
}
