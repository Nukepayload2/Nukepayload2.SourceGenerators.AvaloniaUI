Imports Avalonia.Controls

Namespace Views
	Partial Public Class MainWindow
		Inherits Window

        Private Sub TblName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TblName.TextChanged
            TblGreeting.Text = "Hello, " & TblName.Text
        End Sub
    End Class
End Namespace
