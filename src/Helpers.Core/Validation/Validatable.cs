using System.Linq;

namespace Helpers.Core
{
    public interface IValidatable
    {
        IViolation[] Validate();
        IViolation[] Violations { get; }
        bool IsValid { get; }
    }

    public abstract class Validatable : IValidatable
    {
        protected readonly ViolationHandler baseViolationHandler;


        public Validatable(ViolationHandler violationHandler)
        {
            this.baseViolationHandler = violationHandler;
        }


        public abstract IViolation[] Validate();
        public abstract IViolation[] Violations { get; }
        public virtual bool IsValid => Violations.All(v => v.Level != ViolationLevel.Error);
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