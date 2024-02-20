Imports Avalonia

Namespace Model

    <System.Runtime.CompilerServices.IsReadOnly>
    Public Structure CellLocation
        Implements IEquatable(Of CellLocation)

        Public ReadOnly X As Integer, Y As Integer

        Public Sub New(x As Integer, y As Integer)
            Me.X = x
            Me.Y = y
        End Sub

        Public Function ToPoint() As Point
            Return New Point(GameField.CellSize * X, GameField.CellSize * Y)
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If Not (TypeOf obj Is CellLocation) Then
                Return False
            End If

            Return Equals(DirectCast(obj, CellLocation))
        End Function

        Public Overloads Function Equals(other As CellLocation) As Boolean Implements IEquatable(Of CellLocation).Equals
            Return X = other.X AndAlso
                   Y = other.Y
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return HashCode.Combine(X, Y)
        End Function

        Public Shared Operator =(left As CellLocation, right As CellLocation) As Boolean
            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(left As CellLocation, right As CellLocation) As Boolean
            Return Not left = right
        End Operator
    End Structure
End Namespace
