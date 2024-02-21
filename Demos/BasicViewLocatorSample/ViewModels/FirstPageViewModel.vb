
Namespace ViewModels
	''' <summary>
	'''  This is our ViewModel for the first page
	''' </summary>
	Public Class FirstPageViewModel
		Inherits PageViewModelBase

		''' <summary>
		''' The Title of this page
		''' </summary>
		Public ReadOnly Property Title As String
			Get
				Return "Welcome to our Wizard-Sample."
			End Get
		End Property

		''' <summary>
		''' The content of this page
		''' </summary>
		Public ReadOnly Property Message As String
			Get
				Return "Press ""Next"" to register yourself."
			End Get
		End Property

		' This is our first page, so we can navigate to the next page in any case
		Public Overrides ReadOnly Property CanNavigateNext As Boolean
			Get
				Return True
			End Get
		End Property

		' You cannot go back from this page
		Public Overrides ReadOnly Property CanNavigatePrevious As Boolean
			Get
				Return False
			End Get
		End Property

	End Class
End Namespace
