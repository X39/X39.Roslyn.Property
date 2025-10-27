namespace TestNamespace;

public partial class ValidationStrategyExceptionWithGuardAndNotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
            
    private bool GuardMethod(int oldValue, int newValue) => true;
    [ValidationStrategy(EValidationStrategy.Exception), Guard(nameof(GuardMethod)), NotifyPropertyChanged]
    private int _field;
}