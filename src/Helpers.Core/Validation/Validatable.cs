namespace Helpers.Core
{
    public interface IValidatable
    {
        IViolation[] Validate();
    }

    public abstract class Validatable : IValidatable
    {
        protected readonly ViolationHandler baseViolationHandler;


        public Validatable(ViolationHandler violationHandler)
        {
            this.baseViolationHandler = violationHandler;
        }


        public abstract IViolation[] Validate();
    }



    public abstract class Validatable<TModel> : Validatable
    {
        #region Overrides of Validatable

        protected virtual ViolationHandler<TModel> violationHandler { get; private set; }

        #endregion


        public Validatable() : base(new ViolationHandler<TModel>())
        {
            violationHandler = base.baseViolationHandler as ViolationHandler<TModel>;
        }
    }
}