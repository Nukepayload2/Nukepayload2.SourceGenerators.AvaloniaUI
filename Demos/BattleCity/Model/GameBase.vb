Imports Avalonia.Threading

Namespace Model

    Public MustInherit Class GameBase
        Public Const TicksPerSecond = 60

        Private WithEvents Timer As New DispatcherTimer With {
            .Interval = New TimeSpan(0, 0, 0, 0, 1000 \ TicksPerSecond)
        }

        Public Property CurrentTick As Long

        Private Sub DoTick() Handles Timer.Tick
            Tick()
            CurrentTick += 1
        End Sub

        Protected MustOverride Sub Tick()

        Public Sub Start()
            _timer.IsEnabled = True
        End Sub

        Public Sub [Stop]()
            _timer.IsEnabled = False
        End Sub
    End Class
End Namespace
