Imports System
Imports System.IO
Imports System.Threading

Public Class MainWindow

    Private extractor As WulfsExtractor = New WulfsExtractor()
    Private WithEvents updateUITimer As New System.Windows.Forms.Timer()

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateUITimer.Interval = 200
        updateUITimer.Start()

        txtDataFolder.Text = My.Settings.StartFolder
        If txtDataFolder.Text <> "" Then
            reloadFileTree()
        End If
    End Sub

    Private Sub Log(ByVal txt As String)
        extractor.outputList.Add(txt & vbNewLine)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim folderDlg As New FolderBrowserDialog()

        folderDlg.ShowNewFolderButton = False
        folderDlg.SelectedPath = My.Settings.StartFolder

        If folderDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Settings.StartFolder = folderDlg.SelectedPath
            My.Settings.Save()
            txtDataFolder.Text = folderDlg.SelectedPath
            reloadFileTree()
        End If

    End Sub

    Private Function getFileName(ByVal path As String)
        Dim slash As Integer = InStrRev(path, "\")
        If slash < 0 Then
            Return path
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - slash)
    End Function

    Private Function getFileExt(ByVal path As String)
        Dim dot As Integer = InStrRev(path, ".")
        If dot < 0 Then
            Return path
        End If
        Return Microsoft.VisualBasic.Right(path, path.Length - dot)
    End Function

    Private Sub reloadFileTree()
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
        treeFiles.EndUpdate()
    End Sub

    Private Sub addFile(ByVal file As String, ByVal nodes As TreeNodeCollection)
        Dim node As TreeNode = nodes.Add(getFileName(file))
        node.Tag = file
        reloadFileNode(node)
    End Sub

    Private Sub reloadFileNode(ByVal node As TreeNode)
        node.Nodes.Clear()
        If Directory.Exists(node.Tag + ".extract") Then
            loadFolder(node.Tag + ".extract", node.Nodes)
        End If
    End Sub

    Private Sub loadFolder(ByVal folder As String, ByRef nodes As TreeNodeCollection)
        For Each d In Directory.EnumerateDirectories(folder)
            If Not d.EndsWith(".extract") Then
                Dim node As TreeNode = nodes.Add(getFileName(d))
                loadFolder(d, node.Nodes)
            End If
        Next
        For Each file In Directory.EnumerateFiles(folder)
            Dim filename As String = getFileName(file)
            If filename <> "filelist.txt" And Not filename.EndsWith(".bak") And Not filename.EndsWith(".info.txt") Then
                addFile(file, nodes)
            End If
        Next
    End Sub

    Private Sub updateUI() Handles updateUITimer.Tick
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
                Dim thread As Thread = New Thread(AddressOf extract)
                Dim data As ExtractData = New ExtractData()
                data.filename = selNode.Tag
                data.node = selNode
                thread.Start(Data)
            End If
        End If
    End Sub

    Private Delegate Sub reloadFileNodeDelegate(ByVal node As TreeNode)

    Private Sub extract(ByVal data As ExtractData)
        extractor.extract(data.filename)
        Dim reloadDelegate As New reloadFileNodeDelegate(AddressOf reloadFileNode)
        Invoke(reloadDelegate, data.node)
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
End Class
