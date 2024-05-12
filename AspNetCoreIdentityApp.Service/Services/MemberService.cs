using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Entities.Services.MemberService;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Admin;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.User;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public MemberService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> SignUpAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> SignInAsync(string email, string password, bool rememberMe)
        {
            var errors = new List<IdentityError>();
            var hasUser = await _userManager.FindByEmailAsync(email);
            if (hasUser is null)
            {
                errors.Add(new IdentityError() { Code = "EmailOrPasswordWrong", Description = "Email or password wrong" });
                return IdentityResult.Failed([.. errors]);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, rememberMe, true);
            if (signInResult.Succeeded)
            {
                return IdentityResult.Success;
            }

            if (signInResult.IsLockedOut)
            {
                errors.Add(new IdentityError() { Code = "UserLockedOut", Description = "You can not sign in for 15 minutes." });
                return IdentityResult.Failed([.. errors]);
            }
            var accessFailedCount = await _userManager.GetAccessFailedCountAsync(hasUser);
            errors.Add(new IdentityError() { Code = "EmailOrPasswordWrong", Description = "Email or password wrong." });
            errors.Add(new IdentityError() { Code = "EmailOrPasswordWrongCount", Description = $"Unsuccessful entrance {accessFailedCount}." });
            return IdentityResult.Failed([.. errors]);
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<UserViewModel>>(await _userManager.Users.ToListAsync());
        }
        public async Task<(AppUser? user, IdentityResult result)> GetUserByNameAsync(string userName)
        {
            AppUser? hasUser = await _userManager.FindByNameAsync(userName);
            if (hasUser is null)
            {
                return (hasUser, IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this user name." }));
            }
            return (hasUser, IdentityResult.Success);
        }

        public async Task<(IdentityResult result, string? token, string? userId)> ForgotUserPasswordAsync(string email)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);
            if (hasUser is null)
            {
                return (result: IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this email." }), token: null, userId: null);
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            return (result: IdentityResult.Success, token: resetToken, userId: hasUser.Id);
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(string userId, string token, string password)
        {
            var hasUser = await _userManager.FindByIdAsync(userId);
            if (hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this email." });
            }
            return await _userManager.ResetPasswordAsync(hasUser, token, password);
        }
        public async Task<IdentityResult> ChangeUserPasswordAsync(string userName, string currentPassword, string newPassword)
        {
            var hasUser = await _userManager.FindByNameAsync(userName);
            if (hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this email." });
            }
            var checkCurrentPassword = await _userManager.CheckPasswordAsync(hasUser, currentPassword);
            if (!checkCurrentPassword)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "CurrentPasswordWrong", Description = "Current password is wrong." });
            }
            var checkNewPassword = await _userManager.ChangePasswordAsync(hasUser, currentPassword, newPassword);
            if (!checkNewPassword.Succeeded)
            {
                return checkNewPassword;
            }

            await _userManager.UpdateSecurityStampAsync(hasUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(hasUser, newPassword, true, false);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUserInformationAsync(UserProfileInformationViewModel viewModel, string currentUserName)
        {
            AppUser? hasUser = await _userManager.FindByNameAsync(currentUserName);
            if (hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this user name." });
            }
            hasUser.UserName = viewModel.UserName;
            hasUser.Email = viewModel.Email;
            hasUser.PhoneNumber = viewModel.Phone;
            hasUser.City = viewModel.City;
            hasUser.BirtDate = viewModel.BirtDate;
            hasUser.Gender = viewModel.Gender;
            hasUser.Picture = viewModel.ProfileImage;
            var updateUserInformation = await _userManager.UpdateAsync(hasUser);
            if (!updateUserInformation.Succeeded)
            {
                return updateUserInformation;
            }
            var updateSecurityStamp = await _userManager.UpdateSecurityStampAsync(hasUser);
            if (!updateSecurityStamp.Succeeded)
            {
                return updateSecurityStamp;
            }
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(hasUser, true);
            return updateUserInformation;
        }
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roleManager.CreateAsync(new AppRole() { Name = roleName });
        }
        public async Task<IdentityResult> UpdateRoleAsync(string id,string name)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if (roleToUpdate is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "RoleNotFound", Description = "No registered role was found with this id." });
            }
            roleToUpdate.Name = name;
            return await _roleManager.UpdateAsync(roleToUpdate);
        }
        public async Task<(IdentityResult,AppRole?)> UpdateRoleAsync(RoleUpdate roleUpdate)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(roleUpdate.Id);
            if (roleToUpdate is null)
            {
                return (IdentityResult.Failed(new IdentityError() { Code = "RoleNotFound", Description = "No registered role was found with this id." }),null);
            }
            roleToUpdate.Name = roleUpdate.Name;
            return (await _roleManager.UpdateAsync(roleToUpdate), roleToUpdate);
        }
        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if (roleToUpdate is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "RoleNotFound", Description = "No registered role was found with this id." });
            }
            return await _roleManager.DeleteAsync(roleToUpdate);
        }
        public async Task<(IdentityResult, AppRole?)> GetRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return (IdentityResult.Failed(new IdentityError() { Code = "RoleNotFound", Description = "No registered role was found with this id." }),null);
            }
            return (IdentityResult.Success, role);
        }
        public async Task<IEnumerable<AppRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<IdentityResult> DeleteUserAsync(string userName)
        {
            AppUser? hasUser = await _userManager.FindByNameAsync(userName);
            if (hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this user name." });
            }
            return await _userManager.DeleteAsync(hasUser);
        }
    }
}
