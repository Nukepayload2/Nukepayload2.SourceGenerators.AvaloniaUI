Imports ReactiveUI
Imports System

Namespace ViewModels
	' Instead of implementing "INotifyPropertyChanged" on our own we use "ReactiveObject" as 
	' our base class. Read more about it here: https://www.reactiveui.net
	Public Class ReactiveViewModel
		Inherits ReactiveObject

		Public Sub New()
			' We can listen to any property changes with "WhenAnyValue" and do whatever we want in "Subscribe".
			Me.WhenAnyValue(Function(o) o.Name).Subscribe(Sub(o) Me.RaisePropertyChanged(NameOf(Greeting)))
		End Sub

'INSTANT VB WARNING: Nullable reference types have no equivalent in VB:
'ORIGINAL LINE: private string? _Name;
		Private _Name As String ' This is our backing field for Name

'INSTANT VB WARNING: Nullable reference types have no equivalent in VB:
'ORIGINAL LINE: public string? Name
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				' We can use "RaiseAndSetIfChanged" to check if the value changed and automatically notify the UI
				Me.RaiseAndSetIfChanged(_Name, value)
			End Set
		End Property

		' Greeting will change based on a Name.
		Public ReadOnly Property Greeting() As String
			Get
				If String.IsNullOrEmpty(Name) Then
					' If no Name is provided, use a default Greeting
					Return "Hello World from Avalonia.Samples"
				Else
					' else Greet the User.
					Return $"Hello {Name}"
				End If
			End Get
		End Property
	End Class
End Namespace
