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


Imports System.Text
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Crypto.Generators
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.Asn1.X9
Imports Org.BouncyCastle.Asn1.Sec
Imports Org.BouncyCastle.Math
Imports Org.BouncyCastle.Math.EC
Imports Org.BouncyCastle.OpenSsl
Imports Liphsoft.Crypto.Argon2
Imports System.IO
Imports System.Net

Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography
Imports System.Threading

Module Module1
    Public encryptPass As String
    Public isEncrypted As Boolean
    Public private_key As String
    Public public_key As String
    Public encryptedWallet As String = ""
    Public decryptedWallet As String = ""
    Public address As String
    Public peers(100) As String
    Public total_peers As Integer
    Public balance As Decimal
    Public sync_err As Integer = 0
    Public min_pool
    Public min_speed
    Public min_diff As String
    Public min_block As String = ""
    Public min_pubkey As String = ""
    Public min_limit As Integer = "240"
    Public min_threads As Integer
    Public thd_ids(128) As Integer
    Public thd_speeds(128) As Decimal
    Public min_buffer As String
    Public testnet = False
    Public min_pm As Boolean
    Public last_hr As Int64
    Public Function pem2coin(ByVal data As String)
        data = Replace(data, "-----BEGIN PUBLIC KEY-----", "")
        data = Replace(data, "-----END PUBLIC KEY-----", "")
        data = Replace(data, "-----BEGIN EC PRIVATE KEY-----", "")
        data = Replace(data, "-----END EC PRIVATE KEY-----", "")
        data = Replace(data, "-----BEGIN PRIVATE KEY-----", "")
        data = Replace(data, "-----END PRIVATE KEY-----", "")
        data = Replace(data, "-----END PRIVATE KEY-----", "")
        data = Replace(data, vbCrLf, "")
        data = Replace(data, vbLf, "")
        Dim enc As Byte()
        enc = Convert.FromBase64String(data)
        Return SimpleBase.Base58.Bitcoin.Encode(enc)


    End Function

    Public Function coin2pem(ByVal data As String, Optional ByVal is_private As Boolean = False)
        Dim enc As Byte() = SimpleBase.Base58.Bitcoin.Decode(data)
        Dim tmp As String = Convert.ToBase64String(enc)
        Dim final As String
        Dim tmp2 As String = ""
        For i = 0 To tmp.Length - 1
            tmp2 = tmp2 & tmp(i)
            If i Mod 64 = 0 And i > 0 Then
                tmp2 = tmp2 & vbCrLf
            End If

        Next

        If is_private = True Then
            final = "-----BEGIN EC PRIVATE KEY-----" & vbCrLf & tmp2 & vbCrLf & "-----END EC PRIVATE KEY-----"
        Else
            final = "-----BEGIN PUBLIC KEY-----" & vbCrLf & tmp2 & vbCrLf & "-----END PUBLIC KEY-----"
        End If
        Return final
    End Function




    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.SHA256Managed
        Dim encrypted As String = ""
        Try
            Dim iv(15) As Byte
            Dim hash As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Dim tmp As Byte()
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            AES.BlockSize = 128
            AES.GenerateIV()

            iv = AES.IV
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            tmp = System.Text.ASCIIEncoding.ASCII.GetBytes(encrypted)
            Dim tmp2(tmp.Length + 15) As Byte
            Array.Copy(iv, tmp2, 16)
            Array.Copy(tmp, 0, tmp2, 16, tmp.Length)



            Return Convert.ToBase64String(tmp2)
        Catch ex As Exception
            Return input 'If encryption fails, return the unaltered input.
        End Try
    End Function
    'Decrypt a string with AES
    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.SHA256Managed
        Dim decrypted As String = ""
        input = input.Trim
        Dim iv(15) As Byte
        Dim tmp As Byte()
        Try
            tmp = Convert.FromBase64String(input)

            Array.Copy(tmp, 0, iv, 0, 16)
            Dim tmp2(tmp.Length - 17) As Byte
            Array.Copy(tmp, 16, tmp2, 0, tmp.Length - 16)
            input = Encoding.ASCII.GetString(tmp2)

            Dim hash As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))

            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            AES.BlockSize = 128
            AES.IV = iv
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor

            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return input 'If decryption fails, return the unaltered input.
        End Try
    End Function
    Public Function http_get(ByVal url As String) As String

        'Console.WriteLine(url)
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader

        Try
            frmLog.flog("Requesting: " & url)

            request = DirectCast(WebRequest.Create(url), HttpWebRequest)

            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()
            '   Console.WriteLine(rawresp)
            frmLog.flog("Response: " & rawresp.Trim())
            Return rawresp.Trim()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            frmLog.flog("Exception: " & ex.ToString)
            Return ""
        End Try
    End Function

    Public Function get_json(ByVal url As String)
        On Error GoTo err
        Dim rawresp As String
        rawresp = http_get(url)
        If rawresp = "" Then
            Return ""
        End If

        Dim array As JObject = JObject.Parse(rawresp)


        If array("status") = "ok" Then

            Return array("data")
        Else
err:
            Return ""
        End If

    End Function


    Public Function generate_keys()
        Dim EC As X9ECParameters = SecNamedCurves.GetByName("secp256k1")

        Dim domainParams As ECDomainParameters = New ECDomainParameters(EC.Curve, EC.G, EC.N, EC.H)
        Dim Random As SecureRandom = New SecureRandom()

        ' Generate EC KeyPair
        Dim keyGen As ECKeyPairGenerator = New ECKeyPairGenerator()
        Dim keyParams As ECKeyGenerationParameters = New ECKeyGenerationParameters(domainParams, Random)
        keyGen.Init(keyParams)
        Dim keyPair As AsymmetricCipherKeyPair = keyGen.GenerateKeyPair()

        Dim privateKeyParams As ECPrivateKeyParameters = keyPair.Private
        Dim publicKeyParams As ECPublicKeyParameters = keyPair.Public

        ' Get Private Key
        Dim privD As BigInteger = privateKeyParams.D
        Dim privBytes As Byte() = privD.ToByteArray()

        Dim temp(31) As Byte
        If privBytes.Length = 33 Then
            Array.Copy(privBytes, 1, temp, 0, 32)
            privBytes = temp
        ElseIf privBytes.Length < 32 Then
            temp = Enumerable.Repeat(Of Byte)(0, 32).ToArray()
            Array.Copy(privBytes, 0, temp, 32 - privBytes.Length, privBytes.Length)
            privBytes = temp
        End If
        Dim privKey As String = BitConverter.ToString(privBytes).Replace("-", "")

        ' Get Compressed Public Key
        Dim q As ECPoint = publicKeyParams.Q
        Dim fp As FpPoint = New FpPoint(EC.Curve, q.AffineXCoord, q.AffineYCoord)
        Dim enc As Byte() = fp.GetEncoded(True)
        Dim compressedPubKey As String = BitConverter.ToString(enc).Replace("-", "")

        ' Get Uncompressed Public Key
        enc = fp.GetEncoded(False)
        Dim uncompressedPubKey As String = BitConverter.ToString(enc).Replace("-", "")
        Dim pubk As Byte() = SimpleBase.Base16.Decode(compressedPubKey)
        public_key = SimpleBase.Base58.Bitcoin.Encode(pubk)
        Dim pvkas As Byte() = SimpleBase.Base16.Decode(privKey)
        private_key = SimpleBase.Base58.Bitcoin.Encode(pvkas)
        ' Output

        Dim textWriter As StringWriter = New StringWriter()
        Dim pemWriter As PemWriter = New PemWriter(textWriter)


        pemWriter.WriteObject(keyPair.Private)
        pemWriter.Writer.Flush()
        Dim tmp_private As String = textWriter.ToString()
        ' Console.WriteLine(pem2coin(textWriter.ToString()))
        private_key = pem2coin(textWriter.ToString())
        textWriter = New StringWriter()
        pemWriter = New PemWriter(textWriter)
        pemWriter.WriteObject(keyPair.Public)
        pemWriter.Writer.Flush()


        Dim a As New Chilkat.PublicKey
        a.LoadFromString(textWriter.ToString())
        public_key = a.GetEncoded(True, "base58")
        If public_key.Length < 10 Or private_key.Length < 10 Then

            MsgBox("Could not generate a valid key pair.", vbCritical)
            End
        End If

        Return True
    End Function
    Public Function generate_nonce()
        Dim byte_count As Byte() = New Byte(32) {}
        Dim random_number As New RNGCryptoServiceProvider()
        random_number.GetBytes(byte_count)
        Return System.Convert.ToBase64String(byte_count).Replace("+", "").Replace("/", "").Replace("=", "")
    End Function

    Public Function SplitInParts(s As String, partLength As Integer) As IEnumerable(Of String)
        If String.IsNullOrEmpty(s) Then
            Throw New ArgumentNullException("String cannot be null or empty.")
        End If
        If partLength <= 0 Then
            Throw New ArgumentException("Split length has to be positive.")
        End If
        Return Enumerable.Range(0, Math.Ceiling(s.Length / partLength)).Select(Function(i) s.Substring(i * partLength, If(s.Length - (i * partLength) >= partLength, partLength, Math.Abs(s.Length - (i * partLength)))))
    End Function
    Private Function BytesToString(ByVal Input As Byte()) As String
        Dim Result As New System.Text.StringBuilder(Input.Length * 2)
        Dim Part As String
        For Each b As Byte In Input
            Part = Conversion.Hex(b)
            If Part.Length = 1 Then Part = "0" & Part
            Result.Append(Part)
        Next
        Return Result.ToString()
    End Function

    Public Async Function miner() As Task

        Try
            Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("EN-US")
            Dim hasher As PasswordHasher = New PasswordHasher(1, 524288, 1)
            Dim nonce As String = generate_nonce()
            Dim j As Integer
            j = 0
            Dim start As DateTime = Now
            Dim stopt As DateTime
            Dim el As TimeSpan
            Dim hr As Decimal
            Dim argon As String
            While (1)

                If min_pubkey = "" Then Continue While

                If j = 10 Then
                    stopt = Now
                    el = stopt.Subtract(start)
                    hr = 10 / el.TotalSeconds.ToString
                    thd_speeds(Thread.CurrentThread.ManagedThreadId) = hr
                    start = Now
                    j = 0

                End If


                Dim base As String
                Dim base2 As String
                base = min_pubkey & "-" & nonce & "-" & min_block & "-" & min_diff
                Dim sha512 As SHA512 = SHA512Managed.Create()
                argon = hasher.Hash(base)
                base2 = base & argon


                Dim hashf As Byte() = Encoding.UTF8.GetBytes(base2)
                For i = 1 To 6
                    hashf = sha512.ComputeHash(hashf)
                Next

                base = BytesToString(hashf)

                Dim x = SplitInParts(base, 2)
                Dim duration As String
                duration = Convert.ToInt32(x(10), 16).ToString & Convert.ToInt32(x(15), 16).ToString & Convert.ToInt32(x(20), 16).ToString & Convert.ToInt32(x(23), 16).ToString & Convert.ToInt32(x(31), 16).ToString & Convert.ToInt32(x(40), 16).ToString & Convert.ToInt32(x(45), 16).ToString & Convert.ToInt32(x(55), 16).ToString

                Dim res As Int64 = Numerics.BigInteger.Divide(Numerics.BigInteger.Parse(duration), Numerics.BigInteger.Parse(min_diff))

                If (res <= min_limit) Then
                    Await miner_submit(nonce, argon)
                    nonce = generate_nonce()
                End If
                j = j + 1
            End While
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function
    Public Function miner_update()
        Dim purl As String = min_pool & "/mine.php?q=info&address=" & address & "&worker=lightwallet"

        Dim uTime As Int64
        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        If uTime - last_hr > 600 Then
            purl = purl & "&hashrate=" & min_speed
            last_hr = uTime
        End If
        Dim res = get_json(purl)
        If res.ToString <> "" Then
            min_block = res("block")
            min_diff = res("difficulty")
            If min_pm = True Then
                min_pubkey = res("public_key")
                min_limit = res("limit")
            Else
                min_pubkey = public_key
                min_limit = 240
            End If
            If testnet = True Then
                min_limit = 1000000000
            End If
            ' frmMain.min_log("Updating mining info - " & min_diff)
        Else
            frmMain.min_log("Updating mining info failed. Please check the pool")
        End If

    End Function
    Public Async Function miner_submit(ByVal nonce As String, ByVal argon As String) As Task


        Try
            argon = argon.Substring(30)
            min_buffer = "--> Submitting Nonce: " & nonce & " / " & argon & vbCrLf & min_buffer



            Dim postData As String = "argon=" & WebUtility.UrlEncode(argon) & "&nonce=" & nonce & "&address=" & address
            If min_pm = False Then
                postData = postData & "&public_key=" & public_key & "&private_key=" & private_key
            End If
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create(min_pool & "/mine.php?q=submitNonce"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = True

            postReq.ContentType = "application/x-www-form-urlencoded"
            postReq.ContentLength = byteData.Length

            Dim postreqstream As Stream = postReq.GetRequestStream()
            postreqstream.Write(byteData, 0, byteData.Length)
            postreqstream.Close()
            Dim postresponse As HttpWebResponse

            postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
            Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
            Dim res As String = postreqreader.ReadToEnd

            ' Console.WriteLine(res)
            If res = "" Then
                Exit Function
            End If

            Dim array As JObject = JObject.Parse(res)


            If array("status") = "ok" Then

                min_buffer = "---- Nonce accepted !!! We've earned some shares! ----" & vbCrLf & min_buffer
            Else
                min_buffer = "Nonce rejected! Pool response: " & array("data").ToString & vbCrLf & min_buffer
            End If

        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Function
End Module

