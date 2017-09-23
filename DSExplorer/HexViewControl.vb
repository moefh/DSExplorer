Imports System.ComponentModel

Public Class HexViewControl
    Inherits Panel

    Private dataV As Byte() = Nothing
    Private topLineV As Integer = 0

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    <Category("Appearance"), Description("Bytes to Display.")> _
    Public Property Data() As Byte()
        Get
            Return dataV
        End Get
        Set
            dataV = Value
            CalcScrollBar()
            Invalidate()
        End Set
    End Property

    Private Sub CalcScrollBar()
        Dim numLines As Integer = 0
        If dataV IsNot Nothing Then
            numLines = dataV.Length / 16 - (dataV.Length Mod 16 <> 0)
        End If
        AutoScroll = True
        AutoScrollPosition = New Point With { .X = 0, .Y = 0 }
        AutoScrollMinSize = New Size With {
            .Width = 1,
            .Height = numLines * Font.Height
        }
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        'MyBase.OnPaint(e)

        Dim foreBrush as New SolidBrush(ForeColor)
        Dim backBrush as New SolidBrush(BackColor)
        e.Graphics.FillRectangle(backBrush, Me.ClientRectangle)
        If dataV Is Nothing Then
            If DesignMode then
                e.Graphics.DrawString("HexViewControl", Me.Font, foreBrush, 0, 0)
            Else
                e.Graphics.DrawString(Me.Text, Me.Font, foreBrush, 0, 0)
            End If
            Exit Sub
        End If

        Dim y As Single = AutoScrollPosition.Y Mod Font.Height
        Dim off As Integer = Int((-AutoScrollPosition.Y) / Font.Height) * 16
        While off < dataV.Length
            Dim line As String = dumpLine(off)
            off += 16
            e.Graphics.DrawString(line, Me.Font, foreBrush, 0, y)
            y += Me.Font.Height
            If y > Height Then
                Exit While
            End If
        End While
    End Sub

    Private Function DumpLine(off As Integer) As String
        Dim line As String

        line = String.Format("{0,8:x8} | ", off)
        Dim len = 15
        If off + len >= dataV.Length Then len = dataV.Length - off - 1
        For i = 0 To len
            line += String.Format("{0,2:x2} ", dataV(off + i))
            If i = 7 Then line += " "
        Next
        For i = len+1 To 15
            line += "   "
            If i = 7 Then line += " "
        next
        line += "| "
        For i = 0 To len
            If dataV(off + i) >= 32 AndAlso dataV(off + i) < 127 Then
                line += Chr(dataV(off + i))
            Else
                line += "."
            End If
            If i = 7 Then line += " "
        Next

        For i = len+1 To 15
            line += " "
            If i = 7 Then line += " "
        Next
        line += " |"
        Return line
    End Function
End Class
