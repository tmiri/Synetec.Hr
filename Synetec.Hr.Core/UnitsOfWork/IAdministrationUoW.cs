using Microsoft.AspNetCore.Identity;
using Synetec.Hr.Database.Entities;
using Synetec.Hr.UnitOfWork.BaseUoW;
using Synetec.Hr.UnitOfWork.GenericRepo;

namespace Synetec.Hr.Core.UnitsOfWork
{
    public interface IAdministrationUoW : IBaseUoW
    {
        IGenericRepo<Role> RoleRepository { get; }
        RoleManager<Role> RoleManager { get; }
    }
}
