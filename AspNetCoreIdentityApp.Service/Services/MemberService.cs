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
        private readonly SignInManager<AppUser> _signInManager;

     
        public MemberService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task<(IdentityResult result, string? token, string? userId)> ForgotUserPassword(string email)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);
            if (hasUser is null)
            {
                return (result: IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this email." }), token: null, userId: null);
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            return (result: IdentityResult.Success, token: resetToken, userId: hasUser.Id);
        }

        public async Task<IdentityResult> ResetUserPassword(string userId, string token, string password)
        {
            var hasUser = await _userManager.FindByIdAsync(userId);
            if (hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "UserNotFound", Description = "No registered user was found with this email." });
            }
            return await _userManager.ResetPasswordAsync(hasUser, token, password);
        }
    }
}
