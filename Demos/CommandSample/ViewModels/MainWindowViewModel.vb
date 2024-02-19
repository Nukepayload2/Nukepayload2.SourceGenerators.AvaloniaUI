Imports ReactiveUI
Imports System.Collections.ObjectModel
Imports System.Windows.Input

Namespace ViewModels
    Public Class MainWindowViewModel
        Inherits ViewModelBase

        ' We use the constructor to initialize the Commands.
        Public Sub New()
            ' We initiate our Commands using ReactiveCommand.Create...
            ' see: https://www.reactiveui.net/docs/handbook/commands/

            ' Init OpenThePodBayDoorsDirectCommand    
            OpenThePodBayDoorsDirectCommand = ReactiveCommand.Create(AddressOf OpenThePodBayDoors)

            ' Init OpenThePodBayDoorsFellowRobotCommand
            ' The IObservable<bool> is needed to enable or disable the command depending on valid parameters
            ' The Observable listens to RobotName and will enable the Command if the name is not empty.
            Dim canExecuteFellowRobotCommand As IObservable(Of Boolean) = WhenAnyValue(Function(vm) vm.RobotName, Function(name) Not String.IsNullOrEmpty(name))

            OpenThePodBayDoorsFellowRobotCommand = ReactiveCommand.Create(Of String)(Sub(name) OpenThePodBayDoorsFellowRobot(name), canExecuteFellowRobotCommand)

            ' Init OpenThePodBayDoorsAsyncCommand
            OpenThePodBayDoorsAsyncCommand = ReactiveCommand.CreateFromTask(AddressOf OpenThePodBayDoorsAsync)
        End Sub

        ''' <summary>
        ''' This command will ask HAL-9000 to open the pod bay doors
        ''' </summary>
        ''' // Note: We use the interface ICommand here because this makes things more flexible.
        Public ReadOnly Property OpenThePodBayDoorsDirectCommand As ICommand

        ' The method that will be executed when the command is invoked
        Private Sub OpenThePodBayDoors()
            ConversationLog.Clear()
            AddToConvo("I'm sorry, Dave, I'm afraid I can't do that.")
        End Sub

        ''' <summary>
        ''' This command will ask HAL to open the pod bay doors, but this time we
        ''' check that the command is issued by a fellow robot (really any non-null name)
        ''' </summary>
        Public ReadOnly Property OpenThePodBayDoorsFellowRobotCommand() As ICommand

        ' The method that will be executed when the command is invoked
        Private Sub OpenThePodBayDoorsFellowRobot(robotName As String)
            ConversationLog.Clear()
            AddToConvo($"Hello {robotName}, the Pod Bay is open :-)")
        End Sub

        ' Backing field for RobotName
        Private _RobotName As String

        ''' <summary>
        ''' The name of a robot. If the name is null or empty, there is no other robot present.
        ''' </summary>
        Public Property RobotName As String
            Get
                Return _RobotName
            End Get
            Set(value As String)
                RaiseAndSetIfChanged(_RobotName, value)
            End Set
        End Property

        ''' <summary>
        ''' This command will start an async task for the multi-step Pod bay opening sequence
        ''' </summary>
        Public ReadOnly Property OpenThePodBayDoorsAsyncCommand() As ICommand

        ' This method is an async Task because opening the pod bay doors can take long time.
        ' We don't want our UI to become unresponsive.
        Private Async Function OpenThePodBayDoorsAsync() As Task
            ConversationLog.Clear()
            AddToConvo("Preparing to open the Pod Bay...")
            ' wait a second
            Await Task.Delay(1000)

            AddToConvo("Depressurizing Airlock...")
            ' wait 2 seconds
            Await Task.Delay(2000)

            AddToConvo("Retracting blast doors...")
            ' wait 2 more seconds
            Await Task.Delay(2000)

            AddToConvo("Pod Bay is open to space!")
        End Function

        ' Conversation Log (our output)

        ''' <summary>
        '''  This collection will store what the computer said
        ''' </summary>
        Public ReadOnly Property ConversationLog As New ObservableCollection(Of String)

        ' Just a helper to add content to ConversationLog
        Private Sub AddToConvo(content As String)
            ConversationLog.Add(content)
        End Sub
    End Class
End Namespace
