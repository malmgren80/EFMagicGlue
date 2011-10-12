using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFMagicGlue
{
    public interface IRepository<T> where T : class
    {
		IObjectContext Context { get; }
        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        void Delete(T entity);
        void Add(T entity);
        void Attach(T entity);
        void Update(T entity);
    }
}
