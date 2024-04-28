using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Frameworks.Identity
{   
    /// <summary>
    /// You can customize the error messages by overriding the IdentityErrorDescriber class.
    /// </summary>
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordMismatch()
        {
            //return new IdentityError { Code = "PasswordMismatch", Description = "Incorrect password" };
            return base.PasswordMismatch();
        }
        public override IdentityError DuplicateUserName(string userName)
        { 
            //return new IdentityError { Code = "DuplicateUserName", Description = $"Username {userName} is already taken" };
            return base.DuplicateUserName(userName);
        }
    }
}
