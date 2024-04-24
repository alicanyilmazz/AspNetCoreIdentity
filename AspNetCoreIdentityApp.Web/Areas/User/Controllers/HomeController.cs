using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Web.Areas.User.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IEmailService _emailService;

        public HomeController(IMemberService memberService, IEmailService emailService)
        {
            _memberService = memberService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            (AppUser user, IdentityResult result) = await _memberService.GetUserByNameAsync(User.Identity!.Name!);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.Select(x=>x.Description);
            }
            return View(new UserViewModel { Email = user.Email , PhoneNumber = user.PhoneNumber , UserName = user.UserName});
        }
    }
}
