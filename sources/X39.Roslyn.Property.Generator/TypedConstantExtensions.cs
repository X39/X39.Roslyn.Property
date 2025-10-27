using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace X39.Roslyn.Property.Generator;

public static class TypedConstantExtensions
{
    public static object? GetValue(this TypedConstant self)
    {
        var value = self.Kind switch
        {
            TypedConstantKind.Error => self.Value,
            TypedConstantKind.Primitive => self.Value,
            TypedConstantKind.Enum => self.Value,
            TypedConstantKind.Type => self.Value,
            TypedConstantKind.Array => self.Values.Select(v => v.GetValue()).ToArray(),
            _ => throw new InvalidEnumArgumentException(nameof(self), (int) self.Kind, typeof(TypedConstantKind))
        };
        return value;
    }

    public static string ToCSharp(this TypedConstant self)
    {
        return self.Kind switch
        {
            TypedConstantKind.Error     => Microsoft.CodeAnalysis.CSharp.TypedConstantExtensions.ToCSharpString(self),
            TypedConstantKind.Primitive => Microsoft.CodeAnalysis.CSharp.TypedConstantExtensions.ToCSharpString(self),
            TypedConstantKind.Enum      => Microsoft.CodeAnalysis.CSharp.TypedConstantExtensions.ToCSharpString(self),
            TypedConstantKind.Type      => Microsoft.CodeAnalysis.CSharp.TypedConstantExtensions.ToCSharpString(self),
            TypedConstantKind.Array => string.Concat(
                "new[] { ",
                string.Join(", ", self.Values.First().Values.Select(ToCSharp)),
                " }"
            ),
            _ => throw new InvalidEnumArgumentException(nameof(self), (int) self.Kind, typeof(TypedConstantKind))
        };
    }

    public static string GetFieldName(this ISymbol self)
        => self switch
        {
            IPropertySymbol property => string.Concat(
                "___",
                char.ToLowerInvariant(property.Name[0]),
                property.Name.Substring(1)
            ),
            IFieldSymbol field => field.Name,
            _                  => throw new NotSupportedException(),
        };

    public static ITypeSymbol GetSymbolType(this ISymbol self)
        => self switch
        {
            IPropertySymbol property => property.Type,
            IFieldSymbol field       => field.Type,
            _                        => throw new NotSupportedException(),
        };

    public static bool IsReadOnly(this ISymbol self)
        => self switch
        {
            IPropertySymbol property => false,
            IFieldSymbol field       => field.IsReadOnly,
            _                        => throw new NotSupportedException(),
        };

    public static bool IsNullableFieldSymbol(this ISymbol self)
    {
        switch (self)
        {
            case IFieldSymbol fieldSymbol:
                if (fieldSymbol.NullableAnnotation is NullableAnnotation.Annotated)
                    return true;

                if (fieldSymbol.Type.TypeKind != TypeKind.TypeParameter)
                    return fieldSymbol.NullableAnnotation is NullableAnnotation.None && !fieldSymbol.Type.IsValueType;
                else
                    return fieldSymbol.NullableAnnotation is NullableAnnotation.None && !fieldSymbol.Type.IsValueType;
            case IPropertySymbol propertySymbol:
                if (propertySymbol.NullableAnnotation is NullableAnnotation.Annotated)
                    return true;

                if (propertySymbol.Type.TypeKind != TypeKind.TypeParameter)
                    return propertySymbol.NullableAnnotation is NullableAnnotation.None
                           && !propertySymbol.Type.IsValueType;
                else
                    return propertySymbol.NullableAnnotation is NullableAnnotation.None
                           && !propertySymbol.Type.IsValueType;
            default:
                throw new NotSupportedException();
        }
    }
}