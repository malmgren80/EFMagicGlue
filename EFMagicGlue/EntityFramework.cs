using System;
using System.Configuration;

namespace EFMagicGlue
{
    public static class EntityFramework
    {
        private static IObjectContextStorage _storage;
        private static IObjectContextFactory _factory;
       
        public static void Init(IObjectContextStorage storage, IObjectContextFactory factory)
        {
            _storage = storage;
            _factory = factory;

            ObjectContextInitializer.Instance().InitializeObjectContextOnce(InitializeObjectContexts);
        }

        private static void InitializeObjectContexts()
        {
            ObjectContextManager.Init(_storage, _factory);
        }
    }
}