using System.ComponentModel.DataAnnotations;

namespace TestNamespace;

public partial class ExplicitTypeRangeValidation
{
    [ValidationStrategy(EValidationStrategy.Exception), Range(typeof(int), "5", "6")]
    private int _field;
}