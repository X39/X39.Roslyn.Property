namespace TestNamespace;

public partial class MultipleGuards
{
    private bool GuardMethod1(int oldValue, int newValue) => true;
    private bool GuardMethod2(int oldValue, int newValue) => true;
    [Guard(nameof(GuardMethod1)), Guard(nameof(GuardMethod2))]
    private int _field;
}