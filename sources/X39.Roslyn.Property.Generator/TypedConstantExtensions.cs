using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace X39.Roslyn.Property.Generator;

public static class TypedConstantExtensions
{
    public static object? GetValue(this TypedConstant self)
    {
        if (self.Kind == TypedConstantKind.Error)
            return null;
        var value = self.Kind switch
        {
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
            TypedConstantKind.Array when self.Type is not null => string.Concat(
                $"new {self.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)} {{ ",
                string.Join(", ", self.Values.Select(ToCSharp)),
                " }"
            ),
            TypedConstantKind.Array => string.Concat("[", string.Join(", ", self.Values.Select(ToCSharp)), "]"),
            _ => throw new InvalidEnumArgumentException(nameof(self), (int) self.Kind, typeof(TypedConstantKind))
        };
    }

    public static string ToCSharp(this object? self)
    {
        return self switch
        {
            TypedConstant value  => value.ToCSharp(),
            null                 => "null",
            bool b               => b ? "true" : "false",
            bool[] array         => string.Concat(array.Select(e => e.ToCSharp())),
            byte value           => value.ToString(),
            sbyte value          => value.ToString(),
            short value          => value.ToString(),
            ushort value         => value.ToString(),
            int value            => value.ToString(),
            uint value           => value.ToString(),
            long value           => value.ToString(),
            ulong value          => value.ToString(),
            float value          => value.ToString(CultureInfo.InvariantCulture),
            double value         => value.ToString(CultureInfo.InvariantCulture),
            decimal value        => value.ToString(CultureInfo.InvariantCulture),
            char value           => $"'{value}'",
            string { Length: 0 } => "\"\"",
            string value => value.All(c => c is not ('\0' or '\\' or '\n' or '\r' or '"'))
                ? string.Concat("\"", value, "\"")
                : string.Concat("\"\"\"", value, "\"\"\""),
            string[] values => string.Concat(
                "new string[] { ",
                string.Join(", ", values.Select(e => e.ToCSharp())),
                " }"
            ),
            Enum value     => string.Concat(value.GetType().FullName, ".", Enum.GetName(value.GetType(), value)),
            Type value     => string.Concat(value.FullName),
            byte[] values  => string.Concat("new byte[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            sbyte[] values => string.Concat("new sbyte[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            short[] values => string.Concat("new short[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            ushort[] values => string.Concat(
                "new ushort[] {",
                string.Join(", ", values.Select(e => e.ToCSharp())),
                "}"
            ),
            int[] values   => string.Concat("new int[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            uint[] values  => string.Concat("new uint[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            long[] values  => string.Concat("new long[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            ulong[] values => string.Concat("new ulong[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            float[] values => string.Concat("new float[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            double[] values => string.Concat(
                "new double[] { ",
                string.Join(", ", values.Select(e => e.ToCSharp())),
                " }"
            ),
            decimal[] values => string.Concat(
                "new decimal[] { ",
                string.Join(", ", values.Select(e => e.ToCSharp())),
                " }"
            ),
            char[] values  => string.Concat("new char[] { ", string.Join(", ", values.Select(e => e.ToCSharp())), " }"),
            object[] array => string.Concat("new ", array.GetCSharpType()," { ", string.Join(", ", array.Select(e => e.ToCSharp())), " }"),
            _              => throw new Exception($"Value {self} has no CSharp representation"),
        };
    }

    public static string GetCSharpType(this object? self)
    {
        static string SolveForArray(object[] array)
        {
            if (array.Length == 0)
                return "object[]";
            var types = array.Select(e => e.GetCSharpType()).Distinct().ToArray();
            return types.Length is 1 ? string.Concat(types[0], "[]") : "object[]";
        }
        return self switch
        {
            string         => "string",
            char           => "char",
            bool           => "bool",
            byte           => "byte",
            sbyte          => "sbyte",
            short          => "short",
            ushort         => "ushort",
            int            => "int",
            uint           => "uint",
            long           => "long",
            ulong          => "ulong",
            float          => "float",
            double         => "double",
            decimal        => "decimal",
            string[]       => "string",
            char[]         => "char",
            bool[]         => "bool",
            byte[]         => "byte",
            sbyte[]        => "sbyte",
            short[]        => "short",
            ushort[]       => "ushort",
            int[]          => "int",
            uint[]         => "uint",
            long[]         => "long",
            ulong[]        => "ulong",
            float[]        => "float",
            double[]       => "double",
            decimal[]      => "decimal",
            object[] array => SolveForArray(array),
            _ => "object",
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