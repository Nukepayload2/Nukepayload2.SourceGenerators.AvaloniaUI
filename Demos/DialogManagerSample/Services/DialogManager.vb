Imports Avalonia
Imports Avalonia.Controls
Imports Avalonia.Platform.Storage

Namespace Services
	Public Class DialogManager
		Private Shared ReadOnly RegistrationMapper As New Dictionary(Of Object, Visual)

		Shared Sub New()
			RegisterProperty.Changed.AddClassHandler(Of Visual)(AddressOf RegisterChanged)
		End Sub

		Private Shared Sub RegisterChanged(sender As Visual, e As AvaloniaPropertyChangedEventArgs)
			If sender Is Nothing Then
				Throw New InvalidOperationException("The DialogManager can only be registered on a Visual")
			End If

			' Unregister any old registered context
			If e.OldValue IsNot Nothing Then
				RegistrationMapper.Remove(e.OldValue)
			End If

			' Register any new context
			If e.NewValue IsNot Nothing Then
				RegistrationMapper.Add(e.NewValue, sender)
			End If
		End Sub

		''' <summary>
		''' This property handles the registration of Views and ViewModel
		''' </summary>
		Public Shared ReadOnly RegisterProperty As AttachedProperty(Of Object) =
			AvaloniaProperty.RegisterAttached(Of DialogManager, Visual, Object)("Register")

		''' <summary>
		''' Accessor for Attached property <see cref="RegisterProperty"/>.
		''' </summary>
		Public Shared Sub SetRegister(element As AvaloniaObject, value As Object)
			element.SetValue(RegisterProperty, value)
		End Sub

		''' <summary>
		''' Accessor for Attached property <see cref="RegisterProperty"/>.
		''' </summary>
		Public Shared Function GetRegister(element As AvaloniaObject) As Object
			Return element.GetValue(RegisterProperty)
		End Function

		''' <summary>
		''' Gets the associated <see cref="Visual"/> for a given context. Returns null, if none was registered
		''' </summary>
		''' <param name="context">The context to lookup</param>
		''' <returns>The registered Visual for the context or null if none was found</returns>
		Public Shared Function GetVisualForContext(context As Object) As Visual
			Dim result As Visual = Nothing
			Return If(RegistrationMapper.TryGetValue(context, result), result, Nothing)
		End Function

		''' <summary>
		''' Gets the parent <see cref="TopLevel"/> for the given context. Returns null, if no TopLevel was found
		''' </summary>
		''' <param name="context">The context to lookup</param>
		''' <returns>The registered TopLevel for the context or null if none was found</returns>
		Public Shared Function GetTopLevelForContext(context As Object) As TopLevel
			Return TopLevel.GetTopLevel(GetVisualForContext(context))
		End Function
	End Class

	''' <summary>
	''' A helper class to manage dialogs via extension methods. Add more on your own
	''' </summary>
	Public Module DialogHelper
		''' <summary>
		''' Shows an open file dialog for a registered context, most likely a ViewModel
		''' </summary>
		''' <param name="context">The context</param>
		''' <param name="title">The dialog title or a default is null</param>
		''' <param name="selectMany">Is selecting many files allowed?</param>
		''' <returns>An array of file names</returns>
		''' <exception cref="ArgumentNullException">if context was null</exception>
		<System.Runtime.CompilerServices.Extension>
		Public Async Function OpenFileDialogAsync(context As Object, Optional title As String = Nothing, Optional selectMany As Boolean = True) As Task(Of IEnumerable(Of String))
			If context Is Nothing Then
				Throw New ArgumentNullException(NameOf(context))
			End If

			' lookup the TopLevel for the context
			Dim topLevel = DialogManager.GetTopLevelForContext(context)

			If topLevel IsNot Nothing Then
				' Open the file dialog
				Dim storageFiles = Await topLevel.StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
					.AllowMultiple = selectMany,
					.Title = If(title, "Select any file(s)")
				})

				' return the result
				Return storageFiles.Select(Function(s) s.Name)
			End If
			Return Nothing
		End Function

	End Module
End Namespace
