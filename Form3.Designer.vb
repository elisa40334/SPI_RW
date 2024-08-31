<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.button_Read_BMP = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ColorCheckBox = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(18, 18)
        Me.RichTextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(864, 62)
        Me.RichTextBox2.TabIndex = 0
        Me.RichTextBox2.Text = ""
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.PictureBox2.Location = New System.Drawing.Point(18, 92)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(866, 566)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'button_Read_BMP
        '
        Me.button_Read_BMP.Font = New System.Drawing.Font("新細明體", 14.0!)
        Me.button_Read_BMP.Location = New System.Drawing.Point(918, 18)
        Me.button_Read_BMP.Margin = New System.Windows.Forms.Padding(4)
        Me.button_Read_BMP.Name = "button_Read_BMP"
        Me.button_Read_BMP.Size = New System.Drawing.Size(244, 50)
        Me.button_Read_BMP.TabIndex = 2
        Me.button_Read_BMP.Text = "Read BMP"
        Me.button_Read_BMP.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("新細明體", 14.0!)
        Me.Button1.Location = New System.Drawing.Point(918, 82)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(244, 51)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "噴寫頭模擬"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ColorCheckBox
        '
        Me.ColorCheckBox.AutoSize = True
        Me.ColorCheckBox.Location = New System.Drawing.Point(1086, 230)
        Me.ColorCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorCheckBox.Name = "ColorCheckBox"
        Me.ColorCheckBox.Size = New System.Drawing.Size(72, 22)
        Me.ColorCheckBox.TabIndex = 4
        Me.ColorCheckBox.Text = "Color"
        Me.ColorCheckBox.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("新細明體", 14.0!)
        Me.Button2.Location = New System.Drawing.Point(918, 156)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(244, 51)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "顯示圖片"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1192, 675)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ColorCheckBox)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.button_Read_BMP)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.RichTextBox2)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form3"
        Me.Text = "Form3"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents button_Read_BMP As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ColorCheckBox As CheckBox
    Friend WithEvents Button2 As Button
End Class
