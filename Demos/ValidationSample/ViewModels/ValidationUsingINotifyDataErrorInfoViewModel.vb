Imports ReactiveUI
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class ValidationUsingINotifyDataErrorInfoViewModel
        Inherits ViewModelBase
        Implements INotifyDataErrorInfo

        Public Sub New()
            ' Listen to changes of "ValidationUsingINotifyDataErrorInfo" and re-evaluate the validation
            WhenAnyValue(Function(x) x.EMail).Subscribe(Sub() Validate_EMail())

            ' run INotifyDataErrorInfo-validation on start-up
            Validate_EMail()
        End Sub

        Public Event ErrorsChanged As EventHandler(Of DataErrorsChangedEventArgs) Implements INotifyDataErrorInfo.ErrorsChanged

        ' we have errors present if errors.Count is greater than 0
        Public ReadOnly Property HasErrors() As Boolean Implements INotifyDataErrorInfo.HasErrors
            Get
                Return errors.Count > 0
            End Get
        End Property

        ''' <inheritdoc />
        Public Function GetErrors(propertyName As String) As IEnumerable Implements INotifyDataErrorInfo.GetErrors
            ' Get entity-level errors when the target property is null or empty
            If String.IsNullOrEmpty(propertyName) Then
                Return errors.Values.SelectMany(Function(errors) errors)
            End If

            ' Property-level errors, if any
            Dim result As List(Of ValidationResult) = Nothing
            If errors.TryGetValue(propertyName, result) Then
                Return result
            End If

            ' In case there are no errors we return an empty array.
            Return Array.Empty(Of ValidationResult)
        End Function

        ' Store Errors in a Dictionary
        Private ReadOnly errors As New Dictionary(Of String, List(Of ValidationResult))

        ''' <summary>
        ''' Clears the errors for a given property name.
        ''' </summary>
        ''' <param name="propertyName">The name of the property to clear or all properties if <see langword="null"/></param>
        Protected Sub ClearErrors(Optional propertyName As String = Nothing)
            ' Clear entity-level errors when the target property is null or empty
            If String.IsNullOrEmpty(propertyName) Then
                errors.Clear()
            Else
                errors.Remove(propertyName)
            End If

            ' Notify that errors have changed
            RaiseEvent ErrorsChanged(Me, New DataErrorsChangedEventArgs(propertyName))
            RaisePropertyChanged(NameOf(HasErrors))
        End Sub

        ''' <summary>
        ''' Adds a given error message for a given property name.
        ''' </summary>
        ''' <param name="propertyName">the name of the property</param>
        ''' <param name="errorMessage">The error message to show</param>
        Protected Sub AddError(propertyName As String, errorMessage As String)
            ' Add the cached errors list for later use.
            Dim propertyErrors As List(Of ValidationResult) = Nothing
            If Not errors.TryGetValue(propertyName, propertyErrors) Then
                propertyErrors = New List(Of ValidationResult)()
                errors.Add(propertyName, propertyErrors)
            End If

            propertyErrors.Add(New ValidationResult(errorMessage))

            ' Notify that errors have changed
            RaiseEvent ErrorsChanged(Me, New DataErrorsChangedEventArgs(propertyName))
            RaisePropertyChanged(NameOf(HasErrors))
        End Sub

        Private _EMail As String

        ''' <summary>
        ''' A property that is validated using INotifyDataErrorInfo
        ''' </summary>
        Public Property EMail As String
            Get
                Return _EMail
            End Get
            Set(value As String)
                RaiseAndSetIfChanged(_EMail, value)
            End Set
        End Property

        Private Sub Validate_EMail()
            ' first of all clear all previous errors
            ClearErrors(NameOf(EMail))

            ' No empty string allowed
            If String.IsNullOrEmpty(EMail) Then
                AddError(NameOf(EMail), "This field is required")
            End If

            ' @-sign required
            If EMail Is Nothing OrElse Not EMail.Contains("@"c) Then
                AddError(NameOf(EMail), "Don't forget the '@'-sign")
            End If
        End Sub
    End Class
End Namespace
