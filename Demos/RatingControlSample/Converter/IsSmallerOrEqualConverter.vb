Imports Avalonia.Data.Converters
Imports System.Globalization

Namespace Converter
    ''' <summary>
    ''' A converter that compares two integers and returns true if the first number is smaller or equal to the second number
    ''' </summary>
    Public Class IsSmallerOrEqualConverter
        Implements IMultiValueConverter

        Public Function Convert(values As IList(Of Object), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values.Count <> 2 Then
                Throw New ArgumentException("Expected exactly two numbers")
            End If
            Dim firstNumber = If(TypeOf values(0) Is Integer, CInt(values(0)), 0)
            Dim secondNumber = If(TypeOf values(1) Is Integer, CInt(values(1)), 0)

            Return firstNumber <= secondNumber
        End Function
    End Class
End Namespace
