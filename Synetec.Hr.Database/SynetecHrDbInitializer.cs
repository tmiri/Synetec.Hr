using System;
using Microsoft.AspNetCore.Identity;
using Synetec.Hr.Database.Entities;

namespace Synetec.Hr.Database
{
    public static class SynetecHrDbInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync
                    ("admin").Result == null)
            {
                var user = new User
                {
                    UserName = "admin@synetec.co.uk",
                    Email = "admin@synetec.co.uk",
                    FirstName = "Synetec",
                    LastName = "Admin",
                    Position = "System Administrator",
                    StartDate = new DateTime(1960, 1, 1),
                    DaysCarriedOver = 0,
                    LeaveAllowance = 0,
                    LineManager = "None",
                    ChangePassword = false
                };

                var result = userManager.CreateAsync
                    (user, "Synetec1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Administrator").Wait();
                }

                var user2 = new User
                {
                    UserName = "gsnell@synetec.co.uk",
                    Email = "gsnell@synetec.co.uk",
                    FirstName = "Gregory",
                    LastName = "Scotchburn-Snell",
                    Position = "CTO & Head of Development",
                    StartDate = new DateTime(2013, 7, 11),
                    DaysCarriedOver = 0,
                    LeaveAllowance = 0,
                    LineManager = "None",
                    ChangePassword = true
                };

                result = userManager.CreateAsync
                    (user2, "Synetec1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Line Manager").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("Employee").Result)
            {
                var role = new Role
                {
                    Name = "Employee",
                    Description = "Default role for employees."
                };

                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Administrator").Result)
            {
                var role = new Role
                {
                    Name = "Administrator",
                    Description = "Power user of the application."
                };

                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("HR Manager").Result)
            {
                var role = new Role
                {
                    Name = "HR Manager",
                    Description = "HR Manager role"
                };

                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Line Manager").Result)
            {
                var role = new Role
                {
                    Name = "Line Manager",
                    Description = "Line Manager role"
                };

                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Resource Manager").Result)
            {
                var role = new Role
                {
                    Name = "Line Manager",
                    Description = "Line Manager role"
                };

                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }
    }
}
