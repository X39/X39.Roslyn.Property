namespace TestNamespace;

public partial class NotifyPropertyChangingOnField : System.ComponentModel.INotifyPropertyChanging
{
    public event System.ComponentModel.PropertyChangingEventHandler? PropertyChanging;
                                                                 
    [NotifyPropertyChanging]
    private float _field;
}