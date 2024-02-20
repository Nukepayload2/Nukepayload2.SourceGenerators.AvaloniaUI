Imports Avalonia.Controls
Imports Avalonia.Input

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Partial Public Class MainWindow
	Inherits Window

	Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
		Keyboard.Keys.Add(e.Key)
	End Sub

	Private Sub MainWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
		Keyboard.Keys.Remove(e.Key)
	End Sub
End Class
