﻿Imports Avalonia.Generators.Common.Domain
Imports Avalonia.Generators.NameGenerator
Imports XamlX.TypeSystem

Friend Class OnlyPropertiesVBCodeGenerator
    Inherits OnlyPropertiesCodeGenerator

    Public Overrides Function GenerateCode(className As String, nameSpaceName As String, xamlType As IXamlType, names As IEnumerable(Of ResolvedName)) As String
        Dim namedControls =
            From info In names
            Let typeName = GetVbTypeName(info.XamlType)
            Select $"{Space8}{CsModifierToVB(info.CsFieldModifier)} ReadOnly Property {info.Name} As {typeName}
{Space12}Get
{Space16}Return FindNameScope()?.Find(Of {typeName})(""{info.Name}"")
{Space12}End Get
{Space8}End Property"

        Dim lines = String.Join(vbCrLf, namedControls)

        Return $"' <auto-generated />
Imports Avalonia.Controls

Namespace Global.{nameSpaceName}

    Partial Class {className}
{lines}
    End Class

End Namespace
"
    End Function

End Class
