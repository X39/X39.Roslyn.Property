namespace TestNamespace;

public partial class NotifyPropertyChangedOnField : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChanged]
    // ToDo: Add support for the following attributes to the generated code
    [System.ComponentModel.DataAnnotations.Length(0, 1)]
    [System.ComponentModel.DataAnnotations.AllowedValues(new[] { 1, 2, 3 })]
    [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
    [System.ComponentModel.DataAnnotations.DeniedValues(new[] { 4, 5, 6 })]
    private float _field;
}
