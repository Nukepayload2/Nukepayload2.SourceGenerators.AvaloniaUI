Imports ReactiveUI
Imports System.Reactive.Linq
Imports System.Windows.Input

Namespace ViewModels
    Public Class InteractionViewModel
        Inherits ViewModelBase

        Private _SelectedFiles As String()

        ''' <summary>
        ''' Gets or sets the sample text
        ''' </summary>
        Public Property SelectedFiles As String()
            Get
                Return _SelectedFiles
            End Get
            Set(value As String())
                RaiseAndSetIfChanged(_SelectedFiles, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets the select files interaction
        ''' </summary>
        Public ReadOnly Property SelectFilesInteraction As New Interaction(Of String, String())

        Public ReadOnly Property SelectFilesCommand As ICommand = ReactiveCommand.CreateFromTask(AddressOf SelectFiles)

        Private Async Function SelectFiles() As Task
            SelectedFiles = Await SelectFilesInteraction.Handle("Hello from Avalonia")
        End Function
    End Class
End Namespace