using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synetec.Hr.Database.Entities;
using Synetec.Hr.UnitOfWork.BaseUoW;
using Synetec.Hr.UnitOfWork.GenericRepo;

namespace Synetec.Hr.Core.UnitsOfWork
{
    public class AdministrationUoW : BaseUoW, IAdministrationUoW
    {
        private IGenericRepo<Role> _roleRepository;

        public AdministrationUoW(DbContext context, RoleManager<Role> roleManager) : base(context)
        {
            RoleManager = roleManager;
        }

        public IGenericRepo<Role> RoleRepository =>
            _roleRepository = _roleRepository ?? new GenericRepo<Role>(SynetecHrDbContext);
        public RoleManager<Role> RoleManager { get; }
    }
}
