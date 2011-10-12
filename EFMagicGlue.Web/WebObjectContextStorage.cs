using System;
using System.Collections.Generic;
using System.Web;

namespace EFMagicGlue.Web
{
    /// <example>    
    ///  In web application, esp. MVC app, this is how an ObjectContext is initialized and stored in WebObjectContextStorage.
    ///  This is, again, inspired from S#arp Architect code
    ///  
    /// using Infrastructure.Data;
    /// public class MvcApplication : System.Web.HttpApplication
    /// {
    ///     // other code ...
    ///     public override void Init()
    ///     {
    ///         base.Init();            
    ///         _storage = new WebObjectContextStorage(this);
    ///     }        
    ///      
    ///     protected void Application_BeginRequest(object sender, EventArgs e)
    ///     {
    ///         ObjectContextInitializer.Instance().InitializeObjectContextOnce(() =>
    ///         {
    ///             ObjectContextManager.InitStorage(_storage);
    ///             ObjectContextManager.Init(new[] { Server.MapPath("~/bin/mapping-assembly.dll") });
    ///         });
    ///     }
    ///     private WebObjectContextStorage _storage;
    /// }
    /// </example>    
    public class WebObjectContextStorage : IObjectContextStorage
    {   
        public WebObjectContextStorage(HttpApplication app)
        {
            app.EndRequest += HttpApplication_EndRequest;
        }

        private void  HttpApplication_EndRequest(object sender, EventArgs e)
        {
 	        ObjectContextManager.CloseAllObjectContexts();
            HttpContext.Current.Items.Remove(HttpContextObjectContextStorageKey);
        }        

        public IObjectContext GetObjectContextForKey(string key)
        {
            SimpleObjectContextStorage storage = GetSimpleObjectContextStorage();
            return storage.GetObjectContextForKey(key);
        }

        public void SetObjectContextForKey(string factoryKey, IObjectContext session)
        {
            SimpleObjectContextStorage storage = GetSimpleObjectContextStorage();
            storage.SetObjectContextForKey(factoryKey, session);
        }

        public IEnumerable<IObjectContext> GetAllObjectContexts()
        {
            SimpleObjectContextStorage storage = GetSimpleObjectContextStorage();
            return storage.GetAllObjectContexts();
        }

        private SimpleObjectContextStorage GetSimpleObjectContextStorage()
        {
            HttpContext context = HttpContext.Current;
            SimpleObjectContextStorage storage = context.Items[HttpContextObjectContextStorageKey] as SimpleObjectContextStorage;
            if (storage == null)
            {
                storage = new SimpleObjectContextStorage();
                context.Items[HttpContextObjectContextStorageKey] = storage;
            }
            return storage;
        }       

        private static readonly string HttpContextObjectContextStorageKey = "HttpContextObjectContextStorageKey";       
    }
}
