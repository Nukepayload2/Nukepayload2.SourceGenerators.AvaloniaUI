Imports BasicDataTemplateSample.Models

Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		''' <summary>
		''' As this is a list of Persons, we can add Students and Teachers here. 
		''' </summary>
		Public ReadOnly Property People As New List(Of Person) From {
			New Teacher With {
				.FirstName = "Mr.",
				.LastName = "X",
				.Age = 55,
				.Sex = Sex.Diverse,
				.Subject = "My Favorite Subject"
			},
			New Student With {
				.FirstName = "Hello",
				.LastName = "World",
				.Age = 17,
				.Sex = Sex.Male,
				.Grade = 12
			},
			New Student With {
				.FirstName = "Hello",
				.LastName = "Kitty",
				.Age = 12,
				.Sex = Sex.Female,
				.Grade = 6
			}
		}
	End Class
End Namespace
