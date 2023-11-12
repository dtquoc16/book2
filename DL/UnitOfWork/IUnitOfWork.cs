using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection GetDbConnection { get; }

        DbTransaction GetDbTransaction { get; }

        void BeginTransaction();

        Task BeginTransactionAsync();

        void Rollback();

        Task RollbackAsync();

        void Commit();

        Task CommitAsync();
    }
}
