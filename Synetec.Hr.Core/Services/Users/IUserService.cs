using System;
using System.Collections.Generic;
using Synetec.Hr.Core.Dtos.Users;

namespace Synetec.Hr.Core.Services.Users
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDto> GetAllUsers();
        IEnumerable<string> GetRolesFor(string userId);
        bool CreateUser(UserDto userDto);
    }
}
