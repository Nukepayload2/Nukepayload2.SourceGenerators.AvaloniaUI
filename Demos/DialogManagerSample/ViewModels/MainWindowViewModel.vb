Imports DialogManagerSample.Services
Imports CommunityToolkit.Mvvm.Input

Namespace ViewModels

	Partial Public Class MainWindowViewModel
		Inherits ViewModelBase

		Dim _SelectedFiles As IEnumerable(Of String)
		''' <summary>
		''' Gets or sets a list of Files
		''' </summary>
		Public Property SelectedFiles As IEnumerable(Of String)
			Get
				Return _SelectedFiles
			End Get
			Set(value As IEnumerable(Of String))
				SetProperty(_SelectedFiles, value)
			End Set
		End Property

		''' <summary>
		''' A command used to select some files
		''' </summary>
		Public ReadOnly Property SelectFilesCommand As New AsyncRelayCommand(
		Async Function()
			SelectedFiles = Await OpenFileDialogAsync("Hello Avalonia")
		End Function)
	End Class
End Namespace
