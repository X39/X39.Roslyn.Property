using System.ComponentModel.DataAnnotations;

namespace TestNamespace;

public partial class IntRangeValidation
{
    [ValidationStrategy(EValidationStrategy.Exception), Range(5, 6)]
    private int _field;
}