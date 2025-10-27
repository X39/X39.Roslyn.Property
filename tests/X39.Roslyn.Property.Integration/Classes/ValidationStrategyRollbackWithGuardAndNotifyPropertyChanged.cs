namespace TestNamespace;

public partial class ValidationStrategyRollbackWithGuardAndNotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
            
    private bool GuardMethod(int oldValue, int newValue) => true;
    [ValidationStrategy(EValidationStrategy.Rollback), Guard(nameof(GuardMethod)), NotifyPropertyChanged]
    private int _field;
}