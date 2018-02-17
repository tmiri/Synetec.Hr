using System;
using System.Collections.Generic;
using System.Linq;
using Synetec.Hr.Core.Dtos.Administration;
using Synetec.Hr.Core.UnitsOfWork;
using Synetec.Hr.Database.Entities;

namespace Synetec.Hr.Core.Services.Administration
{
    public class RoleService : IRoleService
    {
        private readonly IAdministrationUoW _administrationUoW;

        private bool _disposed;

        public RoleService(IAdministrationUoW administrationUoW)
        {
            _administrationUoW = administrationUoW;
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _administrationUoW.RoleRepository.GetAllAsQueryable();

            return roles.Any()
                ? roles.Select(x => new RoleDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name
                }).ToList()
                : new List<RoleDto>();
        }

        public RoleDto GetRole(string roleId)
        {
            var role = _administrationUoW.RoleRepository.Get(x => x.Id == roleId).FirstOrDefault();

            if(role == null)
                throw new Exception($"Role with id {roleId} does not exist");

            return new RoleDto()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public bool CreateRole(RoleDto role)
        {
            var roleExists = _administrationUoW.RoleManager.RoleExistsAsync(role.Name).Result;

            if (roleExists) return false;

            var identityRole = new Role()
            {
                Name = role.Name,
                Description = role.Description
            };

            var result = _administrationUoW.RoleManager.CreateAsync(identityRole).Result;
            return result.Succeeded;
        }

        public void EditRole(RoleDto roleDto)
        {
            var role = _administrationUoW.RoleRepository.Get(x => x.Id == roleDto.Id).FirstOrDefault();

            if (role == null)
                throw new Exception($"Role with id {roleDto.Id} does not exist");

            role.Name = roleDto.Name;
            role.Description = roleDto.Description;

            _administrationUoW.RoleRepository.Update(role);
            _administrationUoW.SaveContext();
        }

        public void DeleteRole(string roleId)
        {
            var role = _administrationUoW.RoleRepository.Get(x => x.Id == roleId).FirstOrDefault();

            if (role == null)
                throw new Exception($"Role with id {roleId} does not exist");
            
            _administrationUoW.RoleManager.DeleteAsync(role);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _administrationUoW?.Dispose();
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
