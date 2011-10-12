using System;

namespace EFMagicGlue
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
