Imports Avalonia.Generators.Common.Domain
Imports Avalonia.Generators.NameGenerator

Friend Class AvaloniaNameVBGenerator
    Inherits AvaloniaNameGenerator

    Public Sub New(naming As ViewFileNamingStrategy, pathPattern As IGlobPattern, namespacePattern As IGlobPattern, classes As IViewResolver, names As INameResolver, code As ICodeGenerator)
        MyBase.New(naming, pathPattern, namespacePattern, classes, names, code)
    End Sub

    Protected Overrides ReadOnly Property FileExtension As String
        Get
            Return ".vb"
        End Get
    End Property
End Class
