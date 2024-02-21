Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input
Imports MvvmDialogSample.Core

Namespace ViewModels
    ''' <summary>
    ''' This sample demonstrates how to use a custom Interaction, if you have no ReactiveUI installed
    ''' </summary>
    Partial Public Class CustomInteractionViewModel
        Inherits ObservableObject

        Dim _SelectedFiles As String()
        ''' <summary>
        ''' Gets or sets a list of selected files
        ''' </summary>
        Public Property SelectedFiles As String()
            Get
                Return _SelectedFiles
            End Get
            Set(value As String())
                SetProperty(_SelectedFiles, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets an instance of our own Interaction class
        ''' </summary>
        Public ReadOnly Property SelectFilesInteraction As New Interaction(Of String, String())

        Public ReadOnly Property SelectFilesCommand As New AsyncRelayCommand(
            Async Function() As Task
                SelectedFiles = Await SelectFilesInteraction.HandleAsync("Hello from Avalonia")
            End Function)
    End Class
End Namespace
