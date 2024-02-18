using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Generators.Common;
using Avalonia.Generators.Common.Domain;
using Avalonia.Generators.Compiler;
using Microsoft.CodeAnalysis;

namespace Avalonia.Generators.NameGenerator;

public class AvaloniaNameSourceGenerator : ISourceGenerator
{
    private const string SourceItemGroupMetadata = "build_metadata.AdditionalFiles.SourceItemGroup";

    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            var generator = CreateNameGenerator(context);
            if (generator is null)
            {
                return;
            }

            var partials = generator.GenerateNameReferences(ResolveAdditionalFiles(context), context.CancellationToken);
            foreach (var (fileName, content) in partials)
            {
                if(context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                context.AddSource(fileName, content);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception exception)
        {
            context.ReportNameGeneratorUnhandledError(exception);
        }
    }

    private static IEnumerable<AdditionalText> ResolveAdditionalFiles(GeneratorExecutionContext context)
    {
        return context
            .AdditionalFiles
            .Where(f => context.AnalyzerConfigOptions
                .GetOptions(f)
                .TryGetValue(SourceItemGroupMetadata, out var sourceItemGroup)
                && sourceItemGroup == "AvaloniaXaml");
    }

    private INameGenerator CreateNameGenerator(GeneratorExecutionContext context)
    {
        var options = new GeneratorOptions(context);
        if (!options.AvaloniaNameGeneratorIsEnabled)
        {
            return null;
        }

        var types = new RoslynTypeSystem(context.Compilation);
        ICodeGenerator generator = GetCodeGenerator(options, types);

        var compiler = MiniCompiler.CreateDefault(types, MiniCompiler.AvaloniaXmlnsDefinitionAttribute);
        ViewFileNamingStrategy avaloniaNameGeneratorViewFileNamingStrategy = options.AvaloniaNameGeneratorViewFileNamingStrategy;
        GlobPatternGroup pathPattern = new GlobPatternGroup(options.AvaloniaNameGeneratorFilterByPath);
        GlobPatternGroup namespacePattern = new GlobPatternGroup(options.AvaloniaNameGeneratorFilterByNamespace);
        XamlXViewResolver classes = new XamlXViewResolver(types, compiler, true,
                        type => context.ReportNameGeneratorInvalidType(type),
                        error => context.ReportNameGeneratorUnhandledError(error));
        XamlXNameResolver names = new XamlXNameResolver(options.AvaloniaNameGeneratorClassFieldModifier);
        return CreateNameGenerator(avaloniaNameGeneratorViewFileNamingStrategy, pathPattern, namespacePattern, classes, names, generator);
    }

    private protected virtual ICodeGenerator GetCodeGenerator(GeneratorOptions options, RoslynTypeSystem types)
    {
        return options.AvaloniaNameGeneratorBehavior switch
        {
            Behavior.OnlyProperties => new OnlyPropertiesCodeGenerator(),
            Behavior.InitializeComponent => new InitializeComponentCodeGenerator(types, options.AvaloniaNameGeneratorAttachDevTools),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private protected virtual INameGenerator CreateNameGenerator(
        ViewFileNamingStrategy avaloniaNameGeneratorViewFileNamingStrategy, GlobPatternGroup pathPattern,
        GlobPatternGroup namespacePattern, XamlXViewResolver classes, XamlXNameResolver names, ICodeGenerator generator)
    {
        return new AvaloniaNameGenerator(
                    avaloniaNameGeneratorViewFileNamingStrategy,
                    pathPattern,
                    namespacePattern,
                    classes,
                    names,
                    generator);
    }
}
