Namespace ViewModels
	''' <summary>
	'''  This is our ViewModel for the first page
	''' </summary>
	Public Class ThirdPageViewModel
		Inherits PageViewModelBase

		' The message to display
		Public ReadOnly Property Message As String
			Get
				Return "Done"
			End Get
		End Property

		' This is the last page, so we cannot navigate next in our sample. 
		Public Overrides ReadOnly Property CanNavigateNext As Boolean
			Get
				Return False
			End Get
		End Property

		' We navigate back form this page in any case
		Public Overrides ReadOnly Property CanNavigatePrevious As Boolean
			Get
				Return True
			End Get
		End Property

	End Class
End Namespace
