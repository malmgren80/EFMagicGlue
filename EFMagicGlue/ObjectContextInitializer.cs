using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFMagicGlue
{
    public class ObjectContextInitializer
    {
        private static readonly object SyncLock = new object();
        private bool _isInitialized;

        private ObjectContextInitializer() { }

        private static ObjectContextInitializer _instance;
        public static ObjectContextInitializer Instance()
        {
            if (_instance == null) 
            {
                lock (SyncLock)
                {
                    if (_instance == null)
                        _instance = new ObjectContextInitializer();
                }
            }

            return _instance;
        }

        /// <summary>
        /// This is the method which should be given the call to intialize the ObjectContext; e.g.,
        /// ObjectContextInitializer.Instance().InitializeObjectContextOnce(() => InitializeObjectContext());
        /// where InitializeObjectContext() is a method which calls ObjectContextManager.Init()
        /// </summary>
        /// <param name="initMethod"></param>
        public void InitializeObjectContextOnce(Action initMethod) {
            lock (SyncLock) 
            {
                if (_isInitialized)
                {
                    // TODO: Log warning: already initialized
                    return;
                }

                initMethod();
                _isInitialized = true;
            }
        }

    }
}
