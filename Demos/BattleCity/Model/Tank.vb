Namespace Model

	Public Class Tank
		Inherits MovingGameObject

		Private ReadOnly _speed As Double

		Public Sub New(field As GameField, location As CellLocation, facing As Facing, speed As Double)
			MyBase.New(field, location, facing)
			_speed = speed
		End Sub

		Protected Overrides ReadOnly Property SpeedFactor As Double
			Get
				Return _speed * MyBase.SpeedFactor
			End Get
		End Property
	End Class
End Namespace
