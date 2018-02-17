using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synetec.Hr.Core.Services.Administration;
using Synetec.Hr.Web.Models.Administration;

namespace Synetec.Hr.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IRoleService _roleService;

        public AdministrationController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Administration()
        {
            var roles = _roleService.GetAllRoles();
            var mapRoles = roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            });

            return View(mapRoles);
        }

        protected override void Dispose(bool disposing)
        {
            _roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}