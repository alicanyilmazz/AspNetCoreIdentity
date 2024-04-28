using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Entities.Enums;
using AspNetCoreIdentityApp.Core.Extensions.Identity.ModelState;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.User;
using AspNetCoreIdentityApp.Web.Areas.User.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreIdentityApp.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IEmailService _emailService;
        private readonly IFileProvider _fileProvider;
        public HomeController(IMemberService memberService, IEmailService emailService, IFileProvider fileProvider)
        {
            _memberService = memberService;
            _emailService = emailService;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            (AppUser user, IdentityResult result) = await _memberService.GetUserByNameAsync(User.Identity!.Name!);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = result.Errors.Select(x => x.Description);
            }

            return View(new UserViewModel { Email = user.Email, PhoneNumber = user.PhoneNumber, UserName = user.UserName, ProfileImageUrl = user.Picture });
        }

        [HttpGet]
        public async Task<IActionResult> PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _memberService.ChangeUserPasswordAsync(User.Identity!.Name!, request.CurrentPassword, request.NewPassword);
            if (!response.Succeeded)
            {
                ModelState.AddModelStateErrors(response.Errors);
                return View();
            }
            TempData["SuccessMessage"] = $"Your password changed successfully.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserInformationChange()
        {
            ViewBag.Genders = new SelectList(Enum.GetNames(typeof(Gender)));
            (AppUser currentUser, IdentityResult result) = await _memberService.GetUserByNameAsync(User.Identity!.Name!);
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }
            var user = new UserInformationChangeViewModel
            {
                UserName = currentUser!.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                City = currentUser.City,
                BirtDate = currentUser.BirtDate,
                Gender = currentUser.Gender
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserInformationChange(UserInformationChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var profileInformation = new UserProfileInformationViewModel
            {
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                BirtDate = request.BirtDate,
                Gender = request.Gender
            };

            if (request.ProfileImage is not null && request.ProfileImage.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");
                var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ProfileImage.FileName)}";
                var newProfileImagePath = Path.Combine(wwwrootFolder.First(x => x.Name == "images").PhysicalPath, randomFileName);
                using var stream = new FileStream(newProfileImagePath, FileMode.Create);
                await request.ProfileImage.CopyToAsync(stream);
                profileInformation.ProfileImage = randomFileName;
            }
            var result = await _memberService.UpdateUserInformationAsync(profileInformation, User.Identity!.Name!);
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = $"Your information updated successfully.";

            var updatedUserInformation = new UserInformationChangeViewModel
            {
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                BirtDate = request.BirtDate,
                Gender = request.Gender
            };
            return View(updatedUserInformation);
        }
    }
}
