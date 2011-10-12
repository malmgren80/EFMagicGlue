using System;
using System.Transactions;

using EFMagicGlue.Extensions;

namespace EFMagicGlue
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionScope _transactionScope;

        public UnitOfWork(TransactionScopeOption option)
        {
            // Only create transaction when no ambient transaction exists
            _transactionScope = Transaction.Current == null ? new TransactionScope(option).EnsureDistributed() : null;
        }

        public UnitOfWork()
            : this(TransactionScopeOption.Required)
        {
        }

        public void Commit()
        {
            foreach (var context in ObjectContextManager.Storage.GetAllObjectContexts())
            { 
                context.SaveChanges();
            }

            if (_transactionScope != null)
            { 
                _transactionScope.Complete();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transactionScope != null)
                { 
                    _transactionScope.Dispose();
                }
            }
        }

    }
}
