Imports Avalonia.Controls
Imports Avalonia.Platform.Storage
Imports Avalonia.ReactiveUI
Imports MvvmDialogSample.ViewModels
Imports ReactiveUI

Namespace Views
	Partial Public Class InteractionView
		Inherits ReactiveUserControl(Of InteractionViewModel)

		Sub New()
			InitializeComponent()

			WhenActivated(Sub(d) d(ViewModel.SelectFilesInteraction.RegisterHandler(AddressOf InteractionHandler)))
		End Sub

		Private Async Function InteractionHandler(context As InteractionContext(Of String, String())) As Task
			' Get our parent top level control in order to get the needed service (in our sample the storage provider. Can also be the clipboard etc.)
			Dim topLevel1 = TopLevel.GetTopLevel(Me)
			Dim storageFiles = Await topLevel1.StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
				.AllowMultiple = True,
				.Title = context.Input
			})
			context.SetOutput(storageFiles?.Select(Function(x) x.Name).ToArray())
		End Function
	End Class
End Namespace
