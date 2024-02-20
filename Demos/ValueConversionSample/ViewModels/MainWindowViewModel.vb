Imports ReactiveUI

Namespace ViewModels
    Public Class MainWindowViewModel
        Inherits ViewModelBase

        ' The initial value is 2. 
        Private _Number1? As Decimal = 2

        ''' <summary>
        ''' This is our Number 1
        ''' </summary>
        Public Property Number1 As Decimal?
            Get
                Return _Number1
            End Get
            Set(value? As Decimal)
                RaiseAndSetIfChanged(_Number1, value)
            End Set
        End Property


        ' The initial value is 3.
        Private _Number2? As Decimal = 3

        ''' <summary>
        ''' This is our Number 2
        ''' </summary>
        Public Property Number2 As Decimal?
            Get
                Return _Number2
            End Get
            Set(value? As Decimal)
                RaiseAndSetIfChanged(_Number2, value)
            End Set
        End Property


        ' The initial value is "+" (Add).
        Private _Operator As String = "+"

        ''' <summary>
        ''' Gets or sets the operator to use. The initial value is "+"
        ''' </summary>
        Public Property [Operator] As String
            Get
                Return _Operator
            End Get
            Set(value As String)
                RaiseAndSetIfChanged(_Operator, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets a collection of available operators
        ''' </summary>
        Public ReadOnly Property AvailableMathOperators As String() = {"+", "-", "*", "/"}
    End Class
End Namespace
