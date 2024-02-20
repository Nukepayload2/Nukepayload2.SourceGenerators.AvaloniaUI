Imports System.Globalization
Imports BattleCity.Model
Imports Avalonia.Data.Converters

Namespace Infrastructure

    Public Class CellToScreenConverter
        Implements IValueConverter

        Public Shared ReadOnly Property Instance As New CellToScreenConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return System.Convert.ToDouble(value) * GameField.CellSize
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
