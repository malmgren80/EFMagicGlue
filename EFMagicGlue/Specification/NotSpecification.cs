namespace EFMagicGlue.Specification
{
    public class NotSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public NotSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
            : base(leftSide, rightSide)
        {
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return LeftSide.IsSatisfiedBy(entity) && !RightSide.IsSatisfiedBy(entity);
        }
    }
}