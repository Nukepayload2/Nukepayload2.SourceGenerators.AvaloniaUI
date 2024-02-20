Imports Avalonia
Imports Avalonia.Controls
Imports Avalonia.Controls.Metadata
Imports Avalonia.Controls.Primitives
Imports Avalonia.Controls.Shapes
Imports Avalonia.Data

Namespace Controls
    ''' <summary>
    ''' This control lets the user set a rating, to show their degree of satisfaction 
    ''' </summary>
    <TemplatePart("PART_StarsPresenter", GetType(ItemsControl))> ' This Attribute specifies that "PART_StarsPresenter" is a control, which must be present in the Control-Template
    Public Class RatingControl
        Inherits TemplatedControl

        ' this field holds a reference to the part in the control template that holds the rating stars
        Private _starsPresenter As ItemsControl

        Public Sub New()
            ' When a new instance of the control is created, we need to update the rating stars visible
            UpdateStars()
        End Sub

        ''' <summary>
        ''' Defines the <see cref="NumberOfStars"/> property.
        ''' </summary>
        ''' <remarks>
        ''' We define this property as a styled property, so you can set this property inside your style of the rating control. 
        ''' </remarks>
        Public Shared ReadOnly NumberOfStarsProperty As StyledProperty(Of Integer) =
            AvaloniaProperty.Register(Of RatingControl, Integer)(
            NameOf(NumberOfStars), defaultValue:=5,
            coerce:=AddressOf CoerceNumberOfStars) ' Ensures that we always have a valid number of stars

        ''' <summary>
        ''' Gets or sets the number of available stars
        ''' </summary>
        Public Property NumberOfStars() As Integer
            Get
                Return GetValue(NumberOfStarsProperty)
            End Get
            Set(value As Integer)
                SetValue(NumberOfStarsProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' This function will coerce the <see cref="NumberOfStars"/> property. The minimum allowed number is 1
        ''' </summary>
        ''' <param name="sender">the RatingControl-instance calling this method</param>
        ''' <param name="value">the value to coerce</param>
        ''' <returns>The coerced value</returns>
        Private Shared Function CoerceNumberOfStars(sender As AvaloniaObject, value As Integer) As Integer
            ' the value should not be lower than 1.
            ' Hint: You can also return Math.Max(1, value)
            If value < 1 Then
                Return 1
            Else
                Return value
            End If
        End Function

        ''' <summary>
        ''' Defines the <see cref="Value"/> property.
        ''' </summary>
        ''' <remarks>
        ''' This property doesn't need to be styled. Therefore we can use a direct property, which improves performance and 
        ''' allows us to add validation support.
        ''' </remarks>
        Public Shared ReadOnly ValueProperty As DirectProperty(Of RatingControl, Integer) = AvaloniaProperty.RegisterDirect(Of RatingControl, Integer)(NameOf(Value), Function(o) o.Value, Sub(o, v) o.Value = v, defaultBindingMode:=BindingMode.TwoWay, enableDataValidation:=True) ' Enables DataValidation

        ' For direct properties we need to have a backing field
        Private _value As Integer

        ''' <summary>
        ''' Gets or sets the current value
        ''' </summary>
        Public Property Value() As Integer
            Get
                Return _value
            End Get
            Set(value As Integer)
                SetAndRaise(ValueProperty, _value, value)
            End Set
        End Property


        ''' <summary>
        ''' Defines the <see cref="Stars"/> property.
        ''' </summary>
        ''' <remarks>
        ''' ´This property holds a read-only array of stars. 
        ''' </remarks>
        Public Shared ReadOnly StarsProperty As DirectProperty(Of RatingControl, IEnumerable(Of Integer)) =
            AvaloniaProperty.RegisterDirect(Of RatingControl, IEnumerable(Of Integer))(
            NameOf(Stars), Function(o) o.Stars) ' The getter. As we don't add a setter, this property is read-only

        ' For read-only properties we need to have a backing field. The default value is [1..5]
        Private _stars As IEnumerable(Of Integer) = Enumerable.Range(1, 5)

        ''' <summary>
        ''' Gets the current collection of visible stars
        ''' </summary>
        Public Property Stars As IEnumerable(Of Integer)
            Get
                Return _stars
            End Get
            ' make sure the setter is private
            Private Set(value As IEnumerable(Of Integer))
                SetAndRaise(StarsProperty, _stars, value)
            End Set
        End Property

        ' called when the number of stars changed
        Private Sub UpdateStars()
            ' Stars is an array from 1 to NumberOfStars
            Stars = Enumerable.Range(1, NumberOfStars)
        End Sub

        ' We override OnPropertyChanged of the base class. That way we can react on property changes
        Protected Overrides Sub OnPropertyChanged(change As AvaloniaPropertyChangedEventArgs)
            MyBase.OnPropertyChanged(change)

            ' if the changed property is the NumberOfStarsProperty, we need to update the stars
            If change.Property = NumberOfStarsProperty Then
                UpdateStars()
            End If
        End Sub

        ' We override what happens when the control template is being applied. 
        ' That way we can for example listen to events of controls which are part of the template
        Protected Overrides Sub OnApplyTemplate(e As TemplateAppliedEventArgs)
            MyBase.OnApplyTemplate(e)

            ' if we had a control template before, we need to unsubscribe any event listeners
            If _starsPresenter IsNot Nothing Then
                RemoveHandler _starsPresenter.PointerReleased, AddressOf StarsPresenter_PointerReleased
            End If

            ' try to find the control with the given name
            _starsPresenter = TryCast(e.NameScope.Find("PART_StarsPresenter"), ItemsControl)

            ' listen to pointer-released events on the stars presenter.
            If _starsPresenter IsNot Nothing Then
                AddHandler _starsPresenter.PointerReleased, AddressOf StarsPresenter_PointerReleased
            End If
        End Sub

        ''' <summary>
        ''' Called to update the validation state for properties for which data validation is
        ''' enabled.
        ''' </summary>
        ''' <param name="property">The property.</param>
        ''' <param name="state">The current data binding state.</param>
        ''' <param name="error">The Exception that was passed</param>
        Protected Overrides Sub UpdateDataValidation([property] As AvaloniaProperty, state As BindingValueType, [error] As Exception)
            MyBase.UpdateDataValidation([property], state, [error])

            If [property] = ValueProperty Then
                DataValidationErrors.SetError(Me, [error])
            End If
        End Sub

        Private Sub StarsPresenter_PointerReleased(sender As Object, e As Avalonia.Input.PointerReleasedEventArgs)
            ' e.Source is the original source of this event. In our case, if the user clicked on a star, the original source is a Path.
            If TypeOf e.Source Is Path Then
                Dim star As Path = CType(e.Source, Path)
                ' The DataContext of the star is one of the numbers we have in the Stars-Collection. 
                ' Let's cast the DataContext to an int. If that cast fails, use "0" as a fallback.
                Value = If(CType(star.DataContext, Integer?), 0)
            End If
        End Sub
    End Class
End Namespace
