Imports System.Drawing.Imaging

Public Class DDSImage

    Public Const ddsCompressNONE As Integer = 0
    Public Const ddsCompressBC1  As Integer = 1
    Public Const ddsCompressBC2  As Integer = 2
    Public Const ddsCompressBC3  As Integer = 3

    Public Const flagsLINEARSIZE   As UInteger = &H00080000

    Public Const pfFlagsFOURCC     As UInteger = &H00000004
    Public Const pfFlagsPALETTE    As UInteger = &H00000020
    Public Const pfFlagsRGB        As UInteger = &H00000040
    Public Const pfFlagsLUMINANCE  As UInteger = &H00020000

    Public Const caps1FlagsCOMPLEX = &H00000008
    Public Const caps1FlagsTEXTURE = &H00001000
    Public Const caps1FlagsMIPMAP  = &H00001000

    Public Const caps2FlagsCUBEMAP = &H00000200

    Public size as Integer
    Public flags As UInteger
    Public w As Integer
    Public h As Integer
    Public pitch As UInteger
    Public depth As Integer
    Public numMipMaps As Integer

    Public pfSize As Integer
    Public pfFlags As UInteger
    Public pfFourCC As String
    Public pfBpp As Integer
    Public pfRmask As UInteger
    Public pfGmask As UInteger
    Public pfBmask As UInteger
    Public pfAmask As UInteger

    Public caps1 As UInteger
    Public caps2 As UInteger

    Public compressFormat As Integer

    Dim data As Byte()
    Dim out As TextBox

    Public Sub New(ByVal data As Byte(), ByVal out As TextBox)
        Me.data = data
        Me.out = out
    End Sub

    Private Function GetU32(ByVal off As UInteger) As UInteger
        Return BitConverter.ToUInt32(data, off)
    End Function

    Private Function GetU16(ByVal off As UInteger) As UInt16
        Return BitConverter.ToUInt16(data, off)
    End Function

    Private Function GetString(ByVal off As UInteger, ByVal len As UInteger) As String
        Dim s As String = ""
        For i = 0 To len-1
            s += Convert.ToChar(data(off + i))
        Next
        Return s
    End Function

    Public Sub ReadHeader()
        Dim magic As String = GetString(0, 4)
        If magic <> "DDS " Then Throw New Exception("Bad DDS file type")

        size = GetU32(4)
        flags = GetU32(8)
        h = GetU32(12)
        w = GetU32(16)
        pitch = GetU32(20)
        depth = GetU32(24)
        numMipMaps = GetU32(28)

        pfSize = GetU32(76)
        pfFlags = GetU32(80)
        pfFourCC = GetString(84, 4)
        pfBpp = GetU32(88)
        pfRmask = GetU32(92)
        pfGmask = GetU32(96)
        pfBmask = GetU32(100)
        pfAmask = GetU32(104)

        caps1 = GetU32(108)
        caps2 = GetU32(112)

        If (pfFlags And pfFlagsLUMINANCE) <> 0 Then
            If pfBpp <> 8 And pfBpp <> 16 Then
                Throw New Exception("Invalid BPP for luminance image: " & pfBpp.ToString())
            End If
            Throw New Exception("Luminance image not supported yet")
        Else If (pfFlags And pfFlagsRGB) <> 0 Then
            If pfBpp <> 8 And pfBpp <> 16 And pfBpp <> 24 And pfBpp <> 32 Then
                Throw New Exception("Invalid BPP for RGB image: " & pfBpp.ToString())
            End If
            Throw New Exception("RGB image not supported yet")
        Else If (pfFlags And pfFlagsFOURCC)
            Select Case pfFourCC
                Case "DXT1"
                    pfBpp = 4
                    compressFormat = ddsCompressBC1
                Case "DXT3"
                    pfBpp = 4
                    compressFormat = ddsCompressBC2
                Case "DXT5"
                    pfBpp = 4
                    compressFormat = ddsCompressBC3
                Case Else
                    Throw New Exception("Unsupported DDS type: " & pfFourCC)
            End Select
            If (pfFourCC = "DXT1" Or pfFourCC = "DXT3" or pfFourCC = "DXT5") and (flags And flagsLINEARSIZE) = 0 Then
                Throw New Exception("Can't decode image type " & pfFourCC & " without LINEARSIZE")
            End If
        End If

        If (caps2 And caps2FlagsCUBEMAP) <> 0 Then
            Throw New Exception("Unsupported format: CubeMap")
        End If
    End Sub

    Public Function GetImage() As Bitmap
        ReadHeader()
        Dim img As New Bitmap(w, h, PixelFormat.Format32bppArgb)
        Dim rect As New Rectangle(0, 0, w, h)
        dim bmp As BitmapData = img.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb)
        Try
            Decompress(bmp)
        Finally
            img.UnlockBits(bmp)
        End Try
        Return img
    End Function

    Private Sub DecodeColor(ByVal c As UInteger, ByVal col As UInteger(,), ByVal colNum As Integer)
        Dim r As UInteger = (c >> 11) And &H1F
        Dim g As UInteger = (c >>  5) And &H3F
        Dim b As UInteger = (c      ) And &H1F
        col(colNum, 0) = (r << 3) Or (r >> 2)
        col(colNum, 1) = (g << 2) Or (g >> 4)
        col(colNum, 2) = (b << 3) Or (b >> 2)
    End Sub

    Private Sub InterpolateColor(ByVal color As UInteger(,), ByVal src1 As Integer, ByVal src2 As Integer, ByVal dest As Integer)
        For i = 0 To 2
            color(dest, i) = (2 * color(src1, i) + color(src2, i)) \ 3
        Next
    End Sub

    Private Function DumpColor(ByVal col As UInteger(,), ByVal i As Integer)
        Return "(" & col(i,0) & "," & col(i,1) & "," & col(i,2) & ")"
    End Function

    Private Sub DecodeColorBlock(ByVal off As UInteger, ByVal block As Byte())
        Dim c0 As UInteger = GetU16(off)
        Dim c1 As UInteger = GetU16(off + 2)
        off += 4

        ' decode block palette
        Dim col(3, 2) as UInteger
        DecodeColor(c0, col, 0)
        DecodeColor(c1, col, 1)

        If (c0 > c1) Or (compressFormat <> ddsCompressBC1) Then
            InterpolateColor(col, 0, 1, 2)
            InterpolateColor(col, 1, 0, 3)
        Else
            For i = 0 To 2
                col(2, i) = (col(0, i) + col(1, i) + 1) \ 2
                col(3, i) = 0
            Next
        End If

        ' decode block pixels
        Dim d As Integer = 0
        For y = 0 To 3
            Dim inds As UInteger = data(off + y)
            For x = 0 To 3
                Dim ind = inds And &H3
                inds >>= 2
                block(d+0) = col(ind, 2)
                block(d+1) = col(ind, 1)
                block(d+2) = col(ind, 0)
                If compressFormat = ddsCompressBC1 Then
                    If (c0 <= c1) And (ind = 3) Then
                        block(d+3) = 0
                    End If
                End If
                d += 4
            Next
        Next
    End Sub

    Private Sub DecodeAlphaBlockBC2(ByVal off As UInteger, ByVal block As Byte())
    End Sub

    Private Sub DecodeAlphaBlockBC3(ByVal off As UInteger, ByVal block As Byte())
    End Sub

    Private Sub CopyBlock(ByVal img As BitmapData, ByVal block As Byte(), ByVal sx As Integer, ByVal sy As Integer)
        For y = 0 To 3
            If sy + y >= img.Height Then Exit For
            Dim dest As UInteger = img.Scan0 + (sy+y) * img.Stride + sx*pfBpp
            For x = 0 To 3
                If sx + x >= img.Width Then Exit For
                System.Runtime.InteropServices.Marshal.Copy(block, y*16 + x*4, dest, pfBpp)
                dest += pfBpp
            Next
        Next
    End Sub

    Private Sub Decompress(ByVal img As BitmapData)
        Dim block(64) as Byte

        Dim off As UInteger = 128

        For y = 0 To h\4-1
            For x = 0 To w\4-1
                For i = 0 To 63
                    block(i) = 255
                Next

                Select Case compressFormat
                Case ddsCompressBC1
                    DecodeColorBlock(off, block)
                    off += 8

                Case ddsCompressBC2
                    DecodeAlphaBlockBC2(off, block)
                    DecodeColorBlock(off + 8, block)
                    off += 16

                case ddsCompressBC3
                    DecodeAlphaBlockBC3(off, block)
                    DecodeColorBlock(off + 8, block)
                    off += 16

                End Select

                CopyBlock(img, block, 4*x, 4*y)
            Next
        Next
    End Sub

End Class
