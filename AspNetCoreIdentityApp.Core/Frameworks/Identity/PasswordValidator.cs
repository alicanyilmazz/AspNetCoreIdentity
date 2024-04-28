using AspNetCoreIdentityApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Frameworks.Identity
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new IdentityError() { Code="PasswordContainUserName",Description="Password can not be contain username"});   
            }
            if (password!.ToLower().Contains(user.Email!.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainEmail", Description = "Password can not be contain email" });
            }
            if (password.Length < 6)
            {
                errors.Add(new IdentityError() { Code = "PasswordLength", Description = "Password must be at least 6 characters long" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed([.. errors]));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
