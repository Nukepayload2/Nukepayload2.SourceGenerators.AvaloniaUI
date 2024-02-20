Imports Avalonia.Data.Converters
Imports System.Globalization

Namespace Converter
    ''' <summary>
    ''' This is a converter which will add two numbers
    ''' </summary>
    Public Class MathAddConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            ' For add this is simple. just return the sum of the value and the parameter.
            ' You may want to validate value and parameter in a real world App
            Return DirectCast(value, Decimal?) + DirectCast(parameter, Decimal?)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            ' If we want to convert back, we need to subtract instead of add.
            Return DirectCast(value, Decimal?) - DirectCast(parameter, Decimal?)
        End Function
    End Class
End Namespace
