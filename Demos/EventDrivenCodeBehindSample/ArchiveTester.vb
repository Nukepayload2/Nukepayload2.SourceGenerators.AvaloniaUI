Imports SharpCompress.Archives.SevenZip
Imports System.IO
Imports System.Threading
Imports SharpCompress.Readers

Module ArchiveTester

    Function CollectArchiveFileList(filePath As String) As List(Of FileInfo)
        Dim archiveFiles As New List(Of FileInfo) From {New FileInfo(filePath)}
        If filePath.EndsWith(".001") Then
            Dim filePathNoNumber = filePath.Substring(0, filePath.Length - 3)
            For i = 2 To 999
                Dim nextFile = filePathNoNumber & i.ToString("000")
                If File.Exists(nextFile) Then
                    archiveFiles.Add(New FileInfo(nextFile))
                Else
                    Exit For
                End If
            Next
        End If

        Return archiveFiles
    End Function

    Async Function GuessPasswordAsync(archiveFiles As List(Of FileInfo),
                                      passwords As IReadOnlySet(Of String),
                                      progressCallback As Action,
                                      Optional cancellation As CancellationToken = Nothing) As Task(Of String)
        If HasNoPassword(archiveFiles) Then Return String.Empty
        Dim found As Integer
        Dim result As String = Nothing
        Await Task.WhenAll(
        From password In passwords
        Select Task.Run(
        Sub()
            cancellation.ThrowIfCancellationRequested()
            If Volatile.Read(found) <> 0 Then Return
            TryPassword(archiveFiles, found, result, password)
            progressCallback()
        End Sub, cancellation))
        Return result
    End Function

    Private Sub TryPassword(archiveFiles As List(Of FileInfo), ByRef found As Integer, ByRef result As String, password As String)
        Try
            Dim readerOptions As New ReaderOptions With {
                .Password = password
            }

            Using zipFile = SevenZipArchive.Open(archiveFiles, readerOptions)
                Dim smallestEntry = FindSmallestOrFirstFile(zipFile)

                If smallestEntry IsNot Nothing Then
                    Using strm = smallestEntry.OpenEntryStream
                        Dim crc As New System.IO.Hashing.Crc32
                        crc.Append(strm)
                        Dim hashCode = crc.GetCurrentHashAsUInt32
                        Dim expectedHash = smallestEntry.Crc
                        If CLng(hashCode) = expectedHash Then
                            Volatile.Write(result, password)
                            Volatile.Write(found, 1)
                            Return
                        End If
                    End Using
                Else
                    ' Empty zip file
                    Volatile.Write(result, String.Empty)
                    Volatile.Write(found, 1)
                    Return
                End If
            End Using
        Catch ex As Exception
            ' Failed
        End Try
    End Sub

    Private Function FindSmallestOrFirstFile(zipFile As SevenZipArchive) As SevenZipArchiveEntry
        If zipFile.IsSolid Then
            Return zipFile.Entries.FirstOrDefault(Function(ent) Not ent.IsDirectory AndAlso ent.Size > 0)
        End If
        Return Aggregate ent In zipFile.Entries
               Where Not ent.IsDirectory AndAlso ent.Size > 0
               Into MinBy(ent.Size)
    End Function

    Private Function HasNoPassword(archiveFiles As List(Of FileInfo)) As Boolean
        Dim found = 0
        TryPassword(archiveFiles, found, Nothing, Nothing)
        Return found <> 0
    End Function
End Module
