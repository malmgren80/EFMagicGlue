using System;
using System.Configuration;

namespace EFMagicGlue
{
    public static class EntityFrameworkInitializer
    {
        private static IObjectContextStorage _storage;

        public static bool EnableProfiler
        {
            get
            {
                string efProfConfigSetting = ConfigurationManager.AppSettings.Get("efprof.enabled");
                bool enableProfiler;
                if (!Boolean.TryParse(efProfConfigSetting, out enableProfiler))
                    return false;

                return enableProfiler;
            }
        }

        public static IObjectContextFactory ObjectContextFactory
        {
            get { return null; }
        }

        public static void Init(IObjectContextStorage storage)
        {
            _storage = storage;

            ObjectContextInitializer.Instance().InitializeObjectContextOnce(InitializeObjectContexts);
        }

        private static void InitializeObjectContexts()
        {
            ObjectContextManager.Init(_storage, ObjectContextFactory);
        }
    }
}