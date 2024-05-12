using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Role Name can not be empty!")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
