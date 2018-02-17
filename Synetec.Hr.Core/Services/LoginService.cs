using System;
using Synetec.Hr.Core.Dtos.Login;
using Synetec.Hr.Core.UnitsOfWork;

namespace Synetec.Hr.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountUoW _accountUoW;
        
        private bool _disposed;

        public LoginService(IAccountUoW accountUoW)
        {
            _accountUoW = accountUoW;
        }
        
        public bool Login(LoginDto login)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));

            var result = _accountUoW.SignInManager.PasswordSignInAsync
                (login.UserName, login.Password,
                    login.RememberMe, false)
                .Result;

            return result.Succeeded;
        }

        public void Logout()
        {
            _accountUoW.SignInManager.SignOutAsync().Wait();
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
