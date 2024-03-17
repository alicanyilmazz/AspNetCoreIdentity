using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel(string? userName, string? email, string? phone, string? password,string? passwordConfirm)
    {
        [Display(Name = "User Name")]
        public string? UserName { get; set; } = userName;

        [Display(Name = "Email")]
        public string? Email { get; set; } = email;

        [Display(Name = "Phone")]
        public string? Phone { get; set; } = phone;

        [Display(Name = "Password")]
        public string? Password { get; set; } = password;

        [Display(Name = "Password Confirm")]
        public string?  PasswordConfirm { get; set; } = passwordConfirm;
    }
}
