Imports Avalonia
Imports Avalonia.Controls.ApplicationLifetimes
Imports Avalonia.Data.Core
Imports Avalonia.Data.Core.Plugins
Imports Avalonia.Markup.Xaml
Imports TestableApp.ViewModels
Imports TestableApp.Views

Partial Public Class App
    Inherits Application

    Public Overrides Sub Initialize()
        AvaloniaXamlLoader.Load(Me)
    End Sub

    Public Overrides Sub OnFrameworkInitializationCompleted()
        If TypeOf ApplicationLifetime Is IClassicDesktopStyleApplicationLifetime Then
            Dim desktop = DirectCast(ApplicationLifetime, IClassicDesktopStyleApplicationLifetime)
            ' Line below is needed to remove Avalonia data validation.
            ' Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0)
            desktop.MainWindow = New MainWindow With {.DataContext = New MainWindowViewModel}
        End If

        MyBase.OnFrameworkInitializationCompleted()
    End Sub
End Class
