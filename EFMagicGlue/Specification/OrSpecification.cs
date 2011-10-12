namespace EFMagicGlue.Specification
{
    public class OrSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public OrSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
            : base(leftSide, rightSide)
        {
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return LeftSide.IsSatisfiedBy(entity) || RightSide.IsSatisfiedBy(entity);
        }
    }
}