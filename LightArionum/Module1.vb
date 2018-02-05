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

Imports System.IO
Imports System.Net

Imports Newtonsoft.Json.Linq

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


            request = DirectCast(WebRequest.Create(url), HttpWebRequest)

            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()

            Return rawresp.Trim()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
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
End Module

