Public Class frmDecrypt
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim res = AES_Decrypt(encryptedWallet, pass.Text)
        If res.Substring(0, 8) <> "arionum:" Then
            MsgBox("Invalid wallet password", vbCritical)
        Else
            decryptedWallet = res

            Me.Hide()

        End If



    End Sub

    Private Sub frmDecrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class