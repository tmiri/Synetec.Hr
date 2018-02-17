using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synetec.Hr.Core.Dtos.Administration;
using Synetec.Hr.Core.Services.Administration;
using Synetec.Hr.Web.Models.Administration;

namespace Synetec.Hr.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRole(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View(roleViewModel);

            var roleMap = new RoleDto
            {
                Name = roleViewModel.Name,
                Description = roleViewModel.Description
            };

            var roleCreateed = _roleService.CreateRole(roleMap);

            if (roleCreateed)
            {
                return RedirectToAction("Administration", "Administration");
            }

            ModelState.AddModelError("", "Role not created!");

            return View(roleViewModel);
        }

        public IActionResult EditRole(string roleId)
        {
            var role = _roleService.GetRole(roleId);
            var mapRole = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return View(mapRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View(roleViewModel);

            var roleMap = new RoleDto
            {
                Id = roleViewModel.Id,
                Name = roleViewModel.Name,
                Description = roleViewModel.Description
            };

            _roleService.EditRole(roleMap);

            return RedirectToAction("Administration", "Administration");
        }

        public IActionResult DeleteRole(string roleId)
        {
            var role = _roleService.GetRole(roleId);
            var mapRole = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return View(mapRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRole(RoleViewModel role)
        {
            _roleService.DeleteRole(role.Id);

            return RedirectToAction("Administration", "Administration");
        }

        protected override void Dispose(bool disposing)
        {
            _roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}