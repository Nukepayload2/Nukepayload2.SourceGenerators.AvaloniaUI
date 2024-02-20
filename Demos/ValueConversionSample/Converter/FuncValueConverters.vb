Imports Avalonia.Data.Converters
Imports Avalonia.Media

Namespace Converter

    ''' <summary>
    ''' A static class holding our FuncValueConverter
    ''' </summary>
    ''' <remarks>
    ''' Consume it from XAML via <code>{x:Static conv:FuncValueConverters.MyConverter}</code>
    ''' </remarks>
    Public Module FuncValueConverters
        ''' <summary>
        ''' Gets a Converter that returns a parsed Brush for a given input. Returns Nothing if the input was not parsed successfully
        ''' </summary>
        Public ReadOnly Property StringToBrushConverter As New FuncValueConverter(Of String, Brush)(
            Function(s)
                ' define output variable
                Dim color As Color = Nothing

                ' try parse color. If that was not successful try to add a leading '#'
                If Color.TryParse(s, color) OrElse Color.TryParse($"#{s}", color) Then
                    Return New SolidColorBrush(color)
                End If

                ' If string was not convertible, we return Nothing
                Return Nothing
            End Function)
    End Module
End Namespace
