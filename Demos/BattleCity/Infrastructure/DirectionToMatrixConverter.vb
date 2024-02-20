Imports System.Globalization
Imports BattleCity.Model
Imports Avalonia.Data.Converters
Imports Avalonia.Media
Imports Avalonia

Namespace Infrastructure

    Friend Class DirectionToMatrixConverter
        Implements IValueConverter

        Public Shared ReadOnly Property Instance As New DirectionToMatrixConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim direction = DirectCast(value, Facing)
            Dim transformMatrix = Matrix.Identity
            If direction = Facing.South Then
                transformMatrix = Matrix.CreateScale(1, -1)
            End If
            If direction = Facing.East Then
                transformMatrix = Matrix.CreateRotation(1.5708)
            End If
            If direction = Facing.West Then
                transformMatrix = Matrix.CreateRotation(1.5708) * Matrix.CreateScale(-1, 1)
            End If
            Return New MatrixTransform(transformMatrix)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
