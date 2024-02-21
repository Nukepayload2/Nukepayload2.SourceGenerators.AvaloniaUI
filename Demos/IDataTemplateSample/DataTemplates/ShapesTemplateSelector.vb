Imports Avalonia.Controls.Templates
Imports Avalonia.Controls
Imports Avalonia.Metadata
Imports IDataTemplateSample.Models

Namespace DataTemplates
	Public Class ShapesTemplateSelector
		Implements IDataTemplate

		' This Dictionary should store our shapes. We mark this as [Content], so we can directly add elements to it later.
		<Content>
		Public ReadOnly Property AvailableTemplates As New Dictionary(Of String, IDataTemplate)

		' Build the DataTemplate here
		Public Function Build(param As Object) As Control Implements IDataTemplate.Build
			Dim key = param?.ToString() ' Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
			If key Is Nothing Then ' If the key is null, we throw an ArgumentNullException
				Throw New ArgumentNullException(NameOf(param))
			End If
			Return AvailableTemplates(key).Build(param) ' finally we look up the provided key and let the System build the DataTemplate for us
		End Function

		' Check if we can accept the provided data
		Public Function Match(data As Object) As Boolean Implements IDataTemplate.Match
			' Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
			Dim key = data?.ToString()

			Return TypeOf data Is ShapeType AndAlso Not String.IsNullOrEmpty(key) AndAlso
				AvailableTemplates.ContainsKey(key) ' and the key must be found in our Dictionary
		End Function
	End Class
End Namespace
