Imports Avalonia.Controls
Imports Avalonia.Platform.Storage
Imports MvvmDialogSample.ViewModels

Namespace Views
	Partial Public Class CustomInteractionView
		Inherits UserControl

		' Stores a reference to the disposable in order to clean it up if needed
		Private _selectFilesInteractionDisposable As IDisposable

		Private Sub CustomInteractionView_DataContextChanged(sender As Object, e As EventArgs) Handles Me.DataContextChanged
			' Dispose any old handler
			_selectFilesInteractionDisposable?.Dispose()

			Dim vm = CType(DataContext, CustomInteractionViewModel)
			If vm IsNot Nothing Then
				' register the interaction handler
				_selectFilesInteractionDisposable = vm.SelectFilesInteraction.RegisterHandler(AddressOf InteractionHandler)
			End If
		End Sub

		Private Async Function InteractionHandler(input As String) As Task(Of String())
			' Get a reference to our TopLevel (in our case the parent Window)
			Dim topLevel1 = TopLevel.GetTopLevel(Me)

			' Try to get the files
			Dim storageFiles = Await topLevel1.StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions() With {
				.AllowMultiple = True,
				.Title = input
			})

			' Transform the files as needed and return them. If no file was selected, null will be returned
			Return storageFiles?.Select(Function(x) x.Name)?.ToArray()
		End Function
	End Class
End Namespace
