namespace EFMagicGlue.Specification
{
    public class AndSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public AndSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
            : base(leftSide, rightSide)
        {
        }

        public override bool IsSatisfiedBy(TEntity obj)
        {
            return LeftSide.IsSatisfiedBy(obj) && RightSide.IsSatisfiedBy(obj);
        }
    }
}