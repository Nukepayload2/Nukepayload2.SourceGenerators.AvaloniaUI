Imports Avalonia.Headless
Imports Avalonia.Headless.XUnit
Imports Avalonia.Input
Imports TestableApp.ViewModels
Imports TestableApp.Views
Imports Xunit

Namespace XUnit

	Public Class CalculatorTests
		<AvaloniaFact>
		Public Sub Should_Add_Numbers()
			Dim window = New MainWindow With {.DataContext = New MainWindowViewModel}

			window.Show()

			' Set values to the input boxes by simulating text input:
			window.FirstOperandInput.Focus()
			window.KeyTextInput("10")

			' Or directly to the control:
			window.SecondOperandInput.Text = "20"

			' Raise click event on the button:
			window.AddButton.Focus()
			window.KeyPress(Key.Enter, RawInputModifiers.None)

			Assert.Equal("30", window.ResultBox.Text)
		End Sub

		<AvaloniaFact>
		Public Sub Cannot_Divide_By_Zero()
			Dim window = New MainWindow With {.DataContext = New MainWindowViewModel}

			window.Show()

			' Set values to the input boxes by simulating text input:
			window.SecondOperandInput.Text = "10"
			window.SecondOperandInput.Text = "0"

			' Raise click event on the button:
			window.DivideButton.Focus()
			window.KeyPress(Key.Enter, RawInputModifiers.None)

			Assert.Equal("Cannot divide by zero!", window.ResultBox.Text)
		End Sub
	End Class
End Namespace
