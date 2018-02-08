<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label2 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDecrypt = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lbllasttx = New System.Windows.Forms.Label()
        Me.lasttx = New System.Windows.Forms.TextBox()
        Me.sendAmt = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.sendMsg = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.fee = New System.Windows.Forms.Label()
        Me.sendFee = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.sendTo = New System.Windows.Forms.TextBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Confirmations = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.From = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtpriv = New System.Windows.Forms.TextBox()
        Me.txtpub = New System.Windows.Forms.TextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.miner_log = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.miner_threads = New System.Windows.Forms.TextBox()
        Me.miner_pool = New System.Windows.Forms.TextBox()
        Me.miner_button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblNode = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusNode = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblBalance = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusBlock = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.pool_update = New System.Windows.Forms.Timer(Me.components)
        Me.miner_pm = New System.Windows.Forms.CheckBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Label2 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(3, 60)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(98, 18)
        Label2.TabIndex = 2
        Label2.Text = "Pool / Node"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(3, 86)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(69, 18)
        Label4.TabIndex = 3
        Label4.Text = "Threads"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(831, 56)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(118, 29)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDecrypt
        '
        Me.btnDecrypt.Location = New System.Drawing.Point(831, 19)
        Me.btnDecrypt.Name = "btnDecrypt"
        Me.btnDecrypt.Size = New System.Drawing.Size(118, 29)
        Me.btnDecrypt.TabIndex = 1
        Me.btnDecrypt.Text = "Decrypt"
        Me.btnDecrypt.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(707, 56)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(118, 29)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Backup"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lbllasttx)
        Me.TabPage2.Controls.Add(Me.lasttx)
        Me.TabPage2.Controls.Add(Me.sendAmt)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.sendMsg)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.fee)
        Me.TabPage2.Controls.Add(Me.sendFee)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.sendTo)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(938, 434)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Send Transactions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lbllasttx
        '
        Me.lbllasttx.AutoSize = True
        Me.lbllasttx.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllasttx.Location = New System.Drawing.Point(6, 301)
        Me.lbllasttx.Name = "lbllasttx"
        Me.lbllasttx.Size = New System.Drawing.Size(116, 13)
        Me.lbllasttx.TabIndex = 14
        Me.lbllasttx.Text = "Last transaction id:"
        Me.lbllasttx.Visible = False
        '
        'lasttx
        '
        Me.lasttx.Location = New System.Drawing.Point(10, 317)
        Me.lasttx.MaxLength = 250
        Me.lasttx.Name = "lasttx"
        Me.lasttx.Size = New System.Drawing.Size(920, 21)
        Me.lasttx.TabIndex = 13
        Me.lasttx.Visible = False
        '
        'sendAmt
        '
        Me.sendAmt.Location = New System.Drawing.Point(12, 122)
        Me.sendAmt.MaxLength = 15
        Me.sendAmt.Name = "sendAmt"
        Me.sendAmt.Size = New System.Drawing.Size(136, 21)
        Me.sendAmt.TabIndex = 12
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(388, 220)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(127, 34)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Send Transaction"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'sendMsg
        '
        Me.sendMsg.Location = New System.Drawing.Point(12, 177)
        Me.sendMsg.MaxLength = 250
        Me.sendMsg.Name = "sendMsg"
        Me.sendMsg.Size = New System.Drawing.Size(920, 21)
        Me.sendMsg.TabIndex = 10
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(11, 157)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Message:"
        '
        'fee
        '
        Me.fee.AutoSize = True
        Me.fee.Location = New System.Drawing.Point(268, 125)
        Me.fee.Name = "fee"
        Me.fee.Size = New System.Drawing.Size(0, 15)
        Me.fee.TabIndex = 8
        '
        'sendFee
        '
        Me.sendFee.AutoSize = True
        Me.sendFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sendFee.Location = New System.Drawing.Point(224, 125)
        Me.sendFee.Name = "sendFee"
        Me.sendFee.Size = New System.Drawing.Size(32, 13)
        Me.sendFee.TabIndex = 7
        Me.sendFee.Text = "Fee:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(157, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "ARO"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(10, 102)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Amount:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Pay to:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(9, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(134, 16)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Send Transaction:"
        '
        'sendTo
        '
        Me.sendTo.Location = New System.Drawing.Point(13, 63)
        Me.sendTo.MaxLength = 128
        Me.sendTo.Name = "sendTo"
        Me.sendTo.Size = New System.Drawing.Size(919, 21)
        Me.sendTo.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtaddress)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(938, 434)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Receive Funds"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column1, Me.Column3, Me.Column4, Me.Confirmations, Me.From, Me.Column2, Me.Column6, Me.ID})
        Me.DataGridView1.Location = New System.Drawing.Point(9, 68)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(923, 362)
        Me.DataGridView1.TabIndex = 5
        '
        'Column5
        '
        Me.Column5.HeaderText = "Date"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 58
        '
        'Column1
        '
        Me.Column1.HeaderText = "Action"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 65
        '
        'Column3
        '
        Me.Column3.HeaderText = "Sum"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 58
        '
        'Column4
        '
        Me.Column4.HeaderText = "Fee"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 53
        '
        'Confirmations
        '
        Me.Confirmations.HeaderText = "Confirmations"
        Me.Confirmations.Name = "Confirmations"
        Me.Confirmations.Width = 108
        '
        'From
        '
        Me.From.HeaderText = "From"
        Me.From.Name = "From"
        Me.From.Width = 61
        '
        'Column2
        '
        Me.Column2.HeaderText = "To"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 46
        '
        'Column6
        '
        Me.Column6.HeaderText = "Message"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 83
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Width = 44
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 15)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Transactions:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Your address:"
        '
        'txtaddress
        '
        Me.txtaddress.Location = New System.Drawing.Point(9, 27)
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.Size = New System.Drawing.Size(923, 21)
        Me.txtaddress.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(7, 91)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(946, 462)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.txtpriv)
        Me.TabPage3.Controls.Add(Me.txtpub)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(938, 434)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Wallet info"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(690, 20)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Keep your private key secure! You can use the following keys to re-create your wa" &
    "llet."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 177)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Private key"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Public Key"
        '
        'txtpriv
        '
        Me.txtpriv.Location = New System.Drawing.Point(13, 193)
        Me.txtpriv.Multiline = True
        Me.txtpriv.Name = "txtpriv"
        Me.txtpriv.Size = New System.Drawing.Size(920, 72)
        Me.txtpriv.TabIndex = 1
        '
        'txtpub
        '
        Me.txtpub.Location = New System.Drawing.Point(12, 82)
        Me.txtpub.Multiline = True
        Me.txtpub.Name = "txtpub"
        Me.txtpub.Size = New System.Drawing.Size(921, 61)
        Me.txtpub.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.miner_pm)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.miner_log)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.miner_threads)
        Me.TabPage4.Controls.Add(Label4)
        Me.TabPage4.Controls.Add(Label2)
        Me.TabPage4.Controls.Add(Me.miner_pool)
        Me.TabPage4.Controls.Add(Me.miner_button)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(938, 434)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Miner"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 160)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 15)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "Log:"
        '
        'miner_log
        '
        Me.miner_log.Location = New System.Drawing.Point(7, 178)
        Me.miner_log.Multiline = True
        Me.miner_log.Name = "miner_log"
        Me.miner_log.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.miner_log.Size = New System.Drawing.Size(926, 253)
        Me.miner_log.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(2, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(277, 25)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Arionum Integrated Miner"
        '
        'miner_threads
        '
        Me.miner_threads.Location = New System.Drawing.Point(107, 86)
        Me.miner_threads.Name = "miner_threads"
        Me.miner_threads.Size = New System.Drawing.Size(382, 21)
        Me.miner_threads.TabIndex = 4
        '
        'miner_pool
        '
        Me.miner_pool.Location = New System.Drawing.Point(107, 57)
        Me.miner_pool.Name = "miner_pool"
        Me.miner_pool.Size = New System.Drawing.Size(382, 21)
        Me.miner_pool.TabIndex = 1
        Me.miner_pool.Text = "http://aropool.com"
        '
        'miner_button
        '
        Me.miner_button.Location = New System.Drawing.Point(7, 117)
        Me.miner_button.Name = "miner_button"
        Me.miner_button.Size = New System.Drawing.Size(149, 34)
        Me.miner_button.TabIndex = 0
        Me.miner_button.Text = "Start Mining"
        Me.miner_button.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label1.Location = New System.Drawing.Point(115, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(478, 59)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "ARIONUM LIGHT"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.lblNode, Me.statusNode, Me.ToolStripStatusLabel3, Me.lblBalance, Me.ToolStripStatusLabel6, Me.ToolStripStatusLabel4, Me.statusBlock})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 551)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(956, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(112, 17)
        Me.ToolStripStatusLabel1.Text = "Connected to node:"
        '
        'lblNode
        '
        Me.lblNode.Name = "lblNode"
        Me.lblNode.Size = New System.Drawing.Size(0, 17)
        '
        'statusNode
        '
        Me.statusNode.Name = "statusNode"
        Me.statusNode.Size = New System.Drawing.Size(10, 17)
        Me.statusNode.Text = "."
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(582, 17)
        Me.ToolStripStatusLabel3.Spring = True
        '
        'lblBalance
        '
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(13, 17)
        Me.lblBalance.Text = "0"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.AutoSize = False
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(120, 17)
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(82, 17)
        Me.ToolStripStatusLabel4.Text = "Current block:"
        '
        'statusBlock
        '
        Me.statusBlock.Name = "statusBlock"
        Me.statusBlock.Size = New System.Drawing.Size(22, 17)
        Me.statusBlock.Text = "....."
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "wallet.aro"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.LightArionum.My.Resources.Resources.sigla_arionum
        Me.PictureBox1.Location = New System.Drawing.Point(13, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 83)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 240000
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(707, 18)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 29)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Restore"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Aro Wallet|*.aro"
        '
        'pool_update
        '
        Me.pool_update.Interval = 2000
        '
        'miner_pm
        '
        Me.miner_pm.AutoSize = True
        Me.miner_pm.Checked = True
        Me.miner_pm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.miner_pm.Location = New System.Drawing.Point(495, 61)
        Me.miner_pm.Name = "miner_pm"
        Me.miner_pm.Size = New System.Drawing.Size(92, 19)
        Me.miner_pm.TabIndex = 10
        Me.miner_pm.Text = "Pool Mining"
        Me.miner_pm.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label23)
        Me.TabPage5.Controls.Add(Me.TextBox5)
        Me.TabPage5.Controls.Add(Me.Label22)
        Me.TabPage5.Controls.Add(Me.Label21)
        Me.TabPage5.Controls.Add(Me.Label20)
        Me.TabPage5.Controls.Add(Me.Label19)
        Me.TabPage5.Controls.Add(Me.TextBox4)
        Me.TabPage5.Controls.Add(Me.TextBox3)
        Me.TabPage5.Controls.Add(Me.TextBox2)
        Me.TabPage5.Controls.Add(Me.TextBox1)
        Me.TabPage5.Controls.Add(Me.Label18)
        Me.TabPage5.Controls.Add(Me.Label15)
        Me.TabPage5.Controls.Add(Me.Label13)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(938, 434)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "About"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(3, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(914, 57)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = resources.GetString("Label13.Text")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 72)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(824, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "There has been no PRE-MINE or ICO  on Arionum, the development has been done and " &
    "will always be done free of charge by the Arionum Developers."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 100)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(551, 15)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "If you would like to support the Arionum development, you can donate to one of th" &
    "e addresses below:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 146)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(911, 21)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "5WuRMXGM7Pf8NqEArVz1NxgSBptkimSpvuSaYC79g1yo3RDQc8TjVtGH5chQWQV7CHbJEuq9DmW5fbmCE" &
    "W4AghQr"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(6, 193)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(911, 21)
        Me.TextBox2.TabIndex = 4
        Me.TextBox2.Text = "LWgqzbXGeucKaMmJEvwaAWPFrAgKiJ4Y4m"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(6, 236)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(911, 21)
        Me.TextBox3.TabIndex = 5
        Me.TextBox3.Text = "1LdoMmYitb4C3pXoGNLL1VRj7xk3smGXoU"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(6, 281)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(911, 21)
        Me.TextBox4.TabIndex = 6
        Me.TextBox4.Text = "0x4B904bDf071E9b98441d25316c824D7b7E447527"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 128)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 15)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "ARO"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(3, 175)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 15)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "LTC"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 218)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(30, 15)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "BTC"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(3, 263)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(31, 15)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "ETH"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 307)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(32, 15)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "BCH"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(6, 325)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(911, 21)
        Me.TextBox5.TabIndex = 11
        Me.TextBox5.Text = "qrtkqrl3mxzdzl66nchkgdv73uu3rf7jdy7el2vduw"
        '
        'frmMain
        '
        Me.ClientSize = New System.Drawing.Size(956, 573)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnDecrypt)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Arionum LightWallet v0.3a"
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents btnDecrypt As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtaddress As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtpriv As TextBox
    Friend WithEvents txtpub As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents sendMsg As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents fee As Label
    Friend WithEvents sendFee As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents sendTo As TextBox
    Friend WithEvents sendAmt As TextBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents lblNode As ToolStripStatusLabel
    Friend WithEvents statusNode As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents statusBlock As ToolStripStatusLabel
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Confirmations As DataGridViewTextBoxColumn
    Friend WithEvents From As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents lbllasttx As Label
    Friend WithEvents lasttx As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblBalance As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As ToolStripStatusLabel
    Friend WithEvents Button2 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label17 As Label
    Friend WithEvents miner_log As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents miner_threads As TextBox
    Friend WithEvents miner_pool As TextBox
    Friend WithEvents miner_button As Button
    Friend WithEvents pool_update As Timer
    Friend WithEvents miner_pm As CheckBox
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Label23 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label13 As Label
End Class
