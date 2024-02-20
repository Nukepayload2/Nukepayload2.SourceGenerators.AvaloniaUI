Namespace Model

    Public MustInherit Class MovingGameObject
        Inherits GameObject

        Private ReadOnly _field As GameField
        Private _cellLocation As CellLocation
        Private _facing As Facing
        Private _targetCellLocation As CellLocation

        Protected Sub New(field As GameField, location As CellLocation, facing As Facing)
            MyBase.New(location.ToPoint())
            _field = field
            Me.Facing = facing
            TargetCellLocation = location
            CellLocation = TargetCellLocation
        End Sub

        Public Overrides ReadOnly Property Layer As Integer
            Get
                Return 1
            End Get
        End Property

        Public Property Facing As Facing
            Get
                Return _facing
            End Get
            Set(value As Facing)
                If value = _facing Then
                    Return
                End If
                _facing = value
                OnPropertyChanged()
            End Set
        End Property

        Public Property CellLocation As CellLocation
            Get
                Return _cellLocation
            End Get
            Private Set(value As CellLocation)
                If value.Equals(_cellLocation) Then
                    Return
                End If
                _cellLocation = value
                OnPropertyChanged()
                OnPropertyChanged(NameOf(IsMoving))
            End Set
        End Property

        Public Property TargetCellLocation As CellLocation
            Get
                Return _targetCellLocation
            End Get
            Private Set(value As CellLocation)
                If value.Equals(_targetCellLocation) Then
                    Return
                End If
                _targetCellLocation = value
                OnPropertyChanged()
                OnPropertyChanged(NameOf(IsMoving))
            End Set
        End Property

        Public ReadOnly Property IsMoving As Boolean
            Get
                Return TargetCellLocation <> CellLocation
            End Get
        End Property

        Protected Overridable ReadOnly Property SpeedFactor() As Double
            Get
                Return 1 / 15
            End Get
        End Property

        Public Function SetTarget(loc As CellLocation) As Boolean
            If IsMoving Then
                'We are the bear rolling from the hill
                Throw New InvalidOperationException("Unable to change direction while moving")
            End If
            If loc = CellLocation Then
                Return True
            End If
            Facing = GetDirection(CellLocation, loc)
            If loc.X < 0 OrElse loc.Y < 0 Then
                Return False
            End If
            If loc.X >= _field.Width OrElse loc.Y >= _field.Height Then
                Return False
            End If
            If Not _field.Tiles(loc.X, loc.Y).IsPassable Then
                Return False
            End If

            If _field.GameObjects.OfType(Of MovingGameObject)().Any(Function(t) t IsNot Me AndAlso (t.CellLocation = loc OrElse t.TargetCellLocation = loc)) Then
                Return False
            End If

            TargetCellLocation = loc
            Return True
        End Function

        Public Function GetTileAtDirection(facing As Facing) As CellLocation
            If facing = Facing.North Then
                Return New CellLocation(CellLocation.X, CellLocation.Y - 1)
            End If
            If facing = Facing.South Then
                Return New CellLocation(CellLocation.X, CellLocation.Y + 1)
            End If
            If facing = Facing.West Then
                Return New CellLocation(CellLocation.X - 1, CellLocation.Y)
            End If
            Return New CellLocation(CellLocation.X + 1, CellLocation.Y)
        End Function

        Public Function SetTarget(facing? As Facing) As Boolean
            Return SetTarget(If(facing.HasValue, GetTileAtDirection(facing.Value), CellLocation))
        End Function

        Private Function GetDirection(current As CellLocation, target As CellLocation) As Facing
            If target.X < current.X Then
                Return Facing.West
            End If
            If target.X > current.X Then
                Return Facing.East
            End If
            If target.Y < current.Y Then
                Return Facing.North
            End If
            Return Facing.South
        End Function

        Public Sub SetLocation(loc As CellLocation)
            CellLocation = loc
            Location = loc.ToPoint()
        End Sub

        Public Sub MoveToTarget()
            If TargetCellLocation = CellLocation Then
                Return
            End If
            Dim speed = GameField.CellSize * (_field.Tiles(CellLocation.X, CellLocation.Y).Speed + _field.Tiles(TargetCellLocation.X, TargetCellLocation.Y).Speed) / 2 * SpeedFactor
            Dim pos = Location
            Dim direction = GetDirection(CellLocation, TargetCellLocation)
            If direction = Facing.North Then
                pos = pos.WithY(pos.Y - speed)
                Location = pos
                If pos.Y / GameField.CellSize <= TargetCellLocation.Y Then
                    SetLocation(TargetCellLocation)
                End If
            ElseIf direction = Facing.South Then
                pos = pos.WithY(pos.Y + speed)
                Location = pos
                If pos.Y / GameField.CellSize >= TargetCellLocation.Y Then
                    SetLocation(TargetCellLocation)
                End If
            ElseIf direction = Facing.West Then
                pos = pos.WithX(pos.X - speed)
                Location = pos
                If pos.X / GameField.CellSize <= TargetCellLocation.X Then
                    SetLocation(TargetCellLocation)
                End If
            ElseIf direction = Facing.East Then
                pos = pos.WithX(pos.X + speed)
                Location = pos
                If pos.X / GameField.CellSize >= TargetCellLocation.X Then
                    SetLocation(TargetCellLocation)
                End If
            End If
        End Sub
    End Class
End Namespace
