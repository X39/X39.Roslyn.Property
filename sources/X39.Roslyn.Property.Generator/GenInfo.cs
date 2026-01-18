using System.Collections.Generic;
using System.Linq;

namespace X39.Roslyn.Property.Generator;

internal sealed class GenInfo
{
    public bool? Generate { get; set; }
    public bool? NotifyPropertyChanged { get; set; }
    public string? NotifyPropertyChangedMethod { get; set; }
    public bool? NotifyPropertyChanging { get; set; }
    public string? NotifyPropertyChangingMethod { get; set; }
    public string? ValidationStrategy { get; set; }
    public string? PropertyName { get; set; }
    public string? PropertyEncapsulation { get; set; }
    public bool? VirtualProperty { get; set; }
    public (string type, string from, string to)? Range { get; set; }
    public int? MaxLength { get; set; }
    public (string mode, string epsilonF, string epsilonD, string? custom)? EqualityCheck { get; set; }
    public List<(string methodName, string? className, object?[]? arguments, bool hasOldValue, bool hasNewValue)> GuardMethods { get; set; } = new();
    public (List<string> attributes, bool inherit)? PropertyAttributes { get; set; }
    public (List<string> attributes, bool inherit)? DisableAttributeTakeover { get; set; }
    public ESetterMode SetterMode { get; set; }
    public EGetterMode GetterMode { get; set; }
    public object? DefaultValue { get; set; }
    public bool DefaultValued { get; set; }

    public GenInfo WithDefaults(GenInfo defaultGenInfo)
    {
        return new GenInfo
        {
            Generate                     = Generate ?? defaultGenInfo.Generate,
            NotifyPropertyChanged        = NotifyPropertyChanged ?? defaultGenInfo.NotifyPropertyChanged,
            NotifyPropertyChangedMethod  = NotifyPropertyChangedMethod ?? defaultGenInfo.NotifyPropertyChangedMethod,
            NotifyPropertyChanging       = NotifyPropertyChanging ?? defaultGenInfo.NotifyPropertyChanging,
            NotifyPropertyChangingMethod = NotifyPropertyChangingMethod ?? defaultGenInfo.NotifyPropertyChangingMethod,
            ValidationStrategy           = ValidationStrategy ?? defaultGenInfo.ValidationStrategy,
            PropertyName                 = PropertyName ?? defaultGenInfo.PropertyName,
            PropertyEncapsulation        = PropertyEncapsulation ?? defaultGenInfo.PropertyEncapsulation,
            VirtualProperty              = VirtualProperty ?? defaultGenInfo.VirtualProperty,
            Range                        = Range ?? defaultGenInfo.Range,
            MaxLength                    = MaxLength ?? defaultGenInfo.MaxLength,
            EqualityCheck                = EqualityCheck ?? defaultGenInfo.EqualityCheck,
            GuardMethods                 = GuardMethods.Count > 0 ? GuardMethods : defaultGenInfo.GuardMethods,
            DisableAttributeTakeover = (DisableAttributeTakeover?.inherit ?? false)
                ? (new List<string>(), true)
                : (defaultGenInfo.DisableAttributeTakeover?.inherit ?? false)
                    ? (new List<string>(), true)
                    : DisableAttributeTakeover is null && defaultGenInfo.DisableAttributeTakeover is not null
                        ? (new List<string>(), false)
                        : DisableAttributeTakeover,
            PropertyAttributes = (PropertyAttributes?.inherit ?? true)
                ? (
                    (PropertyAttributes?.attributes ?? Enumerable.Empty<string>())
                    .Concat(defaultGenInfo.PropertyAttributes?.attributes ?? Enumerable.Empty<string>())
                    .ToList(), true)
                : PropertyAttributes is null && defaultGenInfo.PropertyAttributes is not null
                    ? (new List<string>(), false)
                    : PropertyAttributes,
            GetterMode    = GetterMode,
            SetterMode    = SetterMode,
            DefaultValue  = DefaultValued ? DefaultValue : defaultGenInfo.DefaultValue,
            DefaultValued = DefaultValued || defaultGenInfo.DefaultValued,
        };
    }

    public bool GenerateProperty()
    {
        return Generate is not false
               && (Generate is true
                   || NotifyPropertyChanged is not null
                   || NotifyPropertyChanging is not null
                   || ValidationStrategy is not null
                   || PropertyName is not null
                   || PropertyEncapsulation is not null
                   || VirtualProperty is not null
                   // || Range is not null
                   // || MaxLength is not null
                   || EqualityCheck is not null
                   || GuardMethods.Count > 0
                   || PropertyAttributes is not null
                   || DisableAttributeTakeover is not null
                   || DefaultValued);
    }
}