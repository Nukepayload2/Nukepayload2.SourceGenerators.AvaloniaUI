Imports Avalonia
Imports BattleCity.Model
Imports Avalonia.Controls.ApplicationLifetimes
Imports Avalonia.Markup.Xaml

Public Class App
    Inherits Application

    Public Overrides Sub Initialize()
        AvaloniaXamlLoader.Load(Me)
    End Sub

    Public Overrides Sub OnFrameworkInitializationCompleted()
        If TypeOf ApplicationLifetime Is IClassicDesktopStyleApplicationLifetime Then
            Dim lifetime = DirectCast(ApplicationLifetime, IClassicDesktopStyleApplicationLifetime)
            Dim mainWindow As New MainWindow

            Dim field As New GameField
            Dim game As New Game(field)
            game.Start()
            mainWindow.DataContext = field

            lifetime.MainWindow = mainWindow
        End If
    End Sub
End Class
