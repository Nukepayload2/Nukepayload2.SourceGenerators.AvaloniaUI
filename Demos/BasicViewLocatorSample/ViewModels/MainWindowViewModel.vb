Imports DynamicData
Imports ReactiveUI
Imports System.Windows.Input

Namespace ViewModels
	Public Class MainWindowViewModel
		Inherits ViewModelBase

		Public Sub New()
			' Set current page to first on start up
			_CurrentPage = Pages(0)

			' Create Observables which will activate to deactivate our commands based on CurrentPage state
			Dim canNavNext = WhenAnyValue(Function(x) x.CurrentPage.CanNavigateNext)
			Dim canNavPrev = WhenAnyValue(Function(x) x.CurrentPage.CanNavigatePrevious)

			NavigateNextCommand = ReactiveCommand.Create(AddressOf NavigateNext, canNavNext)
			NavigatePreviousCommand = ReactiveCommand.Create(AddressOf NavigatePrevious, canNavPrev)
		End Sub

		' A read.only array of possible pages
		Private ReadOnly Pages As PageViewModelBase() = {
			New FirstPageViewModel,
			New SecondPageViewModel,
			New ThirdPageViewModel
		}

		' The default is the first page
		Private _CurrentPage As PageViewModelBase

		''' <summary>
		''' Gets the current page. The property is read-only
		''' </summary>
		Public Property CurrentPage As PageViewModelBase
			Get
				Return _CurrentPage
			End Get
			Private Set(value As PageViewModelBase)
				RaiseAndSetIfChanged(_CurrentPage, value)
			End Set
		End Property

		''' <summary>
		''' Gets a command that navigates to the next page
		''' </summary>
		Public ReadOnly Property NavigateNextCommand As ICommand

		Private Sub NavigateNext()
			' get the current index and add 1
			Dim index = Pages.IndexOf(CurrentPage) + 1

			'  /!\ Be aware that we have no check if the index is valid. You may want to add it on your own. /!\
			CurrentPage = Pages(index)
		End Sub

		''' <summary>
		''' Gets a command that navigates to the previous page
		''' </summary>
		Public ReadOnly Property NavigatePreviousCommand As ICommand

		Private Sub NavigatePrevious()
			' get the current index and subtract 1
			Dim index = Pages.IndexOf(CurrentPage) - 1

			'  /!\ Be aware that we have no check if the index is valid. You may want to add it on your own. /!\
			CurrentPage = Pages(index)
		End Sub
	End Class
End Namespace
