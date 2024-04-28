using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Authentication.ViewModels
{
    public class SignUpViewModel
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

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and password confirm do not match!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [MaxLength(16, ErrorMessage = "Password can not be longer than 16 characters!")]
        [Required(ErrorMessage = "Password confirm can not be empty!")]
        [Display(Name = "Password Confirm")]
        public string? PasswordConfirm { get; set; }
        public SignUpViewModel()
        {

        }
        public SignUpViewModel(string? userName, string? email, string? phone, string? password, string? passwordConfirm)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }
    }
}
