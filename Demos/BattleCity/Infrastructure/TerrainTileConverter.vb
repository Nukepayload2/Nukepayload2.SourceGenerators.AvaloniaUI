Imports System.Globalization
Imports BattleCity.Model
Imports Avalonia.Data.Converters
Imports Avalonia.Media.Imaging
Imports Avalonia.Platform

Namespace Infrastructure

    Public Class TerrainTileConverter
        Implements IValueConverter

        Private Shared _cache As Dictionary(Of TerrainTileType, Bitmap)
        Public Shared ReadOnly Property Instance As New TerrainTileConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return GetCache()(DirectCast(value, TerrainTileType))
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Private Shared Function GetCache() As Dictionary(Of TerrainTileType, Bitmap)
            _cache = If(_cache, System.Enum.GetValues(GetType(TerrainTileType)).
                OfType(Of TerrainTileType)().
                ToDictionary(Function(t) t, Function(t) New Bitmap(
                    AssetLoader.Open(New Uri($"avares://BattleCity/Assets/{t}.png")))))
            Return _cache
        End Function
    End Class
End Namespace
