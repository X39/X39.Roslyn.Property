using System.ComponentModel.DataAnnotations;

namespace TestNamespace;

public partial class DoubleRangeValidation
{
    [ValidationStrategy(EValidationStrategy.Exception), Range(5.1, 6.1)]
    private double _field;
}