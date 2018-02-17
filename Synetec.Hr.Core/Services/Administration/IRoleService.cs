using System;
using System.Collections.Generic;
using Synetec.Hr.Core.Dtos.Administration;

namespace Synetec.Hr.Core.Services.Administration
{
    public interface IRoleService : IDisposable
    {
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto GetRole(string roleId);
        bool CreateRole(RoleDto role);
        void EditRole(RoleDto roleDto);
        void DeleteRole(string roleId);
    }
}
