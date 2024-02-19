Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		' Add our SimpleViewModel.
		' Note: We need at least a get-accessor for our Properties.
		Public ReadOnly Property SimpleViewModel() As New SimpleViewModel()


		' Add our ReactiveViewModel
		Public ReadOnly Property ReactiveViewModel() As New ReactiveViewModel()
	End Class
End Namespace
