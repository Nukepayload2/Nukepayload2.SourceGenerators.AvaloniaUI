Imports BattleCity.Infrastructure
Imports Avalonia

Namespace Model

	Public MustInherit Class GameObject
		Inherits PropertyChangedBase

		Private _location As Point

		Protected Sub New(location As Point)
			Me.Location = location
		End Sub

		Public Property Location As Point
			Get
				Return _location
			End Get
			Protected Set(value As Point)
				If value.Equals(_location) Then
					Return
				End If
				_location = value
				OnPropertyChanged()
			End Set
		End Property

		Public Overridable ReadOnly Property Layer As Integer
			Get
				Return 0
			End Get
		End Property
	End Class

End Namespace
