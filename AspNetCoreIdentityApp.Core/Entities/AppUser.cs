using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? City { get; set; }
        public string? Picture { get; set; }
        public DateTime? BirtDate { get; set; }
        public byte? Gender { get; set; }
    }
}
