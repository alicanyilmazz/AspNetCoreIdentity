using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Entities.Services.MemberService;
using AspNetCoreIdentityApp.Core.Extensions.Identity.ModelState;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IMemberService _memberService;

        public RolesController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = (await _memberService.GetRolesAsync()).Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(roles);
        }

        public async Task<IActionResult> Index2()
        {
            var roles = (await _memberService.GetRolesAsync()).Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(roles);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _memberService.CreateRoleAsync(request.Name);
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }
            TempData["SuccessMessage"] = "Role was created successfully.";
            return RedirectToAction(nameof(RolesController.Create));
        }

        public async Task<IActionResult> Update(string id)
        {
            (IdentityResult result, AppRole role) = await _memberService.GetRoleAsync(id);
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }
            return View(new RoleUpdateViewModel { Id = role.Id, Name = role.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleUpdateViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            (IdentityResult result, AppRole role) = await _memberService.UpdateRoleAsync(new RoleUpdate { Id = request.Id, Name = request.Name });
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }
            TempData["SuccessMessage"] = "Role was created successfully.";
            return View(role);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _memberService.DeleteRoleAsync(id);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList().First();
                return BadRequest(new { message = "Role deletion failed.", errorDetail = errors });
            }
            TempData["SuccessMessage"] = "Role was deleted successfully.";

            return Ok(new { message = "Role deleted successfully."});
        }
    }
}
