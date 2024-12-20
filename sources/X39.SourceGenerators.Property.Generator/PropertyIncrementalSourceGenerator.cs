using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace X39.SourceGenerators.Property.Generator;

/// <summary>
/// A sample source generator that creates a custom report based on class properties. The target class should be annotated with the 'Generators.ReportAttribute' attribute.
/// When using the source code as a baseline, an incremental source generator is preferable because it reduces the performance overhead.
/// </summary>
[Generator]
public class PropertyIncrementalSourceGenerator : IIncrementalGenerator
{
    private const string RangeAttribute     = "System.ComponentModel.DataAnnotations.RangeAttribute";
    private const string MaxLengthAttribute = "System.ComponentModel.DataAnnotations.MaxLengthAttribute";
    private const string GeneratePropertiesAttribute = "X39.SourceGenerators.Property.GeneratePropertiesAttribute";

    private const string NotifyOnAttribute = "X39.SourceGenerators.Property.NotifyOnAttribute";
    private const string GetterAttribute   = "X39.SourceGenerators.Property.GetterAttribute";
    private const string SetterAttribute   = "X39.SourceGenerators.Property.SetterAttribute";

    private const string NoPropertyAttribute = "X39.SourceGenerators.Property.NoPropertyAttribute";

    // ReSharper disable once InconsistentNaming
    private const string NotifyPropertyChangedAttribute =
        "X39.SourceGenerators.Property.NotifyPropertyChangedAttribute";

    // ReSharper disable once InconsistentNaming
    private const string NotifyPropertyChangingAttribute =
        "X39.SourceGenerators.Property.NotifyPropertyChangingAttribute";

    private const string ValidationStrategyAttribute = "X39.SourceGenerators.Property.ValidationStrategyAttribute";

    private const string PropertyNameAttribute = "X39.SourceGenerators.Property.PropertyNameAttribute";

    private const string PropertyAttributeAttribute = "X39.SourceGenerators.Property.PropertyAttributeAttribute";

    private const string DisableAttributeTakeoverAttribute =
        "X39.SourceGenerators.Property.DisableAttributeTakeoverAttribute";

    private const string PropertyEncapsulationAttribute =
        "X39.SourceGenerators.Property.PropertyEncapsulationAttribute";

    private const string VirtualPropertyAttribute = "X39.SourceGenerators.Property.VirtualPropertyAttribute";

    private const string EqualityCheckAttribute = "X39.SourceGenerators.Property.EqualityCheckAttribute";

    private const string GuardAttribute = "X39.SourceGenerators.Property.GuardAttribute";

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classSyntaxProvider = context
            .SyntaxProvider
            .CreateSyntaxProvider(
                (s, _) => s is ClassDeclarationSyntax,
                (ctx, _) => GetClassDeclarationForSourceGen(ctx)
            )
            .Where(t => t.generateProperty)
            .Select((t, _) => t.Item1);

        // Generate the source code.
        context.RegisterSourceOutput(
            context.CompilationProvider.Combine(classSyntaxProvider.Collect()),
            ((ctx, t) => GenerateCode(ctx, t.Left, t.Right))
        );
    }

    /// <summary>
    /// Checks whether the Node is annotated with the [Report] attribute and maps syntax context to the specific node type (ClassDeclarationSyntax).
    /// </summary>
    /// <param name="context">Syntax context, based on CreateSyntaxProvider predicate</param>
    /// <returns>The specific cast and whether the attribute was found.</returns>
    private static (ClassDeclarationSyntax, bool generateProperty) GetClassDeclarationForSourceGen(
        GeneratorSyntaxContext context
    )
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        // Go through all attributes of the class.
        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
        foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
        {
            var symbolInfo = context.SemanticModel.GetSymbolInfo(attributeSyntax);
            if (symbolInfo.Symbol is not IMethodSymbol attributeSymbol)
                continue; // if we can't get the symbol, ignore it

            string attributeName = attributeSymbol.ContainingType.ToDisplayString();

            // Check the full name of the attribute.
            if (attributeName == GeneratePropertiesAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == NotifyPropertyChangedAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == NotifyPropertyChangingAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == ValidationStrategyAttribute)
                return (classDeclarationSyntax, true);
            // if (attributeName == PropertyNameAttribute) return (classDeclarationSyntax, true);
            if (attributeName == PropertyAttributeAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == DisableAttributeTakeoverAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == PropertyEncapsulationAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == VirtualPropertyAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == EqualityCheckAttribute)
                return (classDeclarationSyntax, true);
            // if (attributeName == GuardAttribute) return (classDeclarationSyntax, true);
            // if (attributeName == GetterAttribute) return (classDeclarationSyntax, true);
            // if (attributeName == SetterAttribute) return (classDeclarationSyntax, true);
        }

        // Go through all fields of the class.
        foreach (var field in classDeclarationSyntax.Members.OfType<FieldDeclarationSyntax>())
        foreach (AttributeListSyntax attributeListSyntax in field.AttributeLists)
        foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
        {
            if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                continue; // if we can't get the symbol, ignore it

            string attributeName = attributeSymbol.ContainingType.ToDisplayString();

            // Check the full name of the attribute.
            if (attributeName == GeneratePropertiesAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == NoPropertyAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == NotifyPropertyChangedAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == NotifyPropertyChangingAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == ValidationStrategyAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == PropertyNameAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == PropertyAttributeAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == DisableAttributeTakeoverAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == PropertyEncapsulationAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == VirtualPropertyAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == EqualityCheckAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == GuardAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == GetterAttribute)
                return (classDeclarationSyntax, true);
            if (attributeName == SetterAttribute)
                return (classDeclarationSyntax, true);
        }

        return (classDeclarationSyntax, false);
    }

    /// <summary>
    /// Generate code action.
    /// It will be executed on specific nodes (ClassDeclarationSyntax annotated with the [Report] attribute) changed by the user.
    /// </summary>
    /// <param name="context">Source generation context used to add source files.</param>
    /// <param name="compilation">Compilation used to provide access to the Semantic Model.</param>
    /// <param name="classDeclarations">Nodes annotated with the [Report] attribute that trigger the generate action.</param>
    private static void GenerateCode(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> classDeclarations
    )
    {
        GenInfo GetGenerationInfo(IReadOnlyCollection<AttributeData> attributes)
        {
            bool? generateProperty = null;
            bool? notifyPropertyChanged = null;
            bool? notifyPropertyChanging = null;
            string? validationStrategy = null;
            string? propertyName = null;
            string? propertyEncapsulation = null;
            bool? virtualProperty = null;
            (string type, string from, string to)? range = null;
            int? maxLength = null;
            (string mode, string epsilonF, string epsilonD, string? custom)? equalityCheck = null;
            (List<string> attributes, bool inherit)? propertyAttributes = null;
            (List<string> attributes, bool inherit)? disableAttributeTakeover = null;
            List<(string methodName, string? className)> guardMethods = new();
            EGetterMode getterMode = default;
            ESetterMode setterMode = default;

            foreach (var attribute in attributes)
            {
                if (attribute.AttributeClass is null)
                    continue;
                var attributeName = attribute.AttributeClass.ToDisplayString();
                switch (attributeName)
                {
                    case RangeAttribute when attribute.AttributeConstructor is not null
                                             && attribute.ConstructorArguments.Length is 2 or 3:
                    {
                        var first = attribute.AttributeConstructor.Parameters[0].Type.ToDisplayString();
                        string rangeAttribute;
                        switch (first)
                        {
                            case "double":
                            {
                                var from = attribute.ConstructorArguments[0].Value?.ToString().Replace(',', '.')
                                           ?? "0.0";
                                var to = attribute.ConstructorArguments[1].Value?.ToString().Replace(',', '.') ?? "0.0";
                                range          = (first, from, to);
                                rangeAttribute = $"[{attributeName}({from}, {to})]";
                                break;
                            }
                            case "int":
                            {
                                var from = attribute.ConstructorArguments[0].Value?.ToString() ?? "0";
                                var to = attribute.ConstructorArguments[1].Value?.ToString() ?? "0";
                                range          = (first, from, to);
                                rangeAttribute = $"[{attributeName}({from}, {to})]";
                                break;
                            }
                            case "System.Type" when attribute.ConstructorArguments.Length is 3:
                            {
                                var from = attribute.ConstructorArguments[1].Value?.ToString() ?? "0";
                                var to = attribute.ConstructorArguments[2].Value?.ToString() ?? "0";
                                range = (first, from, to);
                                rangeAttribute =
                                    $"[{attributeName}(typeof({attribute.ConstructorArguments[0].Value}), \"{from.Replace("\"", "\\\"")}\", \"{to}\")]";
                                break;
                            }
                            default:
                                continue;
                        }

                        disableAttributeTakeover = disableAttributeTakeover is null
                            ? (new List<string> { rangeAttribute }, false)
                            : (disableAttributeTakeover.Value.attributes.Append(rangeAttribute).ToList(),
                                disableAttributeTakeover.Value.inherit);
                        break;
                    }
                    case MaxLengthAttribute when attribute.ConstructorArguments.Length is 1:
                        maxLength = (int) (attribute.ConstructorArguments[0].Value ?? 0);
                        goto default;
                    default:
                    {
                        var builder = new StringBuilder();
                        builder.Append('[');
                        builder.Append(attributeName);
                        if (attribute.ConstructorArguments.Length > 0)
                        {
                            builder.Append('(');
                            for (var i = 0; i < attribute.ConstructorArguments.Length; i++)
                            {
                                if (i > 0)
                                    builder.Append(", ");
                                var csharp = attribute.ConstructorArguments[i].ToCSharp();
                                builder.Append(csharp);
                            }

                            builder.Append(')');
                        }

                        builder.Append(']');
                        disableAttributeTakeover = disableAttributeTakeover is null
                            ? (new List<string> { builder.ToString() }, false)
                            : (disableAttributeTakeover.Value.attributes.Append(builder.ToString()).ToList(),
                                disableAttributeTakeover.Value.inherit);
                        break;
                    }
                    case GetterAttribute:
                        getterMode = (EGetterMode) (int) (attribute.ConstructorArguments[0].Value ?? 0);
                        break;
                    case SetterAttribute:
                        setterMode = (ESetterMode) (int) (attribute.ConstructorArguments[0].Value ?? 0);
                        break;
                    case DisableAttributeTakeoverAttribute:
                        disableAttributeTakeover = disableAttributeTakeover is null
                            ? (new List<string>(), true)
                            : (disableAttributeTakeover.Value.attributes, true);
                        break;
                    case PropertyAttributeAttribute:
                    {
                        var name = attribute.ConstructorArguments[0].Value?.ToString()
                                   ?? attribute
                                       .NamedArguments.FirstOrDefault(a => a.Key == "name")
                                       .Value.Value?.ToString();
                        if (name is null)
                            continue;
                        // name = "Range(0, 100)"
                        // name = "[Range(0, 100)]"
                        // name = "[Range(0, 100), MaxLength(10)]"
                        if (!name.StartsWith("[", StringComparison.Ordinal)
                            && !name.EndsWith("]", StringComparison.Ordinal))
                            name = string.Concat("[", name, "]");
                        var inherit = (bool?) attribute.ConstructorArguments[1].Value
                                      ?? attribute.NamedArguments.FirstOrDefault(a => a.Key == "inherit")
                                          .Value.Value is true;
                        if (propertyAttributes is null)
                            propertyAttributes = (new List<string> { name }, inherit);
                        else
                        {
                            propertyAttributes.Value.attributes.Add(name);
                            propertyAttributes = (propertyAttributes.Value.attributes,
                                propertyAttributes.Value.inherit || inherit);
                        }

                        break;
                    }
                    case GeneratePropertiesAttribute:
                        generateProperty = true;
                        break;
                    case NoPropertyAttribute:
                        generateProperty = false;
                        break;
                    case NotifyPropertyChangedAttribute:
                        notifyPropertyChanged = (bool?) attribute.ConstructorArguments[0].Value ?? true;
                        break;
                    case NotifyPropertyChangingAttribute:
                        notifyPropertyChanging = (bool?) attribute.ConstructorArguments[0].Value ?? true;
                        break;
                    case ValidationStrategyAttribute:
                        validationStrategy = attribute.ConstructorArguments[0].Value?.ToString() ?? "Exception";
                        break;
                    case PropertyNameAttribute:
                        propertyName = attribute.ConstructorArguments[0].Value?.ToString();
                        break;
                    case PropertyEncapsulationAttribute:
                        propertyEncapsulation = attribute.ConstructorArguments[0].Value?.ToString();
                        break;
                    case VirtualPropertyAttribute:
                        virtualProperty = true;
                        break;
                    case EqualityCheckAttribute:
                    {
                        var mode = attribute.ConstructorArguments[0].Value?.ToString()
                                   ?? attribute
                                       .NamedArguments.FirstOrDefault(a => a.Key == "mode")
                                       .Value.Value?.ToString()
                                   ?? "Default";
                        var epsilonF = attribute.ConstructorArguments[1].Value?.ToString()
                                       ?? attribute
                                           .NamedArguments.FirstOrDefault(a => a.Key == "FloatEpsilon")
                                           .Value.Value?.ToString()
                                       ?? "Single.Epsilon";
                        var epsilonD = attribute.ConstructorArguments[2].Value?.ToString()
                                       ?? attribute
                                           .NamedArguments.FirstOrDefault(a => a.Key == "DoubleEpsilon")
                                           .Value.Value?.ToString()
                                       ?? "Double.Epsilon";
                        var custom = attribute.ConstructorArguments[3].Value?.ToString()
                                     ?? attribute
                                         .NamedArguments.FirstOrDefault(a => a.Key == "Custom")
                                         .Value.Value?.ToString();
                        equalityCheck = (mode, epsilonF, epsilonD, custom);
                        break;
                    }
                    case GuardAttribute:
                    {
                        var methodName = attribute.ConstructorArguments[0].Value?.ToString()
                                         ?? attribute
                                             .NamedArguments.FirstOrDefault(a => a.Key == "methodName")
                                             .Value.Value?.ToString();
                        if (methodName is null)
                            continue;
                        var className = attribute.ConstructorArguments[1].Value?.ToString()
                                        ?? attribute
                                            .NamedArguments.FirstOrDefault(a => a.Key == "className")
                                            .Value.Value?.ToString();
                        guardMethods.Add((methodName, className));
                        break;
                    }
                }
            }

            return new GenInfo
            {
                Generate                 = generateProperty,
                NotifyPropertyChanged    = notifyPropertyChanged,
                NotifyPropertyChanging   = notifyPropertyChanging,
                ValidationStrategy       = validationStrategy,
                PropertyName             = propertyName,
                PropertyEncapsulation    = propertyEncapsulation,
                VirtualProperty          = virtualProperty,
                Range                    = range,
                MaxLength                = maxLength,
                EqualityCheck            = equalityCheck,
                GuardMethods             = guardMethods,
                PropertyAttributes       = propertyAttributes,
                DisableAttributeTakeover = disableAttributeTakeover,
                GetterMode               = getterMode,
                SetterMode               = setterMode,
            };
        }

        string NormalizeFieldName(string origFieldName)
        {
            var fieldName = origFieldName.TrimStart('_');
            fieldName = fieldName.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + fieldName.Substring(1);
            if (fieldName.EndsWith("Field", StringComparison.Ordinal) && fieldName != "Field")
                fieldName = fieldName.Substring(0, fieldName.Length - 5);
            if (origFieldName == fieldName)
                return string.Concat(fieldName, "Property");
            return fieldName;
        }

        // Go through all filtered class declarations.
        foreach (var classDeclarationSyntax in classDeclarations)
        {
            // We need to get semantic model of the class to retrieve metadata.
            var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);

            // Symbols allow us to get the compile-time information.
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not { } classSymbol)
                continue;

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            // 'Identifier' means the token of the node. Get class name from the syntax node.
            var fullClassName = string.Concat(
                classDeclarationSyntax.Identifier,
                classDeclarationSyntax.TypeParameterList
            );
            var fileClassName = fullClassName
                .Replace('<', '_')
                .Replace('>', '_')
                .Replace(',', '_')
                .Replace(" ", string.Empty);

            var usingStrings = new HashSet<string> { "using System;", "using System.Collections.Generic;" };
            if (classDeclarationSyntax.Parent is BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                foreach (var s in namespaceDeclarationSyntax.Usings.Select((q) => q.ToString()))
                    usingStrings.Add(s);
                if (namespaceDeclarationSyntax.Parent is CompilationUnitSyntax compilationUnitSyntax)
                    foreach (var s in compilationUnitSyntax.Usings.Select((q) => q.ToString()))
                        usingStrings.Add(s);
            }
            else if (classDeclarationSyntax.Parent is CompilationUnitSyntax compilationUnitSyntax)
                foreach (var s in compilationUnitSyntax.Usings.Select((q) => q.ToString()))
                    usingStrings.Add(s);

            var defaultGenInfo = GetGenerationInfo(classSymbol.GetAttributes());

            // Go through all fields of the class.
            var builder = new StringBuilder();
            builder.AppendLine("// <auto-generated/>");
            builder.AppendLine($"#nullable enable");
            foreach (var usingDirectiveSyntax in usingStrings)
                builder.AppendLine(usingDirectiveSyntax);
            builder.AppendLine();
            builder.AppendLine($"namespace {namespaceName};");
            builder.AppendLine($"partial class {fullClassName}");
            if (defaultGenInfo is { NotifyPropertyChanging: true, NotifyPropertyChanged: true })
                builder.AppendLine(
                    " : System.ComponentModel.INotifyPropertyChanged, System.ComponentModel.INotifyPropertyChanging"
                );
            else if (defaultGenInfo.NotifyPropertyChanged is true)
                builder.AppendLine(" : System.ComponentModel.INotifyPropertyChanged");
            else if (defaultGenInfo.NotifyPropertyChanging is true)
                builder.AppendLine(" : System.ComponentModel.INotifyPropertyChanging");
            builder.AppendLine("{");
            if (defaultGenInfo.NotifyPropertyChanging is true)
                builder.AppendLine(
                    "    public event System.ComponentModel.PropertyChangingEventHandler? PropertyChanging;"
                );
            if (defaultGenInfo.NotifyPropertyChanged is true)
                builder.AppendLine(
                    "    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;"
                );

            var notifyOnDictionary = CreateNotifyOnDictionary(classSymbol);
            var fieldSymbols = classSymbol.GetMembers().OfType<IFieldSymbol>();
            foreach (var fieldSymbol in fieldSymbols)
            {
                if (fieldSymbol.Name.Length > 0 && fieldSymbol.Name[0] == '<')
                    continue;
                var fieldGenInfo = GetGenerationInfo(fieldSymbol.GetAttributes());
                var currentGenInfo = fieldGenInfo.WithDefaults(defaultGenInfo);
                if (!currentGenInfo.GenerateProperty())
                    continue;
                var propertyName = currentGenInfo.PropertyName ?? NormalizeFieldName(fieldSymbol.Name);
                var propertyType = fieldSymbol.Type.ToDisplayString();
                if (IsNullableFieldSymbol(fieldSymbol)
                    && !propertyType.EndsWith("?", StringComparison.InvariantCulture))
                    propertyType = string.Concat(propertyType, '?');
                WriteOutInheritedAttributes(currentGenInfo, builder);
                WriteOutAttributes(currentGenInfo, builder);
                WriteOutDocumentationTrivia(fieldSymbol, builder);
                builder.Append("    "); // Indentation.
                builder.Append(
                    currentGenInfo.PropertyEncapsulation switch
                    {
                        "0" => "public",
                        "1" => "protected",
                        "2" => "private",
                        "3" => "internal",
                        "4" => "protected internal",
                        _   => "public",
                    }
                );
                builder.Append(' ');
                if (currentGenInfo.VirtualProperty is true)
                    builder.Append("virtual ");
                builder.Append(propertyType);
                builder.Append(' ');
                builder.Append(propertyName);
                builder.AppendLine();
                builder.AppendLine("    {");
                if (currentGenInfo.GetterMode is EGetterMode.Default)
                {
                    builder.AppendLine($"        get => {fieldSymbol.Name};");
                }

                if (currentGenInfo.SetterMode is not ESetterMode.None)
                {
                    var isInit = false;
                    if (currentGenInfo.SetterMode is ESetterMode.Set)
                        builder.AppendLine("        set");
                    else if (currentGenInfo.SetterMode is ESetterMode.Init)
                    {
                        builder.AppendLine("        init");
                        isInit = true;
                    }
                    else
                    {
                        if (fieldSymbol.IsReadOnly)
                        {
                            builder.AppendLine("        init");
                            isInit = true;
                        }
                        else
                        {
                            builder.AppendLine("        set");
                        }
                    }

                    builder.AppendLine("        {");

                    if (!isInit)
                    {
                        WriteOutEqualityCheck(currentGenInfo, propertyType, builder, fieldSymbol);
                        WriteOutNotifyPropertyChanging(currentGenInfo, builder, propertyName);
                        foreach (var notifyProperty in notifyOnDictionary.TryGetValue(
                                     propertyName,
                                     out var notifyOnList
                                 )
                                     ? notifyOnList
                                     : [])
                        {
                            WriteOutNotifyPropertyChanging(currentGenInfo, builder, notifyProperty);
                        }
                    }

                    WriteOutRangeValidation(currentGenInfo, builder, propertyName);
                    WriteOutMaxLengthValidation(currentGenInfo, builder, propertyName);
                    WriteOutGuards(currentGenInfo, builder, fieldSymbol, propertyName);
                    builder.AppendLine($"            {fieldSymbol.Name} = value;");
                    if (!isInit)
                    {
                        WriteOutPropertyChanged(currentGenInfo, builder, propertyName);
                        foreach (var notifyProperty in notifyOnDictionary.TryGetValue(
                                     propertyName,
                                     out var notifyOnList
                                 )
                                     ? notifyOnList
                                     : [])
                        {
                            WriteOutPropertyChanged(currentGenInfo, builder, notifyProperty);
                        }
                    }

                    builder.AppendLine("        }");
                }

                builder.AppendLine("    }");
            }

            builder.AppendLine("}");

            // Add the source code to the compilation.
            context.AddSource($"{fileClassName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }

    private static Dictionary<string, List<string>> CreateNotifyOnDictionary(INamedTypeSymbol classSymbol)
    {
        var notifyOnDictionary = new Dictionary<string, List<string>>();

        foreach (var propertySymbol in classSymbol.GetMembers().OfType<IPropertySymbol>())
        {
            var notifyOnList = GetNotifyOnInfo(propertySymbol.GetAttributes());
            foreach (var referredField in notifyOnList)
            {
                if (!notifyOnDictionary.TryGetValue(referredField, out var list))
                    notifyOnDictionary[referredField] = list = new List<string>();
                list.Add(propertySymbol.Name);
            }
        }

        return notifyOnDictionary;

        List<string> GetNotifyOnInfo(IReadOnlyCollection<AttributeData> attributes)
        {
            var notifyOn = new List<string>();
            foreach (var attribute in attributes)
            {
                if (attribute.AttributeClass is null)
                    continue;
                var attributeName = attribute.AttributeClass.ToDisplayString();
                switch (attributeName)
                {
                    case NotifyOnAttribute:
                        if (attribute.ConstructorArguments[0].Value is string property)
                            notifyOn.Add(property);
                        break;
                }
            }

            return notifyOn;
        }
    }

    private static void WriteOutDocumentationTrivia(IFieldSymbol fieldSymbol, StringBuilder builder)
    {
        var declaringSyntax = fieldSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
        if (declaringSyntax is null)
            return;
        if (declaringSyntax.Parent?.Parent is not FieldDeclarationSyntax fieldDeclarationSyntax)
            return;
        var trivia = Enumerable
            .Empty<SyntaxTrivia>()
            .Concat(fieldDeclarationSyntax.AttributeLists.SelectMany((q) => q.DescendantTrivia()))
            .Concat(fieldDeclarationSyntax.Modifiers.SelectMany((q) => q.GetAllTrivia()));
        var doc = trivia
            .Where(
                (q) =>
                {
                    if (q.IsKind(SyntaxKind.DocumentationCommentExteriorTrivia))
                        return true;
                    if (q.IsKind(SyntaxKind.EndOfDocumentationCommentToken))
                        return true;
                    if (q.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                        return true;
                    if (q.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia))
                        return true;
                    return false;
                }
            )
            .ToArray();
        foreach (var syntaxTrivia in doc)
        {
            builder.Append("    "); // Indentation.
            builder.Append(syntaxTrivia.ToFullString());
        }

        if (builder.Length > 2 && builder[builder.Length - 1] != '\n' && builder[builder.Length - 2] != '\n')
            builder.AppendLine();
    }

    private static void WriteOutInheritedAttributes(GenInfo currentGenInfo, StringBuilder builder)
    {
        foreach (var attribute in currentGenInfo.DisableAttributeTakeover?.attributes ?? Enumerable.Empty<string>())
        {
            builder.AppendLine($"    {attribute}");
        }
    }

    private static void WriteOutAttributes(GenInfo currentGenInfo, StringBuilder builder)
    {
        foreach (var attribute in currentGenInfo.PropertyAttributes?.attributes ?? Enumerable.Empty<string>())
        {
            builder.AppendLine($"    {attribute}");
        }
    }

    private static void WriteOutGuards(
        GenInfo currentGenInfo,
        StringBuilder builder,
        IFieldSymbol fieldSymbol,
        string propertyName
    )
    {
        foreach (var guardMethod in currentGenInfo.GuardMethods)
        {
            var method = guardMethod.className is null
                ? guardMethod.methodName
                : string.Concat(guardMethod.className, ".", guardMethod.methodName);
            builder.AppendLine($"            if (!{method}({fieldSymbol.Name}, value))");
            WriteOutValidationStrategyOutcome(currentGenInfo, builder, propertyName, $"Guard method {method} failed");
        }
    }

    private static void WriteOutMaxLengthValidation(GenInfo currentGenInfo, StringBuilder builder, string propertyName)
    {
        if (currentGenInfo.MaxLength is null)
            return;
        builder.AppendLine($"            if (value.Length > {currentGenInfo.MaxLength})");
        WriteOutValidationStrategyOutcome(
            currentGenInfo,
            builder,
            propertyName,
            $"Value must be at most {currentGenInfo.MaxLength} characters long"
        );
    }

    private static void WriteOutRangeValidation(GenInfo currentGenInfo, StringBuilder builder, string propertyName)
    {
        if (currentGenInfo.Range is null)
            return;

        var (type, from, to) = currentGenInfo.Range.Value;
        switch (type)
        {
            case "float":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "double":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "sbyte":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "byte":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "short":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "ushort":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "int":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "uint":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "long":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "ulong":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "decimal":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "bool":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "nint":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "unint":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            case "char":
                builder.AppendLine($"            if (value < {from} || value > {to})");
                break;
            default:
                builder.AppendLine($"            if (value.CompareTo({from}) < 0 || value.CompareTo({to}) > 0)");
                break;
        }

        WriteOutValidationStrategyOutcome(
            currentGenInfo,
            builder,
            propertyName,
            $"Value must be between {from} and {to}"
        );
    }

    private static void WriteOutValidationStrategyOutcome(
        GenInfo currentGenInfo,
        StringBuilder builder,
        string propertyName,
        string reason
    )
    {
        switch (currentGenInfo.ValidationStrategy)
        {
            default:
            // ReSharper disable once RedundantCaseLabel
            case "0":
            // ReSharper disable once RedundantCaseLabel
            case null:
                builder.AppendLine(
                    $"                throw new System.ArgumentException(\"Validation of {propertyName} failed: {reason}\", nameof(value));"
                );
                break;
            case "1" when currentGenInfo.NotifyPropertyChanged is true:
                builder.AppendLine("            {");
                builder.AppendLine(
                    $"                this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(\"{propertyName}\"));"
                );
                builder.AppendLine("                return;");
                builder.AppendLine("            }");
                break;
            case "1":
            case "2":
                builder.AppendLine("                return;");
                break;
        }
    }

    private static void WriteOutPropertyChanged(GenInfo currentGenInfo, StringBuilder builder, string propertyName)
    {
        if (currentGenInfo.NotifyPropertyChanged is not null)
        {
            builder.AppendLine(
                $"            this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(\"{propertyName}\"));"
            );
        }
    }

    private static void WriteOutNotifyPropertyChanging(
        GenInfo currentGenInfo,
        StringBuilder builder,
        string propertyName
    )
    {
        if (currentGenInfo.NotifyPropertyChanging is not null)
        {
            builder.AppendLine(
                $"            this.PropertyChanging?.Invoke(this, new System.ComponentModel.PropertyChangingEventArgs(\"{propertyName}\"));"
            );
        }
    }

    private static void WriteOutEqualityCheck(
        GenInfo currentGenInfo,
        string propertyType,
        StringBuilder builder,
        IFieldSymbol fieldSymbol
    )
    {
        switch (currentGenInfo.EqualityCheck)
        {
            default:
            // ReSharper disable once RedundantCaseLabel
            case null:
            // ReSharper disable once RedundantCaseLabel
            case ("0", _, _, _):
                switch (propertyType)
                {
                    case "System.Single":
                        builder.AppendLine(
                            $"            if (Math.Abs(value - {fieldSymbol.Name}) < {currentGenInfo.EqualityCheck?.epsilonF ?? "Single.Epsilon"}) return;"
                        );
                        break;
                    case "System.Double":
                        builder.AppendLine(
                            $"            if (Math.Abs(value - {fieldSymbol.Name}) < {currentGenInfo.EqualityCheck?.epsilonF ?? "Double.Epsilon"}) return;"
                        );
                        break;
                    case "System.SByte":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Byte":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Int16":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.UInt16":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Int32":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.UInt32":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Int64":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.UInt64":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Decimal":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Boolean":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.IntPtr":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.UIntPtr":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    case "System.Char":
                        builder.AppendLine($"            if (value == {fieldSymbol.Name}) return;");
                        break;
                    default:
                        builder.AppendLine(
                            IsNullableFieldSymbol(fieldSymbol)
                                ? $"            if (value is null && {fieldSymbol.Name} is null || (value?.Equals({fieldSymbol.Name}) ?? false)) return;"
                                : $"            if (value.Equals({fieldSymbol.Name})) return;"
                        );
                        break;
                }

                break;
            case ("1", _, _, { } customMethod):
                builder.AppendLine($"            if ({customMethod}({fieldSymbol.Name}, value)) return;");
                break;
            case ("2", _, _, _):
                break;
        }
    }

    private static bool IsNullableFieldSymbol(IFieldSymbol fieldSymbol)
    {
        if (fieldSymbol.NullableAnnotation is NullableAnnotation.Annotated)
            return true;

        if (fieldSymbol.Type.TypeKind != TypeKind.TypeParameter)
        {
            return fieldSymbol.NullableAnnotation is NullableAnnotation.None && !fieldSymbol.Type.IsValueType;
        }
        else
        {
            return fieldSymbol.NullableAnnotation is NullableAnnotation.None && !fieldSymbol.Type.IsValueType;
        }
    }
}