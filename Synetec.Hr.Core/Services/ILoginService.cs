using System;
using Synetec.Hr.Core.Dtos.Login;

namespace Synetec.Hr.Core.Services
{
    public interface ILoginService : IDisposable
    {
        bool Login(LoginDto login);
        void Logout();
    }
}
