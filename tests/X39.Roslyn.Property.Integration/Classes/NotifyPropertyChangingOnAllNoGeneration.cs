namespace TestNamespace;

[NotifyPropertyChanging(false)]
public partial class NotifyPropertyChangingOnAllNoGeneration
    : System.ComponentModel.INotifyPropertyChanging
{
    public event System.ComponentModel.PropertyChangingEventHandler? PropertyChanging;
    private float                                                    _field;
}