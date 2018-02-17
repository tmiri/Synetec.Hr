using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synetec.Hr.Core.Dtos.Users;
using Synetec.Hr.Core.Services.Administration;
using Synetec.Hr.Core.Services.Users;
using Synetec.Hr.Web.Models.Users;

namespace Synetec.Hr.Web.Controllers
{
    [Authorize(Roles = "Administrator, HR Manager")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Dashboard()
        {
            var users = _userService.GetAllUsers();
            var usersModel = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = _userService.GetRolesFor(user.Id);
                usersModel.Add(new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = roles.FirstOrDefault()
                });
            }

            return View(usersModel);
        }

        public IActionResult CreateUser()
        {
            var roles = _roleService.GetAllRoles();
            var model = new UserViewModel()
            {
                Roles = roles.Select(x => new SelectListItem()
                {
                    Value = x.Name,
                    Text = x.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View(userViewModel);

            var userMap = new UserDto
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Role = userViewModel.Role
            };

            var userCreated = _userService.CreateUser(userMap);

            if (userCreated)
            {
                return RedirectToAction("Dashboard", "Users");
            }

            ModelState.AddModelError("", "User not created!");

            var roles = _roleService.GetAllRoles();
            userViewModel.Roles = roles.Select(x => new SelectListItem()
            {
                Value = x.Name,
                Text = x.Name
            });

            return View(userViewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            _roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}