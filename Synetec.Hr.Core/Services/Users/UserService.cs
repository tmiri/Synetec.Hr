using System;
using System.Collections.Generic;
using System.Linq;
using Synetec.Hr.Core.Dtos.Users;
using Synetec.Hr.Core.UnitsOfWork;
using Synetec.Hr.Database.Entities;

namespace Synetec.Hr.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IAccountUoW _accountUoW;

        private bool _disposed;

        public UserService(IAccountUoW accountUoW)
        {
            _accountUoW = accountUoW;
        }
        
        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _accountUoW.ApplicationUserRepository.GetAllAsQueryable();

            return users.Any()
                ? users.Select(x => new UserDto()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList()
                : new List<UserDto>();
        }

        public IEnumerable<string> GetRolesFor(string userId)
        {
            var user = _accountUoW.ApplicationUserRepository.GetByPrimaryKey(userId);

            if(user == null)
                throw new Exception($"user with id {userId} cannot be found");

            var roles = _accountUoW.UserManager.GetRolesAsync(user).Result;

            return roles;
        }

        public bool CreateUser(UserDto userDto)
        {
            var user = _accountUoW.ApplicationUserRepository.Get(x => x.FirstName.Equals(userDto.FirstName,
                StringComparison.InvariantCultureIgnoreCase) &&
                x.LastName.Equals(userDto.LastName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (user != null) return false;
            
            var identityUser = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email
            };

            var result = _accountUoW.UserManager.CreateAsync(identityUser, "Synetec1!").Result;

            if (!result.Succeeded) return false;

            var addToRole = _accountUoW.UserManager.AddToRoleAsync(identityUser, userDto.Role).Result;
            return addToRole.Succeeded;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _accountUoW?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
