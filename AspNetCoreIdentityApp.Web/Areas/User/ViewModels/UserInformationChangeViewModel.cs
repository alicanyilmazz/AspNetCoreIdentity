using AspNetCoreIdentityApp.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.User.ViewModels
{
    public class UserInformationChangeViewModel
    {
        [Required(ErrorMessage = "User name can not be empty!")]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email can not be empty!")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone can not be empty!")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Invalid phone number!")]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "BirtDate")]
        public DateTime? BirtDate { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile? ProfileImage { get; set; }
    }
}
