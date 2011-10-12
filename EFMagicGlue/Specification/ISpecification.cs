using System;
using System.Linq.Expressions;

namespace EFMagicGlue.Specification
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}
