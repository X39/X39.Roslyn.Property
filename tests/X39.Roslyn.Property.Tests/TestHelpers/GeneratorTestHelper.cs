using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using X39.Roslyn.Property.Generator;
using Xunit;

namespace X39.Roslyn.Property.Tests.TestHelpers;

public static class GeneratorTestHelper
{
    public static void VerifyGeneratedCode(
        string className,
        string sourceCode,
        string expectedCode,
        string[]? acceptedErrors = null)
    {
        acceptedErrors ??= [];

        // Create an instance of the source generator.
        var generator = new PropertyIncrementalSourceGenerator();

        // Source generators should be tested using 'GeneratorDriver'.
        var driver = CSharpGeneratorDriver.Create(generator);

        // We need to create a compilation with the required source code.
        var assemblies = AppDomain
            .CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .OrderBy((q) => q.Location)
            .ToArray();
        var assemblyDir = Path.GetDirectoryName(assemblies.Single(q => q.Location.EndsWith("netstandard.dll")).Location)
                          ?? throw new InvalidOperationException(
                              "Could not find the directory of the netstandard.dll assembly."
                          );
        var selfDir = Path.GetDirectoryName(
            assemblies.Single(q => q.Location.EndsWith("X39.Roslyn.Property.Tests.dll")).Location
        );
        Assert.NotNull(selfDir);
        var compilation = CSharpCompilation.Create(
            nameof(GeneratorTestHelper),
            [CSharpSyntaxTree.ParseText(sourceCode)],
            assemblies
                .Select(assembly => assembly.Location)
                .Append(Path.Combine(selfDir, "X39.Roslyn.Property.dll"))
                .Append(Path.Combine(assemblyDir, "System.ComponentModel.Annotations.dll"))
                .Select(path => MetadataReference.CreateFromFile(path))
                .Distinct()
                .ToArray(),
            new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary,
                reportSuppressedDiagnostics: true,
                optimizationLevel: OptimizationLevel.Debug
            )
        );

        // Run generators and retrieve all results.
        var runResult = driver
            .RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out var _)
            .GetRunResult();

        // Verify that the compilation has no errors.
        var diagnostics = newCompilation.GetDiagnostics();
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error && !acceptedErrors.Contains(d.Id)));

        // All generated files can be found in 'RunResults.GeneratedTrees'.
        var generatedFiles = runResult
            .GeneratedTrees.Where(t => t.FilePath.EndsWith(".g.cs"))
            .Select((q) => (q.FilePath, Code: q.GetText().ToString()))
            .ToArray();

        // Complex generators should be tested using text comparison.
        var (_, classOutput) = Assert.Single(
            generatedFiles,
            f => f.FilePath.EndsWith(string.Concat(className, ".g.cs"))
        );
        Assert.Equal(expectedCode, classOutput, ignoreLineEndingDifferences: true);
    }
}
