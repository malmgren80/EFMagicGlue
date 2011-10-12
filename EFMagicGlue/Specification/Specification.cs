using System;
using System.Linq.Expressions;

namespace EFMagicGlue.Specification
{
    public class Specification<TEntity> : ISpecification<TEntity>
    {
        public Specification(Func<TEntity, bool> predicate)
        {
            _predicate = predicate;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return _predicate.Invoke(entity);
        }

        private readonly Func<TEntity, bool> _predicate;
    }
}
