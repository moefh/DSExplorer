Imports System
Imports System.IO
Imports System.Threading

Public Class MainWindow

    Private WithEvents updateUITimer As New System.Windows.Forms.Timer()
    Private bndBuild As WulfsBNDBuild = New WulfsBNDBuild()
    Private tree As FileTreeManager

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateUITimer.Interval = 200
        updateUITimer.Start()

        tree = New FileTreeManager(treeFiles)
        treeFiles.TreeViewNodeSorter = New TreeNodeSorter()

        ' load settings
        If My.Settings.SavedSettings Then
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = My.Settings.MainWindowPos
            Me.Size = My.Settings.MainWindowSize
        End If
        If My.Settings.MainWindowMax Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
        txtDataFolder.Text = My.Settings.StartFolder
        If txtDataFolder.Text <> "" Then
            tree.Load(txtDataFolder.Text)
        End If
    End Sub

    Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.SavedSettings = True
            My.Settings.MainWindowPos = Me.Location
            My.Settings.MainWindowSize = Me.Size
            My.Settings.MainWindowMax = False
        Else If Me.WindowState = FormWindowState.Maximized Then
            My.Settings.MainWindowMax = True
        End If
    End Sub

    Private Sub Log(ByVal txt As String)
        bndBuild.outputList.Add(txt & vbNewLine)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim folderDlg As New FolderBrowserDialog With {
            .ShowNewFolderButton = False,
            .SelectedPath = My.Settings.StartFolder,
            .Description = "Select the Dark Souls DATA folder:"
        }

        If folderDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Settings.StartFolder = folderDlg.SelectedPath
            My.Settings.Save()
            txtDataFolder.Text = folderDlg.SelectedPath
            tree.Load(txtDataFolder.Text)
        End If

    End Sub

    Private Sub UpdateUI() Handles updateUITimer.Tick
        If bndBuild.work Then
            btnSelect.Enabled = False
            menuFiles.Enabled = False
        Else
            btnSelect.Enabled = True
            menuFiles.Enabled = True
        End If

        SyncLock bndBuild.outputLock
            While bndBuild.outputList.Count > 0
                txtInfo.AppendText(bndBuild.outputList(0))
                bndBuild.outputList.RemoveAt(0)
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

    Private Sub miRebuild_Click(sender As Object, e As EventArgs) Handles miRebuild.Click
        If bndBuild.work Then
            Exit Sub
        End If

        Dim selNode As TreeNode = treeFiles.SelectedNode
        If selNode IsNot Nothing Then
            If selNode.Tag Is Nothing OrElse Not FileUtil.IsFileExtractable(selNode.Tag) Then
                MsgBox("The selected item is not rebuildable.")
            Else
                Dim filename As String = selNode.Tag
                Dim thread As Thread = New Thread(Sub()
                    bndBuild.rebuild(filename)
                End Sub)
                thread.Start()
            End If
        End If
    End Sub

    Private Sub miExtract_Click(sender As Object, e As EventArgs) Handles miExtract.Click
        If bndBuild.work Then
            Exit Sub
        End If

        Dim selNode As TreeNode = treeFiles.SelectedNode
        If selNode IsNot Nothing Then
            If selNode.Tag Is Nothing OrElse Not FileUtil.IsFileExtractable(selNode.Tag) Then
                MsgBox("The selected item is not extractable.")
            Else
                Dim filename As String = selNode.Tag
                Dim thread As Thread = New Thread(Sub()
                    bndBuild.extract(filename)
                    Invoke(Sub()
                        tree.updateFileNode(selNode)
                    End Sub)
                End Sub)
                thread.Start()
            End If
        End If
    End Sub

    Private Sub miReload_Click(sender As Object, e As EventArgs) Handles miReload.Click
        If bndBuild.work Then
            Exit Sub
        End If
        UpdateNodeDisplay(Nothing)
        tree.Load(txtDataFolder.Text)
    End Sub

    Private Sub miExpandChildren_Click(sender As Object, e As EventArgs) Handles miExpandChildren.Click
        If treeFiles.SelectedNode IsNot Nothing Then
            treeFiles.SelectedNode.ExpandAll()
        End If
    End Sub

    Private Sub miCollapseChildren_Click(sender As Object, e As EventArgs) Handles miCollapseChildren.Click
        If treeFiles.SelectedNode IsNot Nothing Then
            treeFiles.SelectedNode.Collapse()
        End If
    End Sub

    Private Sub treeFiles_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeFiles.AfterSelect
        updateNodeDisplay(treeFiles.SelectedNode)
    End Sub

    Private Sub UpdateNodeDisplay(node as TreeNode)
        If node Is Nothing OrElse node.Tag Is Nothing Then
            HexViewer.Data = Nothing
            UpdateFileInfoTab(Nothing, Nothing)
            Exit Sub
        End If

        Dim filename As String = node.Tag
        Try
            Dim data As Byte() = File.ReadAllBytes(filename)
            HexViewer.Data = data
            UpdateFileInfoTab(filename, data)
        Catch e As Exception
            HexViewer.Text = "Error reading file '" & filename & "': " & e.Message
            HexViewer.Data = Nothing
        End Try
    End Sub

    Private Sub UpdateFileInfoTab(ByVal filename As String, ByVal data As Byte())
        If filename Is Nothing Or data Is Nothing Then
            TextFileInfo.Text = ""
            PictureBoxImage.Image = Nothing
            PictureBoxImage.ClientSize = New Size(1,1)
            Exit Sub
        End If

        Dim type As String = ""
        For i = 0 To 3
            If data(i) >= 32 And data(i) < 127 Then
                type += Convert.ToChar(data(i))
            Else
                Exit For
            End If
        Next
        
        TextFileInfo.Text = "Path: " & filename & vbNewLine & "Type: " & type & vbNewLine

        If type = "DDS " then
            UpdateImageTab(data)
        Else
            UpdateImageTab(Nothing)
        End If
    End Sub

    Private Sub UpdateImageTab(ByVal data As Byte())
        If data Is Nothing Then
            PictureBoxImage.Image = Nothing
            PictureBoxImage.ClientSize = New Size(1,1)
            Exit Sub
        End If
        Dim dds As New DDSImage(data, TextFileInfo)
        Try
            PictureBoxImage.Image = dds.GetImage()
            PictureBoxImage.ClientSize = PictureBoxImage.Image.Size
            TextFileInfo.Text += "Flags: " & dds.Flags.ToString("X8") & vbNewLine
            TextFileInfo.Text += "Size: " & dds.w.ToString() & "x" & dds.h.ToString() & vbNewLine
            TextFileInfo.Text += "BPP: " & dds.pfBpp.ToString() & vbNewLine
            TextFileInfo.Text += "pfFlags: " & dds.pfFlags.ToString("X8") & vbNewLine
        Catch e As Exception
            PictureBoxImage.Image = Nothing
            PictureBoxImage.ClientSize = New Size(1,1)
            TextFileInfo.Text += vbNewLine & "Error decoding image: " & e.Message & vbNewLine
        End Try
    End Sub

    Private Sub miOpenFileLocation_Click(sender As Object, e As EventArgs) Handles miOpenFileLocation.Click
        If treeFiles.SelectedNode Is Nothing OrElse treeFiles.SelectedNode.Tag Is Nothing Then
            Exit Sub
        End If

        Dim p as Process = new Process() 
        Dim pi as ProcessStartInfo = new ProcessStartInfo() 
        pi.FileName = "explorer.exe" 
        pi.Arguments = "/select,""" & treeFiles.SelectedNode.Tag & """"
        p.StartInfo = pi 
        p.Start() 
    End Sub
End Class
