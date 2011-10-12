using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace EFMagicGlue
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionStringName;
        private IObjectSet<T> _objectSet;

        public Repository()
            : this(string.Empty)
        {
        }

        public Repository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

    	public IObjectContext Context
        {
            get { return string.IsNullOrEmpty(_connectionStringName) ? ObjectContextManager.Current : ObjectContextManager.CurrentFor(_connectionStringName); }
        }

        protected IObjectSet<T> ObjectSet
        {
            get 
            { 
                if (_objectSet == null)
                    _objectSet = Context.CreateObjectSet<T>();

                return _objectSet;
            }
        }

        public IQueryable<T> AsQueryable()
        {
            return ObjectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return ObjectSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return ObjectSet.Where(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return ObjectSet.Single(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return ObjectSet.First(where);
        }

        public void Delete(T entity)
        {
            ObjectSet.DeleteObject(entity);
        }

        public void Add(T entity)
        {
            ObjectSet.AddObject(entity);
        }

        public void Attach(T entity)
        {
            ObjectSet.Attach(entity);
        }

        public void Update(T entity)
        {
            ObjectStateEntry entityEntry = Context.ObjectStateManager.GetObjectStateEntry(entity); 
            entityEntry.ChangeState(EntityState.Modified); 
        }
    }

}
