<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtDataFolder = New System.Windows.Forms.TextBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.treeFiles = New System.Windows.Forms.TreeView()
        Me.TabView = New System.Windows.Forms.TabControl()
        Me.TabPageHexView = New System.Windows.Forms.TabPage()
        Me.HexViewer = New DSExplorer.HexViewControl()
        Me.TabPageDump = New System.Windows.Forms.TabPage()
        Me.TextFileInfo = New System.Windows.Forms.TextBox()
        Me.TabPageImage = New System.Windows.Forms.TabPage()
        Me.PanelImage = New System.Windows.Forms.Panel()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.menuFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miExtract = New System.Windows.Forms.ToolStripMenuItem()
        Me.miRebuild = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.miExpandChildren = New System.Windows.Forms.ToolStripMenuItem()
        Me.miCollapseChildren = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.miOpenFileLocation = New System.Windows.Forms.ToolStripMenuItem()
        Me.miReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBoxImage = New System.Windows.Forms.PictureBox()
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer2.Panel1.SuspendLayout
        Me.SplitContainer2.Panel2.SuspendLayout
        Me.SplitContainer2.SuspendLayout
        Me.TabView.SuspendLayout
        Me.TabPageHexView.SuspendLayout
        Me.TabPageDump.SuspendLayout
        Me.TabPageImage.SuspendLayout
        Me.PanelImage.SuspendLayout
        Me.menuFiles.SuspendLayout
        CType(Me.PictureBoxImage,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtDataFolder
        '
        Me.txtDataFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtDataFolder.Location = New System.Drawing.Point(83, 12)
        Me.txtDataFolder.MinimumSize = New System.Drawing.Size(50, 4)
        Me.txtDataFolder.Name = "txtDataFolder"
        Me.txtDataFolder.Size = New System.Drawing.Size(784, 20)
        Me.txtDataFolder.TabIndex = 0
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(873, 10)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(89, 23)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "Select..."
        Me.btnSelect.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Data Folder:"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 38)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtInfo)
        Me.SplitContainer1.Size = New System.Drawing.Size(950, 547)
        Me.SplitContainer1.SplitterDistance = 383
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.treeFiles)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.TabView)
        Me.SplitContainer2.Size = New System.Drawing.Size(950, 383)
        Me.SplitContainer2.SplitterDistance = 357
        Me.SplitContainer2.TabIndex = 0
        '
        'treeFiles
        '
        Me.treeFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.treeFiles.Location = New System.Drawing.Point(3, 3)
        Me.treeFiles.Name = "treeFiles"
        Me.treeFiles.Size = New System.Drawing.Size(351, 377)
        Me.treeFiles.TabIndex = 0
        '
        'TabView
        '
        Me.TabView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TabView.Controls.Add(Me.TabPageHexView)
        Me.TabView.Controls.Add(Me.TabPageDump)
        Me.TabView.Controls.Add(Me.TabPageImage)
        Me.TabView.Location = New System.Drawing.Point(3, 3)
        Me.TabView.Name = "TabView"
        Me.TabView.SelectedIndex = 0
        Me.TabView.Size = New System.Drawing.Size(583, 377)
        Me.TabView.TabIndex = 0
        '
        'TabPageHexView
        '
        Me.TabPageHexView.Controls.Add(Me.HexViewer)
        Me.TabPageHexView.Location = New System.Drawing.Point(4, 22)
        Me.TabPageHexView.Name = "TabPageHexView"
        Me.TabPageHexView.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageHexView.Size = New System.Drawing.Size(575, 351)
        Me.TabPageHexView.TabIndex = 1
        Me.TabPageHexView.Text = "Hex Viewer"
        Me.TabPageHexView.UseVisualStyleBackColor = true
        '
        'HexViewer
        '
        Me.HexViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.HexViewer.AutoScroll = true
        Me.HexViewer.AutoScrollMinSize = New System.Drawing.Size(1, 0)
        Me.HexViewer.Data = Nothing
        Me.HexViewer.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.HexViewer.Location = New System.Drawing.Point(0, 0)
        Me.HexViewer.Name = "HexViewer"
        Me.HexViewer.Size = New System.Drawing.Size(572, 348)
        Me.HexViewer.TabIndex = 0
        '
        'TabPageDump
        '
        Me.TabPageDump.Controls.Add(Me.TextFileInfo)
        Me.TabPageDump.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDump.Name = "TabPageDump"
        Me.TabPageDump.Size = New System.Drawing.Size(575, 351)
        Me.TabPageDump.TabIndex = 2
        Me.TabPageDump.Text = "File Info"
        Me.TabPageDump.UseVisualStyleBackColor = true
        '
        'TextFileInfo
        '
        Me.TextFileInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextFileInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextFileInfo.Location = New System.Drawing.Point(0, 0)
        Me.TextFileInfo.Multiline = true
        Me.TextFileInfo.Name = "TextFileInfo"
        Me.TextFileInfo.ReadOnly = true
        Me.TextFileInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextFileInfo.Size = New System.Drawing.Size(575, 351)
        Me.TextFileInfo.TabIndex = 0
        Me.TextFileInfo.WordWrap = false
        '
        'TabPageImage
        '
        Me.TabPageImage.Controls.Add(Me.PanelImage)
        Me.TabPageImage.Location = New System.Drawing.Point(4, 22)
        Me.TabPageImage.Name = "TabPageImage"
        Me.TabPageImage.Size = New System.Drawing.Size(575, 351)
        Me.TabPageImage.TabIndex = 3
        Me.TabPageImage.Text = "Image"
        Me.TabPageImage.UseVisualStyleBackColor = true
        '
        'PanelImage
        '
        Me.PanelImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.PanelImage.AutoScroll = true
        Me.PanelImage.Controls.Add(Me.PictureBoxImage)
        Me.PanelImage.Location = New System.Drawing.Point(0, 0)
        Me.PanelImage.Name = "PanelImage"
        Me.PanelImage.Size = New System.Drawing.Size(575, 351)
        Me.PanelImage.TabIndex = 0
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtInfo.Location = New System.Drawing.Point(3, 3)
        Me.txtInfo.Multiline = true
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(944, 157)
        Me.txtInfo.TabIndex = 0
        Me.txtInfo.WordWrap = false
        '
        'StatusStrip
        '
        Me.StatusStrip.Location = New System.Drawing.Point(0, 588)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(974, 22)
        Me.StatusStrip.TabIndex = 4
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'menuFiles
        '
        Me.menuFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miExtract, Me.miRebuild, Me.ToolStripSeparator1, Me.miExpandChildren, Me.miCollapseChildren, Me.ToolStripSeparator2, Me.miOpenFileLocation, Me.miReload})
        Me.menuFiles.Name = "menuFiles"
        Me.menuFiles.Size = New System.Drawing.Size(174, 148)
        '
        'miExtract
        '
        Me.miExtract.Name = "miExtract"
        Me.miExtract.Size = New System.Drawing.Size(173, 22)
        Me.miExtract.Text = "Extract"
        '
        'miRebuild
        '
        Me.miRebuild.Name = "miRebuild"
        Me.miRebuild.Size = New System.Drawing.Size(173, 22)
        Me.miRebuild.Text = "Rebuild"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(170, 6)
        '
        'miExpandChildren
        '
        Me.miExpandChildren.Name = "miExpandChildren"
        Me.miExpandChildren.Size = New System.Drawing.Size(173, 22)
        Me.miExpandChildren.Text = "Expand Children"
        '
        'miCollapseChildren
        '
        Me.miCollapseChildren.Name = "miCollapseChildren"
        Me.miCollapseChildren.Size = New System.Drawing.Size(173, 22)
        Me.miCollapseChildren.Text = "Collapse Children"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(170, 6)
        '
        'miOpenFileLocation
        '
        Me.miOpenFileLocation.Name = "miOpenFileLocation"
        Me.miOpenFileLocation.Size = New System.Drawing.Size(173, 22)
        Me.miOpenFileLocation.Text = "Open File Location"
        '
        'miReload
        '
        Me.miReload.Name = "miReload"
        Me.miReload.Size = New System.Drawing.Size(173, 22)
        Me.miReload.Text = "Reload Tree"
        '
        'PictureBoxImage
        '
        Me.PictureBoxImage.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxImage.Name = "PictureBoxImage"
        Me.PictureBoxImage.Size = New System.Drawing.Size(575, 351)
        Me.PictureBoxImage.TabIndex = 0
        Me.PictureBoxImage.TabStop = false
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 610)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.txtDataFolder)
        Me.Name = "MainWindow"
        Me.Text = "DarkSouls Explorer"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.Panel2.PerformLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        Me.SplitContainer2.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        Me.TabView.ResumeLayout(false)
        Me.TabPageHexView.ResumeLayout(false)
        Me.TabPageDump.ResumeLayout(false)
        Me.TabPageDump.PerformLayout
        Me.TabPageImage.ResumeLayout(false)
        Me.PanelImage.ResumeLayout(false)
        Me.menuFiles.ResumeLayout(false)
        CType(Me.PictureBoxImage,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents txtDataFolder As TextBox
    Friend WithEvents btnSelect As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtInfo As TextBox
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents treeFiles As TreeView
    Friend WithEvents menuFiles As ContextMenuStrip
    Friend WithEvents miExtract As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents miReload As ToolStripMenuItem
    Friend WithEvents miExpandChildren As ToolStripMenuItem
    Friend WithEvents TabView As TabControl
    Friend WithEvents TabPageHexView As TabPage
    Friend WithEvents HexViewer As HexViewControl
    Friend WithEvents TabPageDump As TabPage
    Friend WithEvents TextFileInfo As TextBox
    Friend WithEvents miRebuild As ToolStripMenuItem
    Friend WithEvents miCollapseChildren As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents miOpenFileLocation As ToolStripMenuItem
    Friend WithEvents TabPageImage As TabPage
    Friend WithEvents PanelImage As Panel
    Friend WithEvents PictureBoxImage As PictureBox
End Class
