using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.ViewModels
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name can not be empty!")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
