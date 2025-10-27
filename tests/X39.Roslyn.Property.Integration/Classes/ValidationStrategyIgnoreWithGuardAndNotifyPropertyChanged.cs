namespace TestNamespace;

public partial class ValidationStrategyIgnoreWithGuardAndNotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
            
    private bool GuardMethod(int oldValue, int newValue) => true;
    [ValidationStrategy(EValidationStrategy.Ignore), Guard(nameof(GuardMethod)), NotifyPropertyChanged]
    private int _field;
}