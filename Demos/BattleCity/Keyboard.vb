Imports Avalonia.Input

Friend Module Keyboard
    Public ReadOnly Keys As New HashSet(Of Key)

    Public Function IsKeyDown(key As Key) As Boolean
        Return Keys.Contains(key)
    End Function
End Module
