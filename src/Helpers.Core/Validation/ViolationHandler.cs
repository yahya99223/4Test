using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Helpers.Core
{
    public class ViolationHandler
    {
        protected readonly HashSet<IViolation> ViolationsHashSet;

        public ViolationHandler()
        {
            ViolationsHashSet = new HashSet<IViolation>();
        }

        public IViolation[] Violations
        {
            get { return ViolationsHashSet.ToArray(); }
        }

        /// <summary>
        ///     Register list of violations to the current ViolationHandler instance
        /// </summary>
        /// <param name="violationsSubList"></param>
        public virtual void AddRange(IList<IViolation> violationsSubList)
        {
            foreach (var violation in violationsSubList)
            {
                ViolationsHashSet.Add(violation);
            }
        }

        /// <summary>
        ///     Register new violation to the current ViolationHandler instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <param name="violationType"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void AddViolation<T>(Expression<Func<T, object>> keyPath, ViolationType violationType = ViolationType.General, string message = null, ViolationLevel level = ViolationLevel.Error)
        {
            var keyPathString = ReflectionHelpers.GetPropertyPath(keyPath);
            message = message ?? GetAutomatedErrorMessage(typeof(T), keyPathString, violationType);
            var key = GenerateKey(keyPath, violationType);
            ViolationsHashSet.Add(new Violation(level, violationType, key, message));
        }

        public static string GenerateKey<T>(Expression<Func<T, object>> keyPath, ViolationType violationType)
        {
            var keyPathString = ReflectionHelpers.GetPropertyPath(keyPath);

            var key = (string.IsNullOrEmpty(keyPathString))
                ? typeof (T).Name + "_" + violationType
                : typeof (T).Name + "_" + keyPathString.Replace(".", "_") + "_" + violationType;
            return key;
        }

        public bool IsValid
        {
            get { return ViolationsHashSet.All(v => v.Level != ViolationLevel.Error); }
        }


        public static string GetAutomatedErrorMessage(Type baseType,string key, ViolationType violationType, Exception exception = null)
        {
            var applicationException = exception as InternalApplicationException;
            if (applicationException != null)
            {
                return applicationException.Message;
            }
            
            if (string.IsNullOrEmpty(key) && baseType != null)
                key = baseType.Name;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("violation key should be passed to generate suitable message.", "key");

            var messageBuilder = new StringBuilder();
            switch (violationType)
            {
                case ViolationType.Required:
                    if (key.Length > 0)
                    {
                        messageBuilder.Append("The ");
                        messageBuilder.Append(key.Split('.').LastOrDefault());
                        messageBuilder.Append(" is required");
                    }
                    break;
                case ViolationType.Duplicated:
                    messageBuilder.Append("There is already item with the same ");
                    if (key.Length > 0)
                    {
                        messageBuilder.Append(key.Split('.').LastOrDefault());
                    }
                    messageBuilder.Append(" value. This field should be unique");
                    break;
                case ViolationType.Invalid:
                    if (key.Length > 0)
                    {
                        messageBuilder.Append("The value of the ");
                        messageBuilder.Append(key.Split('.').LastOrDefault());
                    }
                    messageBuilder.Append(" is invalid");
                    break;
                case ViolationType.MaxLength:
                    if (key.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(key.Split('.').LastOrDefault());
                        messageBuilder.Append(" is too long");
                    }
                    break;
                case ViolationType.MinLength:
                    if (key.Length > 0)
                    {
                        messageBuilder.Append("The length of the ");
                        messageBuilder.Append(key.Split('.').LastOrDefault());
                        messageBuilder.Append(" is too small");
                    }
                    break;
                default:
                    messageBuilder.Append("There is Violation [");
                    messageBuilder.Append(key + "]");
                    messageBuilder.Append(" it's [" + violationType + "]");
                    break;
            }
            messageBuilder.Append("!.");
            return messageBuilder.ToString();
        }
    }



    public class ViolationHandler<T> : ViolationHandler
    {
        /// <summary>
        ///     Register new violation to the current ViolationHandler instance
        /// </summary>
        public void AddViolation(Expression<Func<T, object>> keyPath, ViolationType violationType = ViolationType.General, string message = null, ViolationLevel level = ViolationLevel.Error)
        {
            base.AddViolation<T>(keyPath, violationType, message, level);
        }


        public void CheckNestedViolations(T instance, Expression<Func<T, IValidatable>> subProperty)
        {
            var validatableItem = ReflectionHelpers.GetPropertyValue(instance, subProperty) as Validatable;
            if (validatableItem != null)
            {
                var validatableList = validatableItem.Validate();
                if (validatableList.Any())
                {
                    var key = ReflectionHelpers.GetPropertyPath(subProperty);
                    var message = GetAutomatedErrorMessage(typeof(T), key, ViolationType.Invalid);
                    var level = ViolationLevel.Info;

                    if (validatableList.Any(i => i.Level == ViolationLevel.Error))
                        level = ViolationLevel.Error;
                    else if (validatableList.Any(i => i.Level == ViolationLevel.Warning))
                        level = ViolationLevel.Warning;

                    ViolationsHashSet.Add(new Violation(level, ViolationType.Invalid, key, message, subViolations: validatableList));
                }
            }
        }


        /// <summary>
        ///     Check the navigation property if it contains any violation and add them as sub violation.
        /// </summary>
        public void CheckNestedViolations(T instance, Expression<Func<T, IEnumerable<IValidatable>>> subProperty)
        {
            var validatableItems = ReflectionHelpers.GetPropertyValue(instance, subProperty) as IEnumerable<Validatable>;
            if (validatableItems != null)
            {
                var validatableList = validatableItems.ToList();
                if (validatableList.Any())
                {
                    var subViolations = new List<IViolation>();
                    for (int index = 0; index < validatableList.Count; index++)
                    {
                        var validatable = validatableList[index];
                        var itemViolations = validatable.Validate();
                        /*foreach (var itemViolation in itemViolations)
                        {
                            itemViolation.Index = index;
                        }*/
                        subViolations.AddRange(itemViolations);
                    }

                    if (subViolations.Any())
                    {
                        var key = ReflectionHelpers.GetPropertyPath(subProperty);
                        var message = GetAutomatedErrorMessage(typeof(T), key, ViolationType.Invalid);
                        var level = ViolationLevel.Info;

                        if (subViolations.Any(i => i.Level == ViolationLevel.Error))
                            level = ViolationLevel.Error;
                        else if (subViolations.Any(i => i.Level == ViolationLevel.Warning))
                            level = ViolationLevel.Warning;

                        ViolationsHashSet.Add(new Violation(level, ViolationType.Invalid, key, message, subViolations: subViolations));
                    }
                }
            }
        }
    }
}