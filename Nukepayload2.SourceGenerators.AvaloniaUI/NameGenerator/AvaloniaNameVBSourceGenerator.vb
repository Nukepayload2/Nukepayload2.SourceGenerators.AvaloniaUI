Imports Avalonia.Generators
Imports Avalonia.Generators.Common
Imports Avalonia.Generators.Common.Domain
Imports Avalonia.Generators.Compiler
Imports Avalonia.Generators.NameGenerator
Imports Microsoft.CodeAnalysis

<Generator(LanguageNames.VisualBasic)>
Public Class AvaloniaNameVBSourceGenerator
    Inherits AvaloniaNameSourceGenerator

    Private Protected Overrides Function CreateNameGenerator(avaloniaNameGeneratorViewFileNamingStrategy As ViewFileNamingStrategy, pathPattern As GlobPatternGroup, namespacePattern As GlobPatternGroup, classes As XamlXViewResolver, names As XamlXNameResolver, generator As ICodeGenerator) As INameGenerator
        Return New AvaloniaNameVBGenerator(avaloniaNameGeneratorViewFileNamingStrategy, pathPattern, namespacePattern, classes, names, generator)
    End Function

    Private Protected Overrides Function GetCodeGenerator(options As GeneratorOptions, types As RoslynTypeSystem) As ICodeGenerator
        Select Case options.AvaloniaNameGeneratorBehavior
            Case Behavior.OnlyProperties
                Return New OnlyPropertiesVBCodeGenerator
            Case Behavior.InitializeComponent
                Return New InitializeComponentVBCodeGenerator(types, options.AvaloniaNameGeneratorAttachDevTools)
            Case Else
                Throw New ArgumentOutOfRangeException
        End Select
    End Function
End Class
