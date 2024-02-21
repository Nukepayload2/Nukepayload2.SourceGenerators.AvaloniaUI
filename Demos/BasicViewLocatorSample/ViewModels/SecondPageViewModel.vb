Imports ReactiveUI
Imports System
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
	''' <summary>
	'''  This is our ViewModel for the second page
	''' </summary>
	Public Class SecondPageViewModel
		Inherits PageViewModelBase


		Public Sub New()
			' Listen to changes of MailAddress and Password and update CanNavigateNext accordingly
			WhenAnyValue(Function(x) x.MailAddress, Function(x) x.Password).
				Subscribe(Sub() UpdateCanNavigateNext())
		End Sub


		Private _MailAddress As String

		''' <summary>
		''' The E-Mail of the user. This field is required
		''' </summary>
		<Required>
		<EmailAddress>
		Public Property MailAddress As String
			Get
				Return _MailAddress
			End Get
			Set(value As String)
				RaiseAndSetIfChanged(_MailAddress, value)
			End Set
		End Property

		Private _Password As String

		''' <summary>
		''' The password of the user. This field is required.
		''' </summary>
		<Required>
		Public Property Password As String
			Get
				Return _Password
			End Get
			Set(value As String)
				RaiseAndSetIfChanged(_Password, value)
			End Set
		End Property

		Private _CanNavigateNext As Boolean

		' For this page the user can only go to the next page if all fields are valid. So we need a private setter.
		Public Overrides ReadOnly Property CanNavigateNext As Boolean
			Get
				Return _CanNavigateNext
			End Get
		End Property

		Protected Overrides Sub SetCanNavigateNext(value As Boolean)
			RaiseAndSetIfChanged(_CanNavigateNext, value)
		End Sub

		' We allow navigate back in any case
		Public Overrides ReadOnly Property CanNavigatePrevious As Boolean
			Get
				Return True
			End Get
		End Property

		' Update CanNavigateNext. Allow next page if E-Mail and Password are valid
		Private Sub UpdateCanNavigateNext()
			SetCanNavigateNext(Not String.IsNullOrEmpty(_MailAddress) AndAlso
							   _MailAddress.Contains("@"c) AndAlso
							   Not String.IsNullOrEmpty(_Password))
		End Sub

	End Class
End Namespace
