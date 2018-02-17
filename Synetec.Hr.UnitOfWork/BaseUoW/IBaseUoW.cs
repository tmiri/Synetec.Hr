using System;
using System.Threading.Tasks;

namespace Synetec.Hr.UnitOfWork.BaseUoW
{
    public interface IBaseUoW : IDisposable
    {
        int SaveContext();
        Task<int> SaveContextAsync();
    }
}
