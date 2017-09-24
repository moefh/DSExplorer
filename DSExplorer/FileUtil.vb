Imports System.IO

Public Class FileUtil

    Public Shared Function GetFileType(ByVal filename As String) As String
        Try
            Using f = File.OpenRead(filename)
                Dim data(4) As Byte
                Dim n as Integer
                Dim type As String

                n = f.Read(data, 0, 4)
                type = ""
                For i = 0 To n-1
                    If data(i) > 0 Then
                        type += Convert.ToChar(data(i))
                    Else
                        Exit For
                    End If
                Next
                Return type
            End Using
        Catch e As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function IsFileExtractable(ByVal filename As string) As Boolean
        Dim type As String = GetFileType(filename)
        Select Case type
            Case "BHD5", "BHF3", "BND3", "TPF", "DCX"
                Return True
        End Select
        Return False
    End Function

    Public Shared Function GetPathFileName(ByVal path As String) As String
        Dim slash As Integer = InStrRev(path, "\")
        If slash < 0 Then
            Return path
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - slash)
    End Function

    Public Shared Function GetPathFileExt(ByVal path As String) As String
        Dim dot As Integer = InStrRev(path, ".")
        If dot < 0 Then
            Return ""
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - dot)
    End Function

End Class
