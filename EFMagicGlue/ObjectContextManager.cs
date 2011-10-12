using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace EFMagicGlue
{
    public static class ObjectContextManager
    {
        public static void Init(IObjectContextStorage storage, IObjectContextFactory factory)
        {
            if (storage == null) throw new ArgumentNullException("storage");
            if (factory == null) throw new ArgumentNullException("factory");

            if ((Storage != null) && (Storage != storage))
            {
                throw new ApplicationException("A storage mechanism has already been configured for this application");
            }            
            Storage = storage;
            Factory = factory;
        }

        public static void Clear()
        {
            Storage = null;
            Factory = null;
        }

        /// <summary>
        /// The default connection string name used if only one database is being communicated with.
        /// </summary>
        public static readonly string DefaultConnectionStringName = "DefaultDb";        

        /// <summary>
        /// Used to get the current object context session if you're communicating with a single database.
        /// When communicating with multiple databases, invoke <see cref="CurrentFor()" /> instead.
        /// </summary>
        public static IObjectContext Current
        {
            get
            {
                return CurrentFor(DefaultConnectionStringName);
            }
        }

        /// <summary>
        /// Used to get the current ObjectContext associated with a key; i.e., the key 
        /// associated with an object context for a specific database.
        /// 
        /// If you're only communicating with one database, you should call <see cref="Current" /> instead,
        /// although you're certainly welcome to call this if you have the key available.
        /// </summary>
        public static IObjectContext CurrentFor(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (Storage == null)
            {
                throw new ApplicationException("An IObjectContextStorage has not been initialized");
            }

            IObjectContext context = null;
            lock (SyncLock)
            {
                context = Storage.GetObjectContextForKey(key);

                if (context == null)
                {
                    context = Factory.CreateObjectContextForKey(key);
                    Storage.SetObjectContextForKey(key, context);
                }
            }

            return context;
        }

        /// <summary>
        /// This method is used by application-specific object context storage implementations
        /// and unit tests. Its job is to walk thru existing cached object context(s) and Close() each one.
        /// </summary>
        public static void CloseAllObjectContexts()
        {
            foreach (ObjectContext ctx in Storage.GetAllObjectContexts())
            {
                var context = ctx as IObjectContext;
                if (context == null)
                    continue;

                Storage.SetObjectContextForKey(context.Name, null);
                ctx.Dispose();
            }
        }

        /// <summary>
        /// An application-specific implementation of IObjectContextStorage must be setup either thru
        /// <see cref="InitStorage" /> or one of the <see cref="Init" /> overloads. 
        /// </summary>
        public static IObjectContextStorage Storage { get; set; }

        private static IObjectContextFactory Factory { get; set; }

        private static readonly object SyncLock = new object();
    }
}
