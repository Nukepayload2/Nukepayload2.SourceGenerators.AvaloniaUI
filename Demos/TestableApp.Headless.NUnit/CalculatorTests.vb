Imports Avalonia.Headless
Imports Avalonia.Headless.NUnit
Imports Avalonia.Input
Imports NUnit.Framework
Imports TestableApp.ViewModels
Imports TestableApp.Views

Namespace NUnit

	Public Class CalculatorTests
		<AvaloniaTest>
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

			Assert.That(window.ResultBox.Text, [Is].EqualTo("30"))
		End Sub

		<AvaloniaTest>
		Public Sub Cannot_Divide_By_Zero()
			Dim window = New MainWindow With {.DataContext = New MainWindowViewModel}

			window.Show()

			' Set values to the input boxes by simulating text input:
			window.SecondOperandInput.Text = "10"
			window.SecondOperandInput.Text = "0"

			' Raise click event on the button:
			window.DivideButton.Focus()
			window.KeyPress(Key.Enter, RawInputModifiers.None)

			Assert.That(window.ResultBox.Text, [Is].EqualTo("Cannot divide by zero!"))
		End Sub
	End Class
End Namespace
