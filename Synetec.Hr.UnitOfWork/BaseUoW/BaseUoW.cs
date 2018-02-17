using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Synetec.Hr.UnitOfWork.BaseUoW
{
    public class BaseUoW : IBaseUoW
    {
        private DbContext _context;
        private bool _disposed;

        public BaseUoW(DbContext context)
        {
            _context = context;
        }

        public DbContext SynetecHrDbContext
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("BaseUow: database connection was disposed");
                }

                return _context;
            }
        }

        public int SaveContext()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveContextAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
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
