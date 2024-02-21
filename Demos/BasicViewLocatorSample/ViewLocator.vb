Imports Avalonia.Controls
Imports Avalonia.Controls.Templates
Imports BasicViewLocatorSample.ViewModels

Public Class ViewLocator
	Implements IDataTemplate

	Public Function Build(data As Object) As Control Implements IDataTemplate.Build
		If data Is Nothing Then
			Return New TextBlock With {.Text = "data was null"}
		End If

		Dim name = data.GetType().FullName.Replace("ViewModel", "View")
		Dim type = System.Type.GetType(name)

		If type IsNot Nothing Then
			Return DirectCast(Activator.CreateInstance(type), Control)
		Else
			Return New TextBlock With {.Text = "Not Found: " & name}
		End If
	End Function

	Public Function Match(data As Object) As Boolean Implements IDataTemplate.Match
		Return TypeOf data Is ViewModelBase
	End Function
End Class
