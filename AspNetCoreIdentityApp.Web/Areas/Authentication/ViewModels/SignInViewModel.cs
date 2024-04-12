using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Authentication.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email can not be empty!")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can not be empty!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
        public SignInViewModel()
        {
            
        }
        public SignInViewModel(string? email,string? password)
        {
            Email = email;
            Password = password;
        }
    }
}
