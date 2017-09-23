Public Class TreeNodeSorter
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim tx As TreeNode = CType(x, TreeNode)
        Dim ty As TreeNode = CType(y, TreeNode)

        Return String.Compare(tx.Text, ty.Text)
    End Function
End Class

