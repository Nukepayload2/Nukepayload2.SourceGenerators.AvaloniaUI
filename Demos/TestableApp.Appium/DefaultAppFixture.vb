Imports System.Globalization
Imports System.Runtime.InteropServices
Imports OpenQA.Selenium.Appium
Imports OpenQA.Selenium.Appium.Enums
Imports OpenQA.Selenium.Appium.Mac
Imports OpenQA.Selenium.Appium.Windows
Imports Xunit

Namespace Appium

	<CollectionDefinition("Default")>
	Public Class DefaultCollection
		Implements ICollectionFixture(Of DefaultAppFixture)

	End Class

	Public Class DefaultAppFixture
		Implements IDisposable

		Private Const TestAppPath = "..\..\..\..\TestableApp\bin\Debug\net8.0\TestableApp.exe"

		Private Const TestAppBundleId = "net.avaloniaui.testableApp"

		Public Sub New()
			Dim options = New AppiumOptions

			If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
				ConfigureWin32Options(options)
				Session = New WindowsDriver(Of AppiumWebElement)(New Uri("http://127.0.0.1:4723"), options)

				' https://github.com/microsoft/WinAppDriver/issues/1025
				SetForegroundWindow(New IntPtr(Integer.Parse(Session.WindowHandles(0).Substring(2), NumberStyles.AllowHexSpecifier)))
			ElseIf RuntimeInformation.IsOSPlatform(OSPlatform.OSX) Then
				ConfigureMacOptions(options)
				Session = New MacDriver(Of AppiumWebElement)(New Uri("http://127.0.0.1:4723/wd/hub"), options)
			Else
				Throw New NotSupportedException("Unsupported platform.")
			End If
		End Sub

		Protected Overridable Sub ConfigureWin32Options(options As AppiumOptions)
			Dim path = System.IO.Path.GetFullPath(TestAppPath)
			options.AddAdditionalCapability(MobileCapabilityType.App, path)
			options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Windows)
			options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC")
			' options.AddAdditionalCapability("appArguments", "--customArg");
		End Sub

		Protected Overridable Sub ConfigureMacOptions(options As AppiumOptions)
			options.AddAdditionalCapability("appium:bundleId", TestAppBundleId)
			options.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.MacOS)
			options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "mac2")
			options.AddAdditionalCapability("appium:showServerLogs", True)
			' options.AddAdditionalCapability("appium:arguments", new[] { "--customArg" });
		End Sub

		Public ReadOnly Property Session() As AppiumDriver(Of AppiumWebElement)

		Public Sub Dispose() Implements IDisposable.Dispose
			Try
				Session.Close()
			Catch
				' Closing the session currently seems to crash the mac2 driver.
			End Try
		End Sub

		<DllImport("user32.dll")>
		Private Shared Function SetForegroundWindow(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
		End Function
	End Class

End Namespace
