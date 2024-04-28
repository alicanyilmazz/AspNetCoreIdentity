using AspNetCoreIdentityApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Core.ViewModels.Areas.User
{
    public class UserProfileInformationViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public DateTime? BirtDate { get; set; }
        public Gender Gender { get; set; }
        public string ProfileImage { get; set; }
    }
}
