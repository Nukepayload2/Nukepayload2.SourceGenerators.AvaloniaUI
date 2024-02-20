Imports Avalonia.Data
Imports Avalonia.Data.Converters
Imports System.Globalization

Namespace Converter
    ''' <summary>
    ''' This converter can calculate any number of values. 
    ''' </summary>
    Public Class MathMultiConverter
        Implements IMultiValueConverter

        Public Function Convert(values As IList(Of Object), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            ' We need to validate if the provided values are valid. We need at least 3 values. 
            ' The first value is the operator and the other two values should be a decimal.
            If values.Count <> 3 Then
                ' We can write a message into the Trace if we want to inform the developer.
                Trace.WriteLine("Exactly three values expected")

                ' return "BindingOperations.DoNothing" instead of throwing an Exception.
                ' If you want, you can also return a BindingNotification with an Exception
                Return BindingOperations.DoNothing
            End If

            ' The first item of values is the operation.
            ' The operation to use is stored as a string.
            Dim operation As String = If(TryCast(values(0), String), "+")

            ' Create a variable result and assign the first value we have to if
            Dim value1 As Decimal = If(TypeOf values(1) Is Decimal, CType(values(1), Decimal), 0)
            Dim value2 As Decimal = If(TypeOf values(2) Is Decimal, CType(values(2), Decimal), 0)

            ' depending on the operator calculate the result.
            Select Case operation
                Case "+"
                    Return value1 + value2

                Case "-"
                    Return value1 - value2

                Case "*"
                    Return value1 * value2

                Case "/"
                    ' We cannot divide by zero. If value2 is '0', return an error. 
                    If value2 = 0 Then
                        Return New BindingNotification(New DivideByZeroException("Don't do this!"), BindingErrorType.Error)
                    End If

                    Return value1 / value2
            End Select

            ' If we reach this line, something was wrong. So we return an error notification
            Return New BindingNotification(New InvalidOperationException("Something went wrong"), BindingErrorType.Error)
        End Function
    End Class
End Namespace
