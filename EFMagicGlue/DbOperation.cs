using System;
using System.Diagnostics;

namespace EFMagicGlue
{
    public class DbOperation : IDbOperation
    {
        public Guid OperationId { get; private set; }

        public DbOperation()
        {
            OperationId = Guid.NewGuid();
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
                ObjectContextManager.CloseAllObjectContexts();
            }
        }
    }
}