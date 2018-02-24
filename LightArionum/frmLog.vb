Public Class frmLog
    Function flog(ByVal log As String)
        TextBox1.Text = TextBox1.Text & vbCrLf & log & vbCrLf
    End Function
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub frmLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class