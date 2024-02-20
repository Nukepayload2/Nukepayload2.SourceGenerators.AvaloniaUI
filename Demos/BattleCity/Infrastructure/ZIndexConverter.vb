Imports System.Globalization
Imports BattleCity.Model
Imports Avalonia.Data.Converters

Namespace Infrastructure

    Friend Class ZIndexConverter
        Implements IValueConverter

        Public Shared ReadOnly Property Instance As New ZIndexConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Player Then
                Return 2
            End If
            If TypeOf value Is Tank Then
                Return 1
            End If
            Return 0
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
