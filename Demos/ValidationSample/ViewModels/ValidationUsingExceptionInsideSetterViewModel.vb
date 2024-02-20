Imports ReactiveUI

Namespace ViewModels
    Public Class ValidationUsingExceptionInsideSetterViewModel
        Inherits ViewModelBase

        Private _EMail As String

        ''' <summary>
        ''' Validation using Exceptions (only inside setter allowed!)
        ''' </summary>
        Public Property EMail As String
            Get
                Return _EMail
            End Get
            Set(value As String)
                ' The field may not be null or empty
                If String.IsNullOrEmpty(value) Then
                    Throw New ArgumentNullException(NameOf(EMail), "This field is required")
                    ' The field must contain an '@' sign
                ElseIf Not value.Contains("@"c) Then
                    Throw New ArgumentException(NameOf(EMail), "Not a valid E-Mail-Address")
                    ' The checks were successful, so we can store the value. 
                Else
                    RaiseAndSetIfChanged(_EMail, value)
                End If
            End Set
        End Property
    End Class
End Namespace
