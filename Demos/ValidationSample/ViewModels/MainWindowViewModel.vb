Namespace ViewModels
    Public Class MainWindowViewModel
        Inherits ViewModelBase

        ''' <summary>
        ''' Gets a ViewModel showing how to use DataAnnotations for validation
        ''' </summary>
        Public ReadOnly Property ValidationUsingDataAnnotationViewModel As New ValidationUsingDataAnnotationViewModel

        ''' <summary>
        ''' Gets a ViewModel showing how to use INotifyDataErrorInfo for validation
        ''' </summary>
        Public ReadOnly Property ValidationUsingINotifyDataErrorInfoViewModel As New ValidationUsingINotifyDataErrorInfoViewModel

        ''' <summary>
        ''' Gets a ViewModel showing how to use Exceptions inside the setter for validation
        ''' </summary>
        Public ReadOnly Property ValidationUsingExceptionInsideSetterViewModel As New ValidationUsingExceptionInsideSetterViewModel
    End Class
End Namespace
