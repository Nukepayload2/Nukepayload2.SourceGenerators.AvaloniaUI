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
		''' Gets or sets the sex of the person. You can only set this property on init. 
		''' </summary>
		Public Property Sex As Sex

		' Override ToString()
		Public Overrides Function ToString() As String
			Return $"{FirstName} {LastName}"
		End Function
	End Class
End Namespace
