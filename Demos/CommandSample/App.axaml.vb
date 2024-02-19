Imports Avalonia
Imports Avalonia.Controls.ApplicationLifetimes
Imports Avalonia.Markup.Xaml
Imports CommandSample.ViewModels
Imports CommandSample.Views

Partial Public Class App
    Inherits Application

    Public Overrides Sub Initialize()
        AvaloniaXamlLoader.Load(Me)
    End Sub

    Public Overrides Sub OnFrameworkInitializationCompleted()
        If TypeOf ApplicationLifetime Is IClassicDesktopStyleApplicationLifetime Then
            Dim desktop As IClassicDesktopStyleApplicationLifetime = DirectCast(ApplicationLifetime, IClassicDesktopStyleApplicationLifetime)
            desktop.MainWindow = New MainWindow With {.DataContext = New MainWindowViewModel()}
        End If

        MyBase.OnFrameworkInitializationCompleted()
    End Sub
End Class
