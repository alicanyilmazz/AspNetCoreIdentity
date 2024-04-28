
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.User.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Old Password can not be empty!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Display(Name = "Old Password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "New password confirm can not be empty!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "New Password and new password confirm do not match!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Required(ErrorMessage = "New password confirm can not be empty!")]
        [Display(Name = "New Password Confirm")]
        public string NewPasswordConfirm { get; set; }
    }
}
