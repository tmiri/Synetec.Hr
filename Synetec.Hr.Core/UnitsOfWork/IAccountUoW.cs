using Microsoft.AspNetCore.Identity;
using Synetec.Hr.Database.Entities;
using Synetec.Hr.UnitOfWork.BaseUoW;
using Synetec.Hr.UnitOfWork.GenericRepo;

namespace Synetec.Hr.Core.UnitsOfWork
{
    public interface IAccountUoW : IBaseUoW
    {
        IGenericRepo<User> ApplicationUserRepository { get; }

        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
    }
}
