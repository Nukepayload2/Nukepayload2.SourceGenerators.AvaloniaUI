Namespace Models
	Public Class Person
		''' <summary>
		''' Gets or sets the first name of the person. You can only set this property on init. 
		''' </summary>
		Public Property FirstName As String

		''' <summary>
		''' Gets or sets the last name of the person. You can only set this property on init. 
		''' </summary>
		Public Property LastName As String

		''' <summary>
		''' Gets or sets the age of the person. You can only set this property on init. 
		''' </summary>
		Public Property Age As Integer

		''' <summary>
		''' Gets or sets the sex of the person. You can only set this property on init. 
		''' </summary>
		Public Property Sex As Sex

		' The default DataTemplate will show whatever ToString() will provide. So as a first idea let's change what ToString() will provide.
		Public Overrides Function ToString() As String
			Return $"{FirstName} {LastName} (Age: {Age}, Sex: {Sex})"
		End Function
	End Class
End Namespace
