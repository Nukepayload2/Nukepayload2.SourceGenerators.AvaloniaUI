Imports IDataTemplateSample.Models
Imports ReactiveUI

Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		Private _SelectedShape As ShapeType

		''' <summary>
		''' Gets or sets the selected ShapeType
		''' </summary>
		Public Property SelectedShape As ShapeType
			Get
				Return _SelectedShape
			End Get
			Set(value As ShapeType)
				Me.RaiseAndSetIfChanged(_SelectedShape, value)
			End Set
		End Property

		''' <summary>
		'''  Gets an array of all available ShapeTypes
		''' </summary>
		Public ReadOnly Property AvailableShapes As ShapeType() = [Enum].GetValues(Of ShapeType)()
	End Class
End Namespace
