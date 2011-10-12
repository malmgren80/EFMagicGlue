using System.Collections.Generic;

namespace EFMagicGlue
{
    /// <summary>
    /// Simple object context storage implementation
    /// </summary>
    public class SimpleObjectContextStorage : IObjectContextStorage
    {
        private readonly Dictionary<string, IObjectContext> _storage = new Dictionary<string, IObjectContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleObjectContextStorage"/> class.
        /// </summary>
        public SimpleObjectContextStorage() { }

        /// <summary>
        /// Returns the object context associated with the specified key or
		/// null if the specified key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IObjectContext GetObjectContextForKey(string key)
        {
            IObjectContext context;
            if (!this._storage.TryGetValue(key, out context))
                return null;
            return context;
        }


        /// <summary>
        /// Stores the object context into a dictionary using the specified key.
        /// If an object context already exists by the specified key, 
        /// it gets overwritten by the new object context passed in.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectContext">The object context.</param>
        public void SetObjectContextForKey(string key, IObjectContext objectContext)
        {
            this._storage[key] = objectContext;
        }

        /// <summary>
        /// Returns all the values of the internal dictionary of object contexts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IObjectContext> GetAllObjectContexts()
        {
            var contexts = new List<IObjectContext>();
            foreach (var kvp in _storage)
            {
                if (kvp.Value == null)
                    continue;

                contexts.Add(kvp.Value);
            }

            return contexts;
        }
    }
}
