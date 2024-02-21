Imports Avalonia
Imports Avalonia.Headless

<Assembly: AvaloniaTestApplication(GetType(TestAppBuilder))>

Public Class TestAppBuilder
	Public Function BuildAvaloniaApp() As AppBuilder
		Return AppBuilder.Configure(Of App)().UseHeadless(New AvaloniaHeadlessPlatformOptions())
	End Function
End Class
