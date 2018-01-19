namespace Helpers.Core
{

    public interface IValidatedResult
    {
        /// <summary>
        /// </summary>
        object DataObject { get; }

        /// <summary>
        /// </summary>
        IViolation[] Violations { get; }

        /// <summary>
        /// </summary>
        bool IsValid { get; }

        //IList<IViolation> GetFlatViolations();
    }

    public interface IValidatedResult<T> : IValidatedResult
    {
        T Data { get; }
    }
}