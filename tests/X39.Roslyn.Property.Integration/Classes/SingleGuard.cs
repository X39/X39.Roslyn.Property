namespace TestNamespace;

public partial class SingleGuard
{
    private bool GuardMethod(int oldValue, int newValue) => true;
    [Guard(nameof(GuardMethod))]
    private int _field;
}