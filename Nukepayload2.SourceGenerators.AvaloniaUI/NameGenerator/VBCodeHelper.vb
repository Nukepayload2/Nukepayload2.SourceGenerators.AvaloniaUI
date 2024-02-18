Imports XamlX.TypeSystem

Friend Module VBCodeHelper

    Public Const Space4 = "    "
    Public Const Space8 = Space4 & Space4
    Public Const Space12 = Space8 & Space4
    Public Const Space16 = Space12 & Space4

    Function GetVbTypeName(clrType As IXamlType) As String
        Dim typeName = $"{clrType.Namespace}.{clrType.Name}"
        Dim typeAgs = From arg In clrType.GenericArguments Select GetVbTypeName(arg)
        Dim genericTypeName = If(clrType.GenericArguments.Count = 0,
            $"Global.{typeName}",
            $"Global.{typeName}(Of {String.Join(", ", From arg In typeAgs Select $"Global.{arg}")})")
        Return genericTypeName
    End Function

    Function CsModifierToVB(modifier As String) As String
        If modifier Is Nothing Then Return String.Empty
        Select Case modifier
            Case "public"
                Return "Public"
            Case "internal"
                Return "Friend"
            Case "protected"
                Return "Protected"
            Case "private"
                Return "Private"
            Case Else
                Return modifier
        End Select
    End Function

End Module
