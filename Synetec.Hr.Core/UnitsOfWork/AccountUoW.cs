using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synetec.Hr.Database.Entities;
using Synetec.Hr.UnitOfWork.BaseUoW;
using Synetec.Hr.UnitOfWork.GenericRepo;

namespace Synetec.Hr.Core.UnitsOfWork
{
    public class AccountUoW : BaseUoW, IAccountUoW
    {
        private IGenericRepo<User> _applicationUserRepository;
        
        public AccountUoW(DbContext context,
            UserManager<User> userManager,
            SignInManager<User> loginManager) : base(context)
        {
            UserManager = userManager;
            SignInManager = loginManager;
        }

        public IGenericRepo<User> ApplicationUserRepository => _applicationUserRepository =
            _applicationUserRepository ?? new GenericRepo<User>(SynetecHrDbContext);
        
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }
    }
}
