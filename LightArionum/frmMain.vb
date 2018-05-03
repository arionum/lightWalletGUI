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
Option Strict Off

Imports System.IO
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports System.Threading
Imports System.Text
Imports System.Globalization
Imports QRCoder

Public Class frmMain
    Dim min_thread(32) As Thread
    Dim min_last_speed As Decimal
    Dim i As Integer
    Private trd As Thread

    Private Const HTCLIENT As Integer = &H1
    Private Const HTCAPTION As Integer = &H2
    Private Const WM_NCHITTEST As Integer = &H84
    Public Const WM_NCLBUTTONDBLCLK As Integer = &HA3

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_NCLBUTTONDBLCLK Then Return
        MyBase.WndProc(m)

        If m.Msg = WM_NCHITTEST AndAlso m.Result = HTCLIENT Then
            m.Result = HTCAPTION
        End If
    End Sub



    Public Async Function sync_data() As Task

        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("EN-US")

            If sync_err > 5 Then
                MsgBox("Could not sync data. Attempted 5 times to connect to the nodes", vbCritical)
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

            balance = Decimal.Parse(res)

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
            DataGridView1.Rows.Clear()


            For Each x In res
                Dim nTimestamp As Double = x("date")
                Dim nDateTime As System.DateTime = New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
                nDateTime = nDateTime.AddSeconds(nTimestamp)


                Me.DataGridView1.Rows.Add(nDateTime.ToString("MM\/dd\/yyyy HH:mm"), x("type"), x("val"), x("fee"), x("confirmations"), x("src"), x("dst"), x("message"), x("id"))

            Next
        Catch ex As Exception
            MsgBox("Could not sync data: " & ex.Message, vbCritical)
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.CurrentCulture = New CultureInfo("EN-US")


        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        miner_threads.Text = Environment.ProcessorCount
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
                Dim fstream As FileStream = New FileStream(path + "\wallet.aro", FileMode.Create)
                file = New StreamWriter(fstream, Encoding.ASCII)
                file.WriteLine(wallet)
                file.Close()
                fstream.Close()
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
        Try
            sendAmt.Text = "1.00"
            fee.Text = 1 * 0.0025
        Catch ex As Exception

        End Try

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
        If (total_peers < 2) Then
            MsgBox("Could not get the peer list from arionum.com. Please check your internet connection!", vbCritical)
            End
        End If

        trd = New Thread(AddressOf sync_data)
        trd.IsBackground = True
        trd.Start()
        Dim qrGenerator As New QRCodeGenerator
        Dim QRCodeData As QRCodeData = qrGenerator.CreateQrCode(address & "|" & public_key & "|" & private_key, QRCodeGenerator.ECCLevel.Q)
        Dim QRCode As New QRCode(QRCodeData)
        Dim qrCodeImage As Bitmap = QRCode.GetGraphic(20)
        PictureBox1.Image = qrCodeImage
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
            MsgBox("Not enough balance to send this transaction!", vbCritical)
            Exit Sub
        End If





        If MsgBox("Are you sure you wish to send " + sendAmt.Text + " ARO to " + sendTo.Text + " ?", vbYesNo) = vbYes Then
            Dim uTime As Int64
            uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            Dim info As String
            info = FormatNumber(sum, 8).Replace(",", "") + "-" + FormatNumber(f, 8).Replace(",", "") + "-" + sendTo.Text + "-" + sendMsg.Text + "-1-" + public_key + "-" + uTime.ToString
            ' Console.WriteLine(info)
            frmLog.flog("Transaction data: " & info)
            Dim file As System.IO.StreamWriter
            Try
                file = My.Computer.FileSystem.OpenTextFileWriter("aro.log", True)
                file.WriteLine(info)
                file.Close()
            Catch ex As Exception

            End Try

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


            res = get_json(peer + "/api.php?q=send&version=1&public_key=" + public_key + "&signature=" + sig + "&dst=" + sendTo.Text + "&val=" + FormatNumber(sum, 8).Replace(",", "") + "&date=" + uTime.ToString + "&message=" + sendMsg.Text)

            If res.ToString = "" Then
                r = Generator.Next(0, total_peers - 1)
                peer = peers(r)

                res = get_json(peer + "/api.php?q=send&version=1&public_key=" + public_key + "&signature=" + sig + "&dst=" + sendTo.Text + "&val=" + FormatNumber(sum, 8).Replace(",", "") + "&date=" + uTime.ToString + "&message=" + sendMsg.Text)
                If res.ToString = "" Then
                    MsgBox("Could not send the transaction to the peer (" & peer & ")! Please try again!", vbCritical)
                    Exit Sub
                End If
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
            Dim fstream As FileStream = New FileStream(path + "\wallet.aro", FileMode.Create)
            file = New StreamWriter(fstream, Encoding.ASCII)
            file.WriteLine(wallet)
            file.Close()
            fstream.Close()
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

            Dim file As System.IO.StreamWriter
            Try

                Dim fstream As FileStream = New FileStream(SaveFileDialog1.FileName, FileMode.Create)
                file = New StreamWriter(fstream, Encoding.ASCII)
                file.WriteLine(wallet)
                file.Close()
                fstream.Close()
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
        sync_data()

        Exit Sub
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

                        Dim fstream As FileStream = New FileStream(path + "\wallet.aro", FileMode.Create)
                        file = New StreamWriter(fstream, Encoding.ASCII)
                        file.WriteLine(wallet)
                        file.Close()
                        fstream.Close()

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

                    'Console.WriteLine(public_key)

                    enc = encoder.GetBytes(public_key)
                    For i = 0 To 8
                        'Console.WriteLine(SimpleBase.Base58.Bitcoin.Encode(enc))
                        enc = sha512hasher.ComputeHash(enc)
                    Next
                    Dim xx As Byte() = SimpleBase.Base58.Bitcoin.Decode("11111" & SimpleBase.Base58.Bitcoin.Encode(enc))
                    For i = 0 To xx.Count - 1
                        Console.Write(Convert.ToInt16(xx(i)) & " ")
                    Next

                    address = SimpleBase.Base58.Bitcoin.Encode(enc)
                    Console.WriteLine(address)
                    txtaddress.Text = address
                    If trd.IsAlive = False Then
                        trd = New Thread(AddressOf sync_data)
                        trd.IsBackground = True
                        trd.Start()
                    End If
                    Dim qrGenerator As New QRCodeGenerator
                    Dim QRCodeData As QRCodeData = qrGenerator.CreateQrCode(address & "|" & public_key & "|" & private_key, QRCodeGenerator.ECCLevel.Q)
                    Dim QRCode As New QRCode(QRCodeData)
                    Dim qrCodeImage As Bitmap = QRCode.GetGraphic(20)
                    PictureBox1.Image = qrCodeImage
                End If
            End If
        Catch ex As Exception
            MsgBox("Could not import the wallet file.", vbCritical)
        End Try
    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        e.ThrowException = False
        Console.WriteLine(e.ToString)
    End Sub

    Private Sub miner_button_Click(sender As Object, e As EventArgs) Handles miner_button.Click
        If miner_button.Text = "Start Mining" Then
            miner_button.Text = "Stop"
            miner_pool.Enabled = False
            miner_threads.Enabled = False
            min_pool = miner_pool.Text.Trim
            If miner_pm.Checked = True Then
                min_pm = True
            Else
                MsgBox("You are solo mining against this node. If you do not own this node and do not have a secure connection to it, you should stop imediatly as your private key would be sent to it when generating a block!", vbCritical)
                min_pm = False
            End If
            miner_update()
            pool_update.Enabled = True
            min_threads = Int(miner_threads.Text)

            If (min_threads > 32) Then min_threads = 32
            For i = 0 To min_threads - 1
                min_thread(i) = New Thread(AddressOf miner)
                min_thread(i).IsBackground = True
                thd_ids(i) = min_thread(i).ManagedThreadId
                min_thread(i).Start()

            Next
            min_log("Starting to mine with " & min_threads & " threads")
        Else
            miner_button.Text = "Start Mining"
            miner_pool.Enabled = True
            miner_threads.Enabled = True
            pool_update.Enabled = False
            For i = 0 To min_threads - 1
                min_thread(i).Abort()
            Next
            For i = 0 To 128
                thd_speeds(i) = 0
                thd_ids(i) = 0
            Next
            min_log("Stopped mining")
        End If

    End Sub

    Private Sub pool_update_Tick(sender As Object, e As EventArgs) Handles pool_update.Tick
        Try


            miner_update()
            Dim speed As Decimal = 0

            For i = 0 To 128
                speed = speed + thd_speeds(i)
            Next



            If miner_log.Lines.Count > 200 Then
                miner_log.Text = ""
            End If
            If (speed > 0 And speed <> min_last_speed) Then
                min_last_speed = speed
                min_log("Hashing speed: " & speed & " H/s")
            End If
            If min_buffer <> "" Then
                min_log(min_buffer.Trim)
                min_buffer = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub min_log(ByVal log As String)

        miner_log.Text = log & vbCrLf & miner_log.Text
    End Sub



    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = 196684 Then
            frmLog.Show()
        ElseIf e.KeyData = 196692 And testnet = False Then
            Dim tmp
            testnet = True
            MsgBox("TestNet Enabled")
            Dim peer_data As String
            peer_data = http_get("http://api.arionum.com/testnet.txt")
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
            miner_pool.Text = peers(0)
            miner_pm.Checked = False
            sync_data()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtaddress_TextChanged(sender As Object, e As EventArgs) Handles txtaddress.TextChanged

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub btnDrag_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        frmQR.PictureBox1.Image = PictureBox1.Image
        frmQR.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim val = InputBox("Please enter the value in ARO")
        If IsNumeric(val) Then
            If val > 0 Then


                Dim msg = InputBox("Please enter the transaction message (optional)")

                Dim qr As String = "arosend|" & address & "|" & val & "|" & msg & " "
                Dim qrGenerator As New QRCodeGenerator
                Dim QRCodeData As QRCodeData = qrGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q)
                Dim QRCode As New QRCode(QRCodeData)
                Dim qrCodeImage As Bitmap = QRCode.GetGraphic(20)
                frmQR.PictureBox1.Image = qrCodeImage
                frmQR.ShowDialog()
                'MsgBox(qr)
            End If
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmMain_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick

    End Sub
End Class
