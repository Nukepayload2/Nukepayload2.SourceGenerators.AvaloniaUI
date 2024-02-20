Namespace Model

	Public Class Player
		Inherits MovingGameObject

		Public Sub New(field As GameField, location As CellLocation, facing As Facing)
			MyBase.New(field, location, facing)
		End Sub
	End Class
End Namespace
