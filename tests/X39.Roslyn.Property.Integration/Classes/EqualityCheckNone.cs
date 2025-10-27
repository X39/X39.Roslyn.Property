namespace TestNamespace;

public partial class EqualityCheckNone
{
    [EqualityCheck(EEqualityCheckMode.None)]
    private int _field;
}