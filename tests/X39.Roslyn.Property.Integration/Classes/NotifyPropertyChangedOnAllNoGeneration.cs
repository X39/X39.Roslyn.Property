namespace TestNamespace;

[NotifyPropertyChanged(false)]
public partial class NotifyPropertyChangedOnAllNoGeneration
    : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
    private float                                                   _field;
}