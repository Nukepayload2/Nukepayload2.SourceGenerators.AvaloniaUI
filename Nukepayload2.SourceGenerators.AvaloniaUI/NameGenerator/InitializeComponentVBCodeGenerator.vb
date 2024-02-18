Imports Avalonia.Generators.Common.Domain
Imports Avalonia.Generators.NameGenerator
Imports XamlX.TypeSystem

Friend Class InitializeComponentVBCodeGenerator
    Inherits InitializeComponentCodeGenerator

    Public Sub New(types As IXamlTypeSystem, avaloniaNameGeneratorAttachDevTools As Boolean)
        MyBase.New(types, avaloniaNameGeneratorAttachDevTools)
    End Sub

    Public Overrides Function GenerateCode(className As String, nameSpaceName As String, xamlType As IXamlType, names As IEnumerable(Of ResolvedName)) As String
        Return MyBase.GenerateCode(className, nameSpaceName, xamlType, names)
    End Function
End Class
