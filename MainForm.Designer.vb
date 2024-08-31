<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.Button1.ForeColor = System.Drawing.Color.Red
        Me.Button1.Location = New System.Drawing.Point(19, 60)
        Me.Button1.Margin = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(217, 408)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "連接 USB-SPI 轉接板"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button5.Location = New System.Drawing.Point(28, 156)
        Me.Button5.Margin = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(576, 94)
        Me.Button5.TabIndex = 40
        Me.Button5.Text = "傳送圖片"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 34)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(261, 492)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "連接裝置"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Button4)
        Me.GroupBox5.Controls.Add(Me.Button5)
        Me.GroupBox5.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.GroupBox5.Location = New System.Drawing.Point(322, 252)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(618, 274)
        Me.GroupBox5.TabIndex = 11
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "檔案"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(28, 58)
        Me.Button4.Margin = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(576, 94)
        Me.Button4.TabIndex = 42
        Me.Button4.Text = "選擇圖片"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(472, 111)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(74, 29)
        Me.TextBox2.TabIndex = 53
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(392, 111)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(74, 29)
        Me.TextBox1.TabIndex = 52
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(552, 186)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(74, 29)
        Me.TextBox9.TabIndex = 51
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(472, 186)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(74, 29)
        Me.TextBox8.TabIndex = 50
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(392, 186)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(74, 29)
        Me.TextBox7.TabIndex = 49
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(552, 149)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(74, 29)
        Me.TextBox6.TabIndex = 48
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(472, 148)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(74, 29)
        Me.TextBox5.TabIndex = 47
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(392, 148)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(74, 29)
        Me.TextBox4.TabIndex = 46
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(552, 112)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(74, 29)
        Me.TextBox3.TabIndex = 45
        '
        'Button2
        '
        Me.Button2.AutoSize = True
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("新細明體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(1188, 42)
        Me.Button2.Margin = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(197, 54)
        Me.Button2.TabIndex = 39
        Me.Button2.Text = "清除"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'RichTextBox3
        '
        Me.RichTextBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.RichTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox3.Font = New System.Drawing.Font("細明體", 9.75!)
        Me.RichTextBox3.ForeColor = System.Drawing.Color.Red
        Me.RichTextBox3.Location = New System.Drawing.Point(961, 113)
        Me.RichTextBox3.Margin = New System.Windows.Forms.Padding(8, 5, 8, 5)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.Size = New System.Drawing.Size(424, 413)
        Me.RichTextBox3.TabIndex = 123
        Me.RichTextBox3.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(957, 59)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 20)
        Me.Label1.TabIndex = 124
        Me.Label1.Text = "暫存器值"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(329, 76)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 20)
        Me.Label2.TabIndex = 125
        Me.Label2.Text = "權重："
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(768, 105)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(91, 29)
        Me.TextBox10.TabIndex = 126
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(768, 158)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(91, 29)
        Me.TextBox11.TabIndex = 127
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(694, 139)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.TabIndex = 128
        Me.Label3.Text = "Row："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(687, 82)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 20)
        Me.Label4.TabIndex = 129
        Me.Label4.Text = "Column："
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1418, 582)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox11)
        Me.Controls.Add(Me.TextBox10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.RichTextBox3)
        Me.Controls.Add(Me.TextBox9)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox8)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.TextBox7)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(8, 2, 8, 2)
        Me.Name = "MainForm"
        Me.Text = "SPI傳輸系統"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button5 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
