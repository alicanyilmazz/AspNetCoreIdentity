using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Extensions.Identity.ModelState;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Web.Areas.Authentication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberService _memberService;

        public HomeController(ILogger<HomeController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identityResult = await _memberService.SignUpAsync(
            new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone,
            }, request.Password!);
            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Registration was completed successfully.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            ModelState.AddModelStateErrors(identityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "Admin" });

            var response = await _memberService.SignInAsync(request.Email, request.Password, request.RememberMe);

            if (!response.Succeeded)
            {
                ModelState.AddModelStateErrors(response.Errors.Select(x => x.Description).ToList());
                return View();
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task SignOut()
        {
            await _memberService.SignOutAsync();
        }

    }
}
