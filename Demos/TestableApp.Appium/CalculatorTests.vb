Imports OpenQA.Selenium.Appium
Imports Xunit

Namespace Appium

	<Collection("Default")>
	Public Class CalculatorTests
		Private ReadOnly _session As AppiumDriver(Of AppiumWebElement)


		Public Sub New(fixture As DefaultAppFixture)
			_session = fixture.Session
		End Sub

		<Fact>
		Public Sub Should_Add_Numbers()
			' Assert:
			Dim firstOperandInput = _session.FindElementByAccessibilityId("FirstOperandInput")
			Dim secondOperandInput = _session.FindElementByAccessibilityId("SecondOperandInput")
			Dim addButton = _session.FindElementByAccessibilityId("AddButton")
			Dim resultBox = _session.FindElementByAccessibilityId("ResultBox")

			' Act:
			firstOperandInput.Clear()
			firstOperandInput.SendKeys("10")
			secondOperandInput.Clear()
			secondOperandInput.SendKeys("20")

			addButton.Click()

			' Assert:
			Assert.Equal("30", resultBox.Text)
		End Sub

		<Fact>
		Public Sub Cannot_Divide_By_Zero()
			' Assert:
			Dim firstOperandInput = _session.FindElementByAccessibilityId("FirstOperandInput")
			Dim secondOperandInput = _session.FindElementByAccessibilityId("SecondOperandInput")
			Dim divideButton = _session.FindElementByAccessibilityId("DivideButton")
			Dim resultBox = _session.FindElementByAccessibilityId("ResultBox")

			' Act:
			firstOperandInput.Clear()
			firstOperandInput.SendKeys("10")
			secondOperandInput.Clear()
			secondOperandInput.SendKeys("0")

			divideButton.Click()

			' Assert:
			Assert.Equal("Cannot divide by zero!", resultBox.Text)
		End Sub
	End Class
End Namespace
