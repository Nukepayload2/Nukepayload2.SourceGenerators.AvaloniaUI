Imports ReactiveUI
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		Public ReadOnly Property Greeting() As String
			Get
				Return "Welcome to Avalonia!"
			End Get
		End Property


		' the initial value is 6
		Private _NumberOfStars As Integer = 6

		''' <summary>
		''' Gets or sets the number of stars
		''' </summary>
		Public Property NumberOfStars() As Integer
			Get
				Return _NumberOfStars
			End Get
			Set(value As Integer)
				Me.RaiseAndSetIfChanged(_NumberOfStars, value)
			End Set
		End Property


		' the initial value is 2
		Private _RatingValue As Integer = 2

		''' <summary>
		''' Gets or sets the current rating value. 
		''' It must be between 0 and 5
		''' </summary>
		<Range(0, 5)>
		Public Property RatingValue() As Integer
			Get
				Return _RatingValue
			End Get
			Set(value As Integer)
				Me.RaiseAndSetIfChanged(_RatingValue, value)
			End Set
		End Property
	End Class
End Namespace
