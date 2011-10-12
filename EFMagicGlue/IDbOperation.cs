using System;

namespace EFMagicGlue
{
    public interface IDbOperation : IDisposable
    {
        Guid OperationId { get; }
    }
}