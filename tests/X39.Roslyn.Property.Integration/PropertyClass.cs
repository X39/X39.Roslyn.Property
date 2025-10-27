using System.ComponentModel.DataAnnotations;

namespace X39.Roslyn.Property.Integration;

[NotifyPropertyChanged, NotifyPropertyChanging]
public partial class ExplicitTypeRangeValidation
{
    public partial int Field { get; set; }
}