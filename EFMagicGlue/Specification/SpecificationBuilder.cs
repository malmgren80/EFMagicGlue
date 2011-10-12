using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFMagicGlue.Specification
{
    public class SpecificationBuilder<TEntity>
    {
        private readonly bool _valueWhenEmpty;
        private ISpecification<TEntity> _inner;

        public SpecificationBuilder(bool valueWhenEmpty)
        {
            _valueWhenEmpty = valueWhenEmpty;
        }

        public void And(ISpecification<TEntity> specification)
        {
            if (_inner == null)
                _inner = new Specification<TEntity>(x => true);

            _inner = new AndSpecification<TEntity>(_inner, specification);
        }

        public void Or(ISpecification<TEntity> specification)
        {
            if (_inner == null)
                _inner = new Specification<TEntity>(x => false);

            _inner = new OrSpecification<TEntity>(_inner, specification);
        }

        public void Not(ISpecification<TEntity> specification)
        {
            if (_inner == null)
                throw new NotSupportedException("Failed to build composite specification: First specification can't be Not!");

            _inner = new NotSpecification<TEntity>(_inner, specification);
        }

        public ISpecification<TEntity> Build()
        {
            return _inner ?? (_inner = new Specification<TEntity>(x => _valueWhenEmpty));
        }
    }
}
