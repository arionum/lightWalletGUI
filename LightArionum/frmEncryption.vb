Public Class frmEncryption
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Hide()

    End Sub

    Private Sub btnEncrypt_Click(sender As Object, e As EventArgs) Handles btnEncrypt.Click
        If pass.Text <> pass2.Text Then
            MsgBox("The passwords do not match", vbCritical)
            Exit Sub
        End If
        If pass.Text.Length < 8 Then
            MsgBox("The password must be at least 8 characters long.", vbCritical)
            Exit Sub
        End If
        encryptPass = pass.Text
        isEncrypted = True


        Me.Hide()

    End Sub

    Private Sub frmEncryption_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class