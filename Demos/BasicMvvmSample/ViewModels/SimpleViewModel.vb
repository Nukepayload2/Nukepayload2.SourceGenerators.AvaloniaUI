Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace ViewModels
	' This is our simple ViewModel. We need to implement the interface "INotifyPropertyChanged"
	' in order to notify the View if any of our properties changed.
	Public Class SimpleViewModel
		Implements INotifyPropertyChanged

		' This event is implemented by "INotifyPropertyChanged" and is all we need to inform 
		' our View about changes.
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		' For convenience we add a method which will raise the above event.
		'INSTANT VB WARNING: Nullable reference types have no equivalent in VB:
		'ORIGINAL LINE: private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
		Private Sub RaisePropertyChanged(<CallerMemberName> Optional ByVal propertyName As String = Nothing)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub

		' ---- Add some Properties ----

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
				' We only want to update the UI if the Name actually changed, so we check if the value is actually new
				If _Name <> value Then
					' 1. update our backing field
					_Name = value

					' 2. We call RaisePropertyChanged() to notify the UI about changes. 
					' We can omit the property name here because [CallerMemberName] will provide it for us.  
					RaisePropertyChanged()

					' 3. Greeting also changed. So let's notify the UI about it. 
					RaisePropertyChanged(NameOf(Greeting))
				End If
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
