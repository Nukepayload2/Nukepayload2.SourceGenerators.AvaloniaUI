Imports Avalonia

Namespace Model

    Public Enum TerrainTileType
        Plain 'passable, shoot-thru
        WoodWall 'impassable, takes 1 shot to bring down
        StoneWall 'impassable, indestructible
        Water 'impassable, shoot-thru
        Pavement 'passable, 2x speed
        Forest 'passable at half speed, shoot-thru
    End Enum

    Public Class TerrainTile
        Inherits GameObject

        Private Shared ReadOnly Speeds As New Dictionary(Of TerrainTileType, Double)() From {
            {TerrainTileType.Plain, 1},
            {TerrainTileType.WoodWall, 0},
            {TerrainTileType.StoneWall, 0},
            {TerrainTileType.Water, 0},
            {TerrainTileType.Pavement, 2},
            {TerrainTileType.Forest, 0.5}
        }

        Private Shared ReadOnly ShootThrus As New Dictionary(Of TerrainTileType, Boolean)() From {
            {TerrainTileType.Plain, True},
            {TerrainTileType.WoodWall, False},
            {TerrainTileType.StoneWall, False},
            {TerrainTileType.Water, True},
            {TerrainTileType.Pavement, True},
            {TerrainTileType.Forest, True}
        }

        Public Sub New(location As Point, type As TerrainTileType)
            MyBase.New(location)
            Me.Type = type
        End Sub


        Public ReadOnly Property Speed As Double
            Get
                Return Speeds(Type)
            End Get
        End Property
        Public ReadOnly Property ShootThru As Boolean
            Get
                Return ShootThrus(Type)
            End Get
        End Property
        Public ReadOnly Property IsPassable As Boolean
            Get
                Return Speed > 0.1
            End Get
        End Property
        Public Property Type As TerrainTileType
    End Class
End Namespace
