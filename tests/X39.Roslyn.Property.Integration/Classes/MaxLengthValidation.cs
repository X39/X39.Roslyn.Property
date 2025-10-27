using System.ComponentModel.DataAnnotations;

namespace TestNamespace;

public partial class MaxLengthValidation
{
    [ValidationStrategy(EValidationStrategy.Exception), MaxLength(123)]
    private string _field;
}