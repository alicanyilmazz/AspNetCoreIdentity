using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Authentication.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email can not be empty!")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public ForgotPasswordViewModel()
        {
            
        }

        public ForgotPasswordViewModel(string? email)
        {
            Email = email;
        }
    }
}
