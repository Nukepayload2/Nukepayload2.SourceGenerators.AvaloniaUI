Imports Avalonia.Input

Namespace Model

    Public Class Game
        Inherits GameBase

        Private ReadOnly _field As GameField

        Public Sub New(field As GameField)
            _field = field
        End Sub

        Private ReadOnly Property Random As New Random

        Protected Overrides Sub Tick()
            If Not _field.Player.IsMoving Then
                If Keyboard.IsKeyDown(Key.Up) Then
                    _field.Player.SetTarget(Facing.North)
                ElseIf Keyboard.IsKeyDown(Key.Down) Then
                    _field.Player.SetTarget(Facing.South)
                ElseIf Keyboard.IsKeyDown(Key.Left) Then
                    _field.Player.SetTarget(Facing.West)
                ElseIf Keyboard.IsKeyDown(Key.Right) Then
                    _field.Player.SetTarget(Facing.East)
                End If
            End If

            For Each tank In _field.GameObjects.OfType(Of Tank)()
                If Not tank.IsMoving Then
                    If Not tank.SetTarget(tank.Facing) Then
                        If Not tank.SetTarget(CType(Random.Next(4), Facing)) Then
                            tank.SetTarget(CType(Nothing, Facing?))
                        End If
                    End If
                End If
            Next tank

            For Each obj In _field.GameObjects.OfType(Of MovingGameObject)()
                obj.MoveToTarget()
            Next
        End Sub
    End Class
End Namespace
