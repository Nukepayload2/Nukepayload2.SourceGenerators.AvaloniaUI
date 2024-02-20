Imports System.Collections.ObjectModel
Imports Avalonia
Imports BattleCity.Infrastructure

Namespace Model

    Public Class GameField
        Inherits PropertyChangedBase

        Public Const CellSize = 32.0

        Public Sub New()
            MyClass.New(20, 15)
        End Sub

        Public Sub New(width As Integer, height As Integer)
            Me.Width = width
            Me.Height = height

            Tiles = New TerrainTile(width - 1, height - 1) {}
            For x = 0 To width - 1
                For y = 0 To height - 1
                    Tiles(x, y) = New TerrainTile(New Point(x * CellSize, y * CellSize), GetTypeForCoords(x, y))
                    GameObjects.Add(Tiles(x, y))
                Next y
            Next x

            Player = New Player(Me, New CellLocation(width \ 2, height \ 2), Facing.East)
            GameObjects.Add(Player)

            Dim c = 0
            Do While c < 10
                Dim x = Random.Next(Me.Width - 1)
                Dim y = Random.Next(Me.Height - 1)
                If Not Tiles(x, y).IsPassable Then
                    Continue Do
                End If
                c += 1
                GameObjects.Add(New Tank(Me, New CellLocation(x, y), Random.Next(4), Random.NextDouble() * 4 + 1))
            Loop
        End Sub

        Public Shared ReadOnly Property DesignInstance As New GameField

        Public ReadOnly Property GameObjects As New ObservableCollection(Of GameObject)

        Public ReadOnly Property Tiles As TerrainTile(,)

        Public ReadOnly Property Player As Player
        Public ReadOnly Property Height As Integer
        Public ReadOnly Property Width As Integer

        Private ReadOnly Property Random As New Random

        Private Function GetTypeForCoords(x As Integer, y As Integer) As TerrainTileType
            If x \ 2 = Width \ 4 Then
                Return TerrainTileType.Pavement
            End If
            If y \ 2 = Height \ 4 Then
                Return TerrainTileType.Water
            End If

            If x * y = 0 Then
                Return TerrainTileType.StoneWall
            End If
            If (x + 1 - Width) * (y + 1 - Height) = 0 Then
                Return TerrainTileType.WoodWall
            End If

            'if Random.NextDouble()<0.1 then return TerrainTileType.WoodWall
            If Random.NextDouble() < 0.3 Then
                Return TerrainTileType.Forest
            End If
            Return TerrainTileType.Plain
        End Function
    End Class
End Namespace
