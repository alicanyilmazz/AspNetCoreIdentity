using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Authentication.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Display(Name = "New Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and password confirm do not match!")]
        [Required(ErrorMessage = "Password confirm can not be empty!")]
        [Display(Name = "New Password Confirm")]
        public string? PasswordConfirm { get; set; }
    }
}
