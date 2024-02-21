Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input

Namespace ViewModels

	Partial Public Class MainWindowViewModel
		Inherits ObservableObject

		Dim _SecondOperand As Double?
		Public Property SecondOperand As Double?
			Get
				Return _secondOperand
			End Get
			Set(value As Double?)
				SetProperty(_SecondOperand, value)
			End Set
		End Property

		Dim _FirstOperand As Double?
		Public Property FirstOperand As Double?
			Get
				Return _firstOperand
			End Get
			Set(value As Double?)
				SetProperty(_FirstOperand, value)
			End Set
		End Property

		Dim _Result As String
		Public Property Result As String
			Get
				Return _result
			End Get
			Set(value As String)
				SetProperty(_Result, value)
			End Set
		End Property

		Public ReadOnly Property AddCommand As New RelayCommand(
			Sub() Result = FirstOperand + SecondOperand)

		Public ReadOnly Property SubtractCommand As New RelayCommand(
			Sub() Result = FirstOperand - SecondOperand)

		Public ReadOnly Property MultiplyCommand As New RelayCommand(
			Sub() Result = FirstOperand * SecondOperand)

		Public ReadOnly Property DivideCommand As New RelayCommand(
			Sub()
				Result = If(SecondOperand = 0, "Cannot divide by zero!", FirstOperand / SecondOperand)
			End Sub)

	End Class

End Namespace
