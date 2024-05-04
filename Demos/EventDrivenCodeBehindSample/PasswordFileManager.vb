Imports System.IO

Module PasswordFileManager
    Public Const PasswordFilePath = "passwd.utf8.txt"

    Function LoadDistinctTrimPasswords() As IReadOnlySet(Of String)
        If Not File.Exists(PasswordFilePath) Then Return New HashSet(Of String)
        Return Aggregate pw In File.ReadAllLines(PasswordFilePath)
               Where pw.Length > 0
               Select pw.Trim Into ToHashSet
    End Function

End Module
