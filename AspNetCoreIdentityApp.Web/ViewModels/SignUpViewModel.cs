namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel(string? userName, string? email, string? phone, string? password)
    {
        public string? UserName { get; set; } = userName;
        public string? Email { get; set; } = email;
        public string? Phone { get; set; } = phone;
        public string? Password { get; set; } = password;
    }
}
