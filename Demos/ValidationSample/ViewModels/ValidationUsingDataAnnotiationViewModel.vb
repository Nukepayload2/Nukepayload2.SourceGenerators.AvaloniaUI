Imports ReactiveUI
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class ValidationUsingDataAnnotationViewModel
        Inherits ViewModelBase

        Private _EMail As String

        ''' <summary>
        ''' Validation using DataAnnotation
        ''' </summary>
        <Required>
        <EmailAddress>
        Public Property EMail As String
            Get
                Return _EMail
            End Get
            Set(value As String)
                RaiseAndSetIfChanged(_EMail, value)
            End Set
        End Property
    End Class
End Namespace
