namespace EFMagicGlue.Specification
{
    public abstract class CompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        protected readonly ISpecification<TEntity> LeftSide;
        protected readonly ISpecification<TEntity> RightSide;

        public CompositeSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
        }

        public abstract bool IsSatisfiedBy(TEntity entity);
    }
}