using System;
using System.Data.Objects;

namespace EFMagicGlue
{
    public interface IObjectContext : IDisposable
    {
        ObjectStateManager ObjectStateManager { get; }
        string Name { get; }

        IObjectSet<T> CreateObjectSet<T>() where T : class;
        int SaveChanges();
    }
}
