Imports System.Windows.Input

Namespace Core
	''' <summary>
	''' Simple implementation of Interaction pattern from ReactiveUI framework.
	''' https://www.reactiveui.net/docs/handbook/interactions/
	''' </summary>
	Public NotInheritable Class Interaction(Of TInput, TOutput)
		Implements IDisposable, ICommand

		' this is a reference to the registered interaction handler. 
		Private _handler As Func(Of TInput, Task(Of TOutput))

		''' <summary>
		''' Performs the requested interaction <see langword="async"/>. Returns the result provided by the View
		''' </summary>
		''' <param name="input">The input parameter</param>
		''' <returns>The result of the interaction</returns>
		''' <exception cref="InvalidOperationException"></exception>
		Public Function HandleAsync(input As TInput) As Task(Of TOutput)
			If _handler Is Nothing Then
				Throw New InvalidOperationException("Handler wasn't registered")
			End If

			Return _handler(input)
		End Function

		''' <summary>
		''' Registers a handler to our Interaction
		''' </summary>
		''' <param name="handler">the handler to register</param>
		''' <returns>a disposable object to clean up memory if not in use anymore/></returns>
		''' <exception cref="InvalidOperationException"></exception>
		Public Function RegisterHandler(handler As Func(Of TInput, Task(Of TOutput))) As IDisposable
			If _handler IsNot Nothing Then
				Throw New InvalidOperationException("Handler was already registered")
			End If

			_handler = handler
			RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
			Return Me
		End Function

		Public Sub Dispose() Implements IDisposable.Dispose
			_handler = Nothing
		End Sub

		Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
			Return _handler IsNot Nothing
		End Function

		Public Sub Execute(parameter As Object) Implements ICommand.Execute
			HandleAsync(parameter)
		End Sub

		Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
	End Class
End Namespace
