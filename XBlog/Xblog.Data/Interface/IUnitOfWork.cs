using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xblog.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
    {
    }
}
