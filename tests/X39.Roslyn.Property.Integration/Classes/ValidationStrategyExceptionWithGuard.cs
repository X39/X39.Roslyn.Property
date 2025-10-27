namespace TestNamespace;

public partial class ValidationStrategyExceptionWithGuard
{
    private bool GuardMethod(int oldValue, int newValue) => true;
    [ValidationStrategy(EValidationStrategy.Exception), Guard(nameof(GuardMethod))]
    private int _field;
}