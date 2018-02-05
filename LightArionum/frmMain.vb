'The MIT License (MIT)
'Copyright(c) 2018 AroDev 

'www.arionum.com

'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software And associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, And to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions:

'The above copyright notice And this permission notice shall be included In all
'copies Or substantial portions of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND,
'EXPRESS Or IMPLIED, INCLUDING BUT Not LIMITED TO THE WARRANTIES OF
'MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT.
'IN NO EVENT SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
'DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE
'Or OTHER DEALINGS IN THE SOFTWARE.

Imports System.IO
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports System.Threading


Public Class frmMain
    Dim i As Integer
    Private trd As Thread
    Public Async Function sync_data() As Task
        Try


            If sync_err > 5 Then
                sync_err = 0
                Exit Function
            End If
            Dim Generator As System.Random = New System.Random()
            Dim r = Generator.Next(0, total_peers - 1)
            Dim peer = peers(r)

            Dim res

            res = get_json(peer + "/api.php?q=getPendingBalance&account=" + address)

            If res.ToString = "" Then
                sync_err = sync_err + 1
                Await sync_data()
                Exit Function
            End If

            balance = res

            lblBalance.Text = "Balance: " + balance.ToString

            Me.statusNode.Text = peer


            res = get_json(peer + "/api.php?q=currentBlock")

            If res.ToString = "" Then
                Exit Function
            End If
            Me.statusBlock.Text = res("height")

            res = get_json(peer + "/api.php?q=getTransactions&account=" + address)

            If res.ToString = "" Then
                Exit Function
            End If
            Me.DataGridView1.Rows.Clear()

            For Each x In res
                Dim nTimestamp As Double = x("date")
                Dim nDateTime As System.DateTime = New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
                nDateTime = nDateTime.AddSeconds(nTimestamp)

                Me.DataGridView1.Rows.Add(nDateTime.ToString("MM\/dd\/yyyy HH:mm"), x("type"), x("val"), x("fee"), x("confirmations"), x("src"), x("dst"), x("message"), x("id"))
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        Dim path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\Arionum"

        If (Not Directory.Exists(path)) Then
            FileSystem.MkDir(path)
        End If

        If (Not File.Exists(path + "\wallet.aro") Or 1 = 2) Then
            'generating the wallet and the first address

            generate_keys()
            If (private_key.Length < 20) Then
                MsgBox("Could not generate the keys", vbCritical)
                End
            End If
            Dim wallet = "arionum:" + private_key + ":" + public_key
            Dim pass As String = "1"
            Dim pass2 As String = "2"

            If MsgBox("A new wallet has been generated. Would you like to encrypt this wallet?", vbYesNo, "Encryption") = vbYes Then

                frmEncryption.ShowDialog()
                If (encryptPass.Length < 8) Then

                    MsgBox("Could not encrypt the wallet. Exiting...", vbCritical)
                    End

                End If
                wallet = AES_Encrypt(wallet, encryptPass)

            End If

            Dim file As System.IO.StreamWriter
            Try
                file = My.Computer.FileSystem.OpenTextFileWriter(path + "\wallet.aro", False)
                file.WriteLine(wallet)
                file.Close()
            Catch ex As Exception
                MsgBox("Could not write the wallet file. Please check the permissions on " + path + "\wallet.aro", vbCritical)
                End
            End Try



        Else
            'importing the wallet
            isEncrypted = False
            Dim s As String
            Dim tr As System.IO.TextReader = New System.IO.StreamReader(path + "\wallet.aro")
            s = tr.ReadToEnd
            tr.Close()

            If s.Substring(0, 8) <> "arionum:" Then
                encryptedWallet = s
                frmDecrypt.ShowDialog()
                s = decryptedWallet
                If s.Length = 0 Then
                    MsgBox("Could not decrypt wallet. Exiting...", vbCritical)
                    End
                End If
                isEncrypted = True
            End If

            Dim wal = s.Split(":")
            private_key = wal(1)
            public_key = wal(2)



        End If
        public_key = public_key.Trim()
        private_key = private_key.Trim()
        If public_key.Length < 20 Or private_key.Length < 20 Then

            MsgBox("Could not load the wallet. The keys seem invalid.", vbCritical)
            End
        End If

        txtpub.Text = public_key
        txtpriv.Text = private_key
        sendAmt.Text = "1.00"
        fee.Text = sendAmt.Text * 0.0025
        If isEncrypted = True Then
            btnDecrypt.Text = "Decrypt"
        Else
            btnDecrypt.Text = "Encrypt"
        End If
        Dim encoder As New Text.UTF8Encoding()
        Dim enc As Byte()

        Dim sha512hasher As New System.Security.Cryptography.SHA512Managed()


        enc = encoder.GetBytes(public_key)
        For i = 0 To 8
            enc = sha512hasher.ComputeHash(enc)
        Next



        address = SimpleBase.Base58.Bitcoin.Encode(enc)
        txtaddress.Text = address
        Dim tmp() As String


        Dim peer_data As String
        peer_data = http_get("http://api.arionum.com/peers.txt")
        'tmp = RegularExpressions.Regex.Split(peer_data, Environment.NewLine)
        Dim arg() As String = {vbCrLf, vbLf}
        tmp = peer_data.Split(arg, StringSplitOptions.None)
        peer_data = ""


        total_peers = 0
        For Each t As String In tmp
            If total_peers > 99 Then Exit For

            t = t.Trim()
            If t <> "" Then
                peers(total_peers) = t
                total_peers = total_peers + 1
            End If
        Next
        If total_peers > 10 Then


            peer_data = String.Join(vbCrLf, peers).Trim()
            Try
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(path + "\peers.txt", False)
                file.Write(peer_data)
                file.Close()
            Catch ex As Exception

            End Try
        Else
            Dim s As String
            Dim tr As System.IO.TextReader = New System.IO.StreamReader(path + "\peers.txt")
            s = tr.ReadToEnd
            tr.Close()
            tmp = s.Split(arg, StringSplitOptions.None)
            peer_data = ""
            total_peers = 0
            For Each t As String In tmp
                If total_peers > 99 Then Exit For

                t = t.Trim()
                If t <> "" Then
                    peers(total_peers) = t
                    total_peers = total_peers + 1

                End If
            Next
        End If


        trd = New Thread(AddressOf sync_data)
        trd.IsBackground = True
        trd.Start()
    End Sub

    Private Sub InitializeComponent(asdas)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDecrypt = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(492, 274)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(118, 29)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDecrypt
        '
        Me.btnDecrypt.Location = New System.Drawing.Point(492, 239)
        Me.btnDecrypt.Name = "btnDecrypt"
        Me.btnDecrypt.Size = New System.Drawing.Size(118, 29)
        Me.btnDecrypt.TabIndex = 1
        Me.btnDecrypt.Text = "Decrypt"
        Me.btnDecrypt.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(492, 204)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(118, 29)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Backup"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(47, 98)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(157, 134)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(149, 108)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(149, 108)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.ClientSize = New System.Drawing.Size(622, 315)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnDecrypt)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmMain"
        Me.Text = "Arionum LightWallet v0.1"
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        End
    End Sub



    Private Sub sendAmt_TextChanged_1(sender As Object, e As EventArgs) Handles sendAmt.TextChanged
        Try


            Dim f As Decimal = Convert.ToDecimal(sendAmt.Text) * 0.0025
            If f < 0.00000001 Then f = 0.00000001
            If f > 10 Then f = 10

            fee.Text = f
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sendAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles sendAmt.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or Asc(e.KeyChar) = 8 Or (e.KeyChar = "." And sender.Text.IndexOf(".") = -1))


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If sendTo.Text.Length < 10 Then Exit Sub
        If Convert.ToDecimal(sendAmt.Text) < 0.00000001 Then
            MsgBox("Invalid amount", vbCritical)
            Exit Sub
        End If
        Dim sum As Decimal = Convert.ToDecimal(sendAmt.Text)
        Dim f As Decimal = sum * 0.0025
        If f < 0.00000001 Then f = 0.00000001
        If f > 10 Then f = 10
        If (balance < f + sum) Then
            ' MsgBox("Not enough balance to send this transaction!", vbCritical)
            ' Exit Sub
        End If





        If MsgBox("Are you sure you wish to send " + sendAmt.Text + " ARO to " + sendTo.Text + " ?", vbYesNo) = vbYes Then
            Dim uTime As Int64
            uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            Dim info As String
            info = FormatNumber(sum, 8).Replace(",", "") + "-" + FormatNumber(f, 8).Replace(",", "") + "-" + sendTo.Text + "-" + sendMsg.Text + "-1-" + public_key + "-" + uTime.ToString
            Console.WriteLine(info)
            Dim res As String
            Static Generator As System.Random = New System.Random()
            Dim r = Generator.Next(0, total_peers - 1)
            Dim peer = peers(r)

            Dim tmp_key As String = coin2pem(private_key, True)
            Dim tmp_key2 As String = coin2pem(public_key, False)


            Dim textReader As TextReader = New StringReader(tmp_key)
            Dim pemReader As PemReader = New PemReader(textReader)
            Dim _keyPair As AsymmetricCipherKeyPair = pemReader.ReadObject()
            Dim _privateKeyParams As ECPrivateKeyParameters = _keyPair.Private
            Dim _publicKeyParams As ECPublicKeyParameters = _keyPair.Public



            Dim signer As ISigner = SignerUtilities.GetSigner("SHA-256withECDSA")
            signer.Init(True, _keyPair.Private)
            Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(info)
            signer.BlockUpdate(bytes, 0, bytes.Length)
            Dim signature As Byte() = signer.GenerateSignature()

            Dim sig As String = SimpleBase.Base58.Bitcoin.Encode(signature)


            res = get_json(peer + "/api.php?q=send&version=1&public_key=" + public_key + "&signature=" + sig + "&dst=" + sendTo.Text + "&val=" + FormatNumber(sum, 8) + "&date=" + uTime.ToString + "&message=" + sendMsg.Text)

            If res.ToString = "" Then
                MsgBox("Could not send the transaction to the peer! Please try again!", vbCritical)
                Exit Sub
            End If
            lasttx.Visible = True
            lbllasttx.Visible = True
            lasttx.Text = res.ToString
        End If


    End Sub

    Private Sub btnDecrypt_Click(sender As Object, e As EventArgs) Handles btnDecrypt.Click
        Dim path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\Arionum"
        Dim wallet As String = "arionum:" + private_key + ":" + public_key
        If isEncrypted = True Then
            If MsgBox("Are you sure you wish to decrypt the wallet ?", vbYesNo) = vbNo Then
                Exit Sub
            End If
            isEncrypted = False
        Else
            frmEncryption.ShowDialog()
            If isEncrypted = False Then Exit Sub
            If (encryptPass.Length < 8) Then
                MsgBox("Could not encrypt the wallet.", vbCritical)
                Exit Sub
            End If
            wallet = AES_Encrypt(wallet, encryptPass)
            isEncrypted = True

        End If
        Dim file As System.IO.StreamWriter
        Try
            file = My.Computer.FileSystem.OpenTextFileWriter(path + "\wallet.aro", False)
            file.WriteLine(wallet)
            file.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
            MsgBox("Could not write the wallet file. Please check the permissions on " + path + "\wallet.aro. Also, please save a backup of the current wallet in a different location.", vbCritical)
            If isEncrypted = True Then
                isEncrypted = False
            Else
                isEncrypted = True
            End If
            Exit Sub
        End Try
        If isEncrypted = True Then
            btnDecrypt.Text = "Decrypt"
        Else
            btnDecrypt.Text = "Encrypt"
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        SaveFileDialog1.Filter = "ARO Wallet|*.aro"
        Dim wallet As String
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If isEncrypted = True Then
                wallet = encryptedWallet
            Else
                wallet = "arionum:" + private_key + ":" + public_key
            End If
            '
            Dim file As System.IO.StreamWriter
            Try
                file = My.Computer.FileSystem.OpenTextFileWriter(SaveFileDialog1.FileName, False)
                file.WriteLine(wallet)
                file.Close()
            Catch ex As Exception
                MsgBox("Could not write the wallet file. Please check the permissions on " + SaveFileDialog1.FileName, vbCritical)
            End Try
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If trd.IsAlive = False Then
                trd = New Thread(AddressOf sync_data)
                trd.IsBackground = True
                trd.Start()
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try


            If MsgBox("Restoring another wallet will delete the current wallet. Are you sure you wish to proceed?", vbYesNo) = vbYes Then
                Dim wallet As String

                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\Arionum"
                    Dim uTime As Int64
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim file As System.IO.StreamWriter
                    If isEncrypted = True Then
                        wallet = encryptedWallet
                    Else
                        wallet = "arionum:" + private_key + ":" + public_key
                    End If
                    Try
                        file = My.Computer.FileSystem.OpenTextFileWriter(path + "\wallet_backup_" & uTime.ToString & ".aro", False)
                        file.WriteLine(wallet)
                        file.Close()
                    Catch ex As Exception
                        MsgBox("Could not write a backup the old wallet file. Restore failed.", vbCritical)
                        Exit Sub
                    End Try



                    isEncrypted = False
                    Dim s As String
                    Dim tr As System.IO.TextReader = New System.IO.StreamReader(OpenFileDialog1.FileName)
                    s = tr.ReadToEnd
                    tr.Close()

                    If s.Substring(0, 8) <> "arionum:" Then
                        encryptedWallet = s
                        frmDecrypt.ShowDialog()
                        s = decryptedWallet
                        If s.Length = 0 Then
                            MsgBox("Could not decrypt wallet. Exiting...", vbCritical)
                            End
                        End If
                        isEncrypted = True
                    End If

                    Dim wal = s.Split(":")
                    private_key = wal(1)
                    public_key = wal(2)
                    If public_key.Length < 10 Or private_key.Length < 10 Then

                        MsgBox("Could not import the wallet. The keys seem invalid.", vbCritical)
                        Exit Sub
                    End If
                    If isEncrypted = True Then
                        wallet = encryptedWallet
                    Else
                        wallet = "arionum:" + private_key + ":" + public_key
                    End If

                    Try
                        file = My.Computer.FileSystem.OpenTextFileWriter(path + "\wallet.aro", False)
                        file.WriteLine(wallet)
                        file.Close()
                    Catch ex As Exception
                        MsgBox("Could not write the wallet file. Please check the permissions on " + path + "\wallet.aro. Restore failed.", vbCritical)
                        Exit Sub
                    End Try


                    public_key = public_key.Trim()
                    private_key = private_key.Trim()
                    txtpub.Text = public_key
                    txtpriv.Text = private_key
                    If isEncrypted = True Then
                        btnDecrypt.Text = "Decrypt"
                    Else
                        btnDecrypt.Text = "Encrypt"
                    End If
                    Dim encoder As New Text.UTF8Encoding()
                    Dim enc As Byte()

                    Dim sha512hasher As New System.Security.Cryptography.SHA512Managed()


                    enc = encoder.GetBytes(public_key)
                    For i = 0 To 8
                        enc = sha512hasher.ComputeHash(enc)
                    Next



                    address = SimpleBase.Base58.Bitcoin.Encode(enc)
                    txtaddress.Text = address
                    If trd.IsAlive = False Then
                        trd = New Thread(AddressOf sync_data)
                        trd.IsBackground = True
                        trd.Start()
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox("Could not import the wallet file.", vbCritical)
        End Try
    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        e.ThrowException = False

    End Sub
End Class
