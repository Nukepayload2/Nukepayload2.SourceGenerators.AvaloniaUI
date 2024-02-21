Namespace ViewModels
	''' <summary>
	''' An abstract class for enabling page navigation.
	''' </summary>
	Public MustInherit Class PageViewModelBase
		Inherits ViewModelBase

		''' <summary>
		''' Gets if the user can navigate to the next page
		''' </summary>
		Public MustOverride ReadOnly Property CanNavigateNext As Boolean

		Protected Overridable Sub SetCanNavigateNext(value As Boolean)
			Throw New NotImplementedException()
		End Sub

		''' <summary>
		''' Gets if the user can navigate to the previous page
		''' </summary>
		Public MustOverride ReadOnly Property CanNavigatePrevious As Boolean

		Protected Overridable Sub SetCanNavigatePrevious(value As Boolean)
			Throw New NotImplementedException()
		End Sub

	End Class
End Namespace
