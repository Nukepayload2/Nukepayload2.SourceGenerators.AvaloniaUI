Imports FuncDataTemplateSample.Models

Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		Public ReadOnly Property People As New List(Of Person) From {
			New Person With {
				.FirstName = "Mr.",
				.LastName = "X",
				.Sex = Sex.Diverse
			},
			New Person With {
				.FirstName = "Hello",
				.LastName = "World",
				.Sex = Sex.Male
			},
			New Person With {
				.FirstName = "Hello",
				.LastName = "Kitty",
				.Sex = Sex.Female
			}
		}
	End Class
End Namespace
