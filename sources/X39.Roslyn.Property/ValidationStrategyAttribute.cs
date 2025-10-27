using System;

namespace X39.Roslyn.Property;

#pragma warning disable CS1574, CS1584, CS1581, CS1580
/// <summary>
/// Makes the field or class, annotated with the 'ValidationStrategy' attribute, create
/// Properties which, for a given validation, will behave according to the specified strategy.
/// </summary>
/// <seealso cref="System.ComponentModel.DataAnnotations.MaxLengthAttribute"/>
/// <seealso cref="System.ComponentModel.DataAnnotations.RangeAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ValidationStrategyAttribute : Attribute
{
    /// <summary>
    /// Gets the validation strategy specified for how the property should behave during validation failures.
    /// </summary>
    public EValidationStrategy Strategy { get; }

    /// <summary>
    /// Makes the field or class, annotated with the 'ValidationStrategy' attribute, create
    /// Properties which, for a given validation, will behave according to the specified strategy.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.MaxLengthAttribute"/>
    /// <seealso cref="System.ComponentModel.DataAnnotations.RangeAttribute"/>
    public ValidationStrategyAttribute(EValidationStrategy strategy = EValidationStrategy.Exception)
    {
        Strategy = strategy;
    }
}
#pragma warning restore CS1574, CS1584, CS1581, CS1580
