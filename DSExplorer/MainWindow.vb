Imports System
Imports System.IO
Imports System.Threading

Public Class MainWindow

    Private extractor As WulfsExtractor = New WulfsExtractor()
    Private WithEvents updateUITimer As New System.Windows.Forms.Timer()

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateUITimer.Interval = 200
        updateUITimer.Start()

        treeFiles.TreeViewNodeSorter = New TreeNodeSorter()
        txtDataFolder.Text = My.Settings.StartFolder
        If txtDataFolder.Text <> "" Then
            reloadFileTree()
        End If
    End Sub

    Private Sub Log(ByVal txt As String)
        extractor.outputList.Add(txt & vbNewLine)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim folderDlg As New FolderBrowserDialog With {
            .ShowNewFolderButton = False,
            .SelectedPath = My.Settings.StartFolder
        }

        If folderDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Settings.StartFolder = folderDlg.SelectedPath
            My.Settings.Save()
            txtDataFolder.Text = folderDlg.SelectedPath
            reloadFileTree()
        End If

    End Sub

    Private Function GetFileName(ByVal path As String)
        Dim slash As Integer = InStrRev(path, "\")
        If slash < 0 Then
            Return path
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - slash)
    End Function

    Private Function GetFileExt(ByVal path As String)
        Dim dot As Integer = InStrRev(path, ".")
        If dot < 0 Then
            Return path
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - dot)
    End Function

    Private Sub ReloadFileTree()
        treeFiles.BeginUpdate()
        treeFiles.Nodes.Clear()
        Try
            For Each file In Directory.EnumerateFiles(txtDataFolder.Text)
                If getFileExt(file).ToLower = "bhd5" Then
                    addFile(file, treeFiles.Nodes)
                End If
            Next
        Catch e As Exception
            MsgBox("Error loading files: " + e.Message)
        End Try
        treeFiles.Sort()
        treeFiles.EndUpdate()
    End Sub

    Private Sub AddFile(ByVal file As String, ByVal nodes As TreeNodeCollection)
        If file.EndsWith("\filelist.txt") OrElse file.EndsWith(".bak") OrElse file.EndsWith(".info.txt") Then
            Exit Sub
        End If

        Dim node As TreeNode = nodes.Add(getFileName(file))
        node.Tag = file
        If Directory.Exists(node.Tag + ".extract") Then
            loadFolder(node.Tag + ".extract", node.Nodes)
        End If
    End Sub

    Private Sub LoadFolder(ByVal folder As String, ByRef nodes As TreeNodeCollection)
        For Each d In Directory.EnumerateDirectories(folder)
            If Not d.EndsWith(".extract") Then
                Dim node As TreeNode = nodes.Add(getFileName(d))
                LoadFolder(d, node.Nodes)
            End If
        Next
        For Each file In Directory.EnumerateFiles(folder)
            Dim filename As String = getFileName(file)
            AddFile(file, nodes)
        Next
    End Sub

    Private Sub UpdateFileNode(ByVal node As TreeNode)
        If node Is Nothing OrElse node.Tag Is Nothing Then
            Exit Sub
        End If

        Dim topNode = treeFiles.TopNode
        treeFiles.BeginUpdate()
        node.Nodes.Clear()
        If Directory.Exists(node.Tag + ".extract") Then
            loadFolder(node.Tag + ".extract", node.Nodes)
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

    Private Sub UpdateUI() Handles updateUITimer.Tick
        If extractor.isWorking Then
            btnSelect.Enabled = False
            menuFiles.Enabled = False
        Else
            btnSelect.Enabled = True
            menuFiles.Enabled = True
        End If

        SyncLock extractor.outputLock
            While extractor.outputList.Count > 0
                txtInfo.AppendText(extractor.outputList(0))
                extractor.outputList.RemoveAt(0)
            End While
        End SyncLock


        If txtInfo.Lines.Count > 10000 Then
            Dim newList As List(Of String) = txtInfo.Lines.ToList
            While newList.Count > 10000
                newList.RemoveAt(0)
            End While
            txtInfo.Lines = newList.ToArray
        End If
    End Sub

    Private Sub treeFiles_MouseUp(sender As Object, e As MouseEventArgs) Handles treeFiles.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim p As Point = New Point(e.X, e.Y)
            Dim node = treeFiles.GetNodeAt(p)
            If node IsNot Nothing Then
                treeFiles.SelectedNode = node
                menuFiles.Show(treeFiles, p)
            End If
        End If
    End Sub

    Private Structure ExtractData
        Dim filename As String
        Dim node As TreeNode
    End Structure


    Private Sub miExtract_Click(sender As Object, e As EventArgs) Handles miExtract.Click
        If extractor.isWorking() Then
            Return
        End If

        Dim selNode As TreeNode = treeFiles.SelectedNode
        If selNode IsNot Nothing Then
            If selNode.Tag Is Nothing Then
                MsgBox("The selected item is not extractable.")
            Else
                Dim filename As String = selNode.Tag
                Dim thread As Thread = New Thread(Sub()
                    extractor.extract(filename)
                    Invoke(Sub()
                        updateFileNode(selNode)
                    End Sub)
                End Sub)
                thread.Start()
            End If
        End If
    End Sub

    Private Sub miReload_Click(sender As Object, e As EventArgs) Handles miReload.Click
        If extractor.isWorking() Then
            Return
        End If

        reloadFileTree()
    End Sub

    Private Sub miExpandAll_Click(sender As Object, e As EventArgs) Handles miExpandAll.Click
        treeFiles.ExpandAll()
    End Sub

    Private Sub treeFiles_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeFiles.AfterSelect
        updateNodeDisplay(treeFiles.SelectedNode)
    End Sub

    Private Sub UpdateNodeDisplay(node as TreeNode)
        If node Is Nothing OrElse node.Tag Is Nothing Then
            Exit Sub
        End If

        Dim filename As String = node.Tag
        Try
            'hexDump(File.ReadAllBytes(filename))
            HexViewer.Data = File.ReadAllBytes(filename)
        Catch e As Exception
            HexViewer.Text = "Error reading file '" & filename & "': " & e.Message
            HexViewer.Data = Nothing
        End Try
    End Sub

End Class
