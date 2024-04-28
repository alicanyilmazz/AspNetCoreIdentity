using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.Extensions.Identity.ModelState
{
    public static class ModelStateErrorHandler
    {
        public static void AddModelStateErrors(this ModelStateDictionary modelState,List<string> errors)
        {
            errors.ForEach(error => 
            {
                modelState.AddModelError(string.Empty, error);
            });
        }
        public static void AddModelStateErrors(this ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
        {
            errors.ToList().ForEach(error =>
            {
                modelState.AddModelError(string.Empty, error.Description);
            });
        }
    }
}
