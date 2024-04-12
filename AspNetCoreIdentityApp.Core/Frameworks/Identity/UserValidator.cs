using AspNetCoreIdentityApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Frameworks.Identity
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            if (user.UserName!.Length > 0 && !char.IsLetter(user.UserName![0]))
            {
                errors.Add(new IdentityError() { Code = "UserNameFirstCharacterMustBeLetter", Description = "Username first character must be letter" });
            }
            if (user.UserName!.Contains("admin"))
            {
                errors.Add(new IdentityError() { Code = "UserNameContainAdmin", Description = "Username can not contain admin" });
            }
            if (user.Email!.StartsWith(".com"))
            {
                errors.Add(new IdentityError() { Code = "EmailStartsWithCom", Description = "Email can not start with .com" });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed([.. errors]));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
