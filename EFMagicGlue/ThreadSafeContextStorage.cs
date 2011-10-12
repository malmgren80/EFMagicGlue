using System;
using System.Collections.Generic;

namespace EFMagicGlue
{
    public class ThreadSafeContextStorage : IObjectContextStorage
    {
        private readonly object _syncLock = new object();

        [ThreadStatic] 
        private static IDictionary<string, IObjectContext> _contexts;
        
        public IObjectContext GetObjectContextForKey(string key)
        {
            if (_contexts == null)
                return null;

            return _contexts.ContainsKey(key) ? _contexts[key] : null;
        }

        public void SetObjectContextForKey(string key, IObjectContext objectContext)
        {
            lock (_syncLock)
            {
                if (_contexts == null)
                    _contexts = new Dictionary<string, IObjectContext>();

                _contexts[key] = objectContext;
            }
        }

        public IEnumerable<IObjectContext> GetAllObjectContexts()
        {
            var contexts = new List<IObjectContext>();
            if (_contexts == null)
                return contexts;

            lock (_syncLock)
            {
                foreach (var kvp in _contexts)
                {
                    if (kvp.Value == null)
                        continue;

                    contexts.Add(kvp.Value);
                }
            }
            return contexts;
        }
    }
}