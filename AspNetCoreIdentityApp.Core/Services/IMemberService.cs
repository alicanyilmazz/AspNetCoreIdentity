using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Entities.Services.MemberService;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Admin;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.User;
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
        public Task<(IdentityResult result, string? token, string? userId)> ForgotUserPasswordAsync(string email);
        public Task<IdentityResult> ResetUserPasswordAsync(string userId, string token, string password);
        public Task<IdentityResult> ChangeUserPasswordAsync(string userName, string currentPassword, string newPassword);
        public Task<(AppUser? user, IdentityResult result)> GetUserByNameAsync(string userName);
        public Task<IdentityResult> UpdateUserInformationAsync(UserProfileInformationViewModel viewModel, string currentUserName);
        public Task<IdentityResult> CreateRoleAsync(string roleName);
        public Task<IEnumerable<AppRole>> GetRolesAsync();
        public Task<(IdentityResult, AppRole?)> GetRoleAsync(string id);
        public Task<IdentityResult> UpdateRoleAsync(string id, string name);
        public Task<(IdentityResult, AppRole?)> UpdateRoleAsync(RoleUpdate roleUpdate);
        public Task<IdentityResult> DeleteRoleAsync(string id);
    }
}
