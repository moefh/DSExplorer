Imports System.IO

Public Class FileTreeManager

    Dim treeFiles As TreeView

    Public Sub New(ByVal treeFiles As TreeView)
        Me.treeFiles = treeFiles
    End Sub

    Public Sub Load(ByVal dataFolder As String)
        treeFiles.BeginUpdate()
        treeFiles.Nodes.Clear()
        Try
            For Each file In Directory.EnumerateFiles(dataFolder)
                If FileUtil.GetPathFileExt(file).ToLower = "bhd5" Then
                    addFile(file, treeFiles.Nodes)
                End If
            Next
        Catch e As Exception
            MsgBox("Error loading files: " & e.Message)
        End Try
        treeFiles.Sort()
        treeFiles.EndUpdate()
    End Sub

    Private Sub AddFile(ByVal file As String, ByVal nodes As TreeNodeCollection)
        If file.EndsWith("\filelist.txt") OrElse file.EndsWith(".bak") OrElse file.EndsWith(".info.txt") Then
            Exit Sub
        End If

        Dim node As TreeNode = nodes.Add(FileUtil.GetPathFileName(file))
        node.Tag = file
        If Directory.Exists(node.Tag & ".extract") Then
            loadFolder(node.Tag & ".extract", node.Nodes)
        End If
    End Sub

    Private Sub LoadFolder(ByVal folder As String, ByRef nodes As TreeNodeCollection)
        For Each d In Directory.EnumerateDirectories(folder)
            If Not d.EndsWith(".extract") Then
                Dim node As TreeNode = nodes.Add(FileUtil.GetPathFileName(d))
                LoadFolder(d, node.Nodes)
            End If
        Next
        For Each file In Directory.EnumerateFiles(folder)
            Dim filename As String = FileUtil.GetPathFileName(file)
            AddFile(file, nodes)
        Next
    End Sub

    Public Sub UpdateFileNode(ByVal node As TreeNode)
        If node Is Nothing OrElse node.Tag Is Nothing Then
            Exit Sub
        End If

        Dim topNode = treeFiles.TopNode
        treeFiles.BeginUpdate()
        node.Nodes.Clear()
        If Directory.Exists(node.Tag & ".extract") Then
            loadFolder(node.Tag & ".extract", node.Nodes)
        Else
            Dim dirname As String = Microsoft.VisualBasic.Left(node.Tag, InStrRev(node.Tag, "\"))
            UpdateNodeChildren(node.Parent, dirname)
        End If
        treeFiles.EndUpdate()
        treeFiles.TopNode = topNode
    End Sub

    Private Sub UpdateNodeChildren(ByVal node As TreeNode, dirname As String)
        Dim fileList As New HashSet(Of String)(Directory.EnumerateFiles(dirname))
        For Each child In node.Nodes
            If child.Tag IsNot Nothing AndAlso fileList.Contains(child.Tag) Then
                fileList.Remove(child.Tag)
            End If
        Next

        For Each file In fileList
            AddFile(file, node.Nodes)
        Next
        treeFiles.Sort()
    End Sub

End Class
