Imports System.IO
Imports System.Threading
Imports Avalonia.Controls
Imports Avalonia.Platform.Storage
Imports Avalonia.Threading

Partial Class MainWindow
    Inherits Window

    Private _testCancellationTokenSource As CancellationTokenSource
    Private _lastPassword As String
    Private _archTestProgressValue As Integer

    Private WithEvents ArchProgTimer As New DispatcherTimer With {
        .Interval = TimeSpan.FromMilliseconds(100)
    }

    Private Async Sub BtnOpenArchive_Click() Handles BtnOpenArchive.Click
        BtnOpenArchive.IsEnabled = False
        BtnCancelTest.IsEnabled = True
        BtnCopyPassword.IsEnabled = False
        BtnDictDistinct.IsEnabled = False
        BtnDictImport.IsEnabled = False
        PrgGuess.IsVisible = False
        TblUseStatus.Text = "Processing"
        Try
            Dim pickedFiles = Await StorageProvider.OpenFilePickerAsync(
            New FilePickerOpenOptions With {
                .FileTypeFilter = {
                    New FilePickerFileType("7z file") With {
                        .Patterns = {"*.7z", "*.7z.001"}
                    }
                }
            })
            If pickedFiles.Count < 1 Then
                TblUseStatus.Text = "Canceled"
                Return
            End If
            Dim pickedFile = pickedFiles(0)
            Dim archFiles = CollectArchiveFileList(pickedFile.Path.LocalPath)
            Dim passwords = LoadDistinctTrimPasswords()
            Dim tokenSrc As New CancellationTokenSource
            _testCancellationTokenSource = tokenSrc
            PrgGuess.IsVisible = True
            PrgGuess.Maximum = passwords.Count
            PrgGuess.Value = 0
            ArchProgTimer.IsEnabled = True
            _archTestProgressValue = 0
            Dim password = Await GuessPasswordAsync(archFiles, passwords,
                Sub() _archTestProgressValue += 1, tokenSrc.Token)
            If password Is Nothing Then
                TblUseStatus.Text = "No matching password"
            ElseIf password.Length = 0 Then
                TblUseStatus.Text = "This file doesn't have password"
            Else
                TblUseStatus.Text = "Password: " & password
                _lastPassword = password
                BtnCopyPassword.IsEnabled = True
            End If
            ArchProgTimer.IsEnabled = False
            PrgGuess.Value = PrgGuess.Maximum
        Catch ex As TaskCanceledException
            TblUseStatus.Text = "Canceled"
            PrgGuess.IsVisible = False
        Catch ex As Exception
            PrgGuess.IsVisible = False
            TblUseStatus.Text = ex.GetType.FullName & ": " & ex.Message
        Finally
            BtnOpenArchive.IsEnabled = True
            BtnCancelTest.IsEnabled = False
            BtnDictDistinct.IsEnabled = True
            BtnDictImport.IsEnabled = True
        End Try
    End Sub

    Private Sub ArchProgTimer_Tick() Handles ArchProgTimer.Tick
        PrgGuess.Value = _archTestProgressValue
    End Sub

    Private Sub BtnCancelTest_Click() Handles BtnCancelTest.Click
        _testCancellationTokenSource?.Cancel()
    End Sub

    Private Async Sub BtnCopyPassword_Click() Handles BtnCopyPassword.Click
        Try
            Await Clipboard.SetTextAsync(_lastPassword)
            TblUseStatus.Text = $"Copied {_lastPassword} to clipboard"
        Catch ex As Exception
            TblUseStatus.Text = "Failed to copy: " & ex.Message
        End Try
    End Sub

    Private Sub BtnDictDistinct_Click() Handles BtnDictDistinct.Click
        Try
            File.WriteAllLines(PasswordFilePath, LoadDistinctTrimPasswords.Order)
            TblMaintainStatus.Text = "Optimization complete"
        Catch ex As Exception
            TblMaintainStatus.Text = "Optimization failed: " & ex.Message
        End Try
    End Sub

    Private Async Sub BtnDictImport_Click() Handles BtnDictImport.Click
        IsEnabled = False
        Try
            Dim newText = Await Clipboard.GetTextAsync
            If newText = Nothing Then Return
            Dim passwds = newText.Split(ControlChars.Lf, StringSplitOptions.TrimEntries)
            Dim passMerged = LoadDistinctTrimPasswords().ToHashSet
            Dim oldCount = passMerged.Count
            For Each pw In passwds
                passMerged.Add(pw)
            Next
            Dim newCount = passMerged.Count
            Dim countDiff = newCount - oldCount
            If countDiff > 0 Then
                TblMaintainStatus.Text = $"Imported {countDiff} items."
                File.WriteAllLines(PasswordFilePath, passMerged)
            Else
                TblMaintainStatus.Text = "No new items were imported."
            End If
        Catch ex As Exception
            TblMaintainStatus.Text = "Failed to import: " & ex.Message
        Finally
            IsEnabled = True
        End Try
    End Sub

End Class
