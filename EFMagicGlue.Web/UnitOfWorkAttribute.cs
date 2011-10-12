using System;
using System.Transactions;
using System.Web.Mvc;


namespace EFMagicGlue.Web
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class UnitOfWorkAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Has to be threadstatic because of this: http://stackoverflow.com/questions/5335350/unitofwork-in-action-filter-seems-to-be-caching
        /// </summary>
        [ThreadStatic]
        private static IUnitOfWork _unitOfWork;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWork = new UnitOfWork(TransactionScopeOption.Required);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                if (filterContext.Exception == null)
                {
                    _unitOfWork.Commit();
                }
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}