Imports System.IO
Imports System.Drawing
Imports System.Text
Imports System.Drawing.Drawing2D
Imports System.Threading


'Imports System
'Imports System.Runtime.InteropServices

Public Class MainForm
    Private Const V As Integer = &H1234
    Public INFINITE = &HFFFFFFFFUI
    Public EXT_IO_TRIG = &HAA      'external IO trigger 
    Public SPI_READ_TRIG = &HB0    'SPI  read trigger only use at SPI as slaver
    Public SPI_WRITE_TRIG = &HB1   'SPI  write trigger only use at SPI as slaver

    Dim WriteBuff_to_flash(4096) As Byte
    Dim addr_len, flash_total_addr As Integer

    Dim maxsize As Integer

    'Public Shared WriteBuff_to_flash(4096) As Byte

    Private hexdata, path, programdata As String

    Dim byDeviceId As Byte
    Dim serialNo As String
    Dim spiCfg As Byte
    Dim timeoutRW As Long
    Dim workMode As Byte
    Dim readCnt, WriteCnt As UInteger
    'Private data(2048, 44) As UInteger
    'Private data(2048, 50) As Byte
    Dim size As Integer
    Dim image_size As Integer

    Dim Image_Height As Integer
    Dim Image_Width As Integer

    ' Dim runThread As Thread
    '  Private Sub readTrig()
    'Dim msg As UInteger
    '     While True
    '        If (USBIO_ReadTrig(byDeviceId, msg, INFINITE)) Then
    '            handleTrig(msg)
    '       End If
    '    End While
    ' End Sub


    Private Sub MainForm_Loadd(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 設置所有 TextBox 的預設值為 1
        TextBox1.Text = "1"
        TextBox2.Text = "1"
        TextBox3.Text = "1"
        TextBox4.Text = "1"
        TextBox5.Text = "1"
        TextBox6.Text = "1"
        TextBox7.Text = "1"
        TextBox8.Text = "1"
        TextBox9.Text = "1"
        TextBox10.Text = "250"
        TextBox11.Text = "250"
    End Sub

    Private Sub handleTrig(ByVal msg As UInteger)
        Dim length As Byte
        Dim flag As Byte
        length = (msg / &H80) And &HFF
        flag = msg / &HFF
        Select Case flag
            Case SPI_WRITE_TRIG
                receData(length)
            Case EXT_IO_TRIG
                IOTrigged()
            Case Else
        End Select
    End Sub

    Private Sub receData(ByVal length As Byte)
        Dim readBuff() As Byte
        Dim lpPara(2) As Byte
        Dim str As String
        ReDim readBuff(length)
        str = ""
        If (USBIO_SPIRead(byDeviceId, lpPara, 0, readBuff, length)) Then
            ' Val2Str(str, readBuff, length, RichTextBox3.Text.Length Mod 50)
            ' RichTextBox3.Text = RichTextBox3.Text + str
            readCnt = readCnt + length
            InitControl()
        End If
    End Sub

    Private Sub IOTrigged()

    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        byDeviceId = 255
        serialNo = "??????????"
        spiCfg = 4
        timeoutRW = &HC800C8
        readCnt = 0
        WriteCnt = 0
        InitControl()
        USBIO_SetUSBNotify(False, AddressOf USB_Event)






        maxsize = 200


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'connect reset
        If (byDeviceId = 255) Then
            byDeviceId = USBIO_OpenDevice()
            If (byDeviceId = 255) Then
                MsgBox("No found usb2ish device!")
            Else

                USBIO_GetSerialNo(byDeviceId, serialNo)

                USBIO_SPIGetConfig(byDeviceId, spiCfg, timeoutRW)
                USBIO_GetWorkMode(byDeviceId, workMode)
                USBIO_SetTrigNotify(byDeviceId, AddressOf Trig_Event)
                'runThread = New Thread(AddressOf readTrig)
                ' runThread.Start()

            End If
        Else
            CloseDevice()
        End If
        InitControl()
        'RichTextBox2.AppendText(0.ToString("X2"))
        'RichTextBox2.AppendText(0.ToString("X2"))

    End Sub

    Public Sub InitControl()
        Dim tmpstr As String
        tmpstr = Format("%d", Int(timeoutRW / &H10000))

        If (byDeviceId = 255) Then
            Button1.Text = "連接 USB-SPI 轉接板"
            sFormTitle = "SPI圖片傳送系統"
            Me.Text = sFormTitle
        Else
            Button1.Text = "取消連接"
            sFormTitle = "SPI肚块╰参::" + serialNo
            Me.Text = sFormTitle
        End If

        '1020, 620
        Me.Width = 1020
        Me.Height = 420


    End Sub

    Private Function IsHex(ByVal str As String) As Boolean
        Dim length As Short
        Dim i As Short
        length = str.Length()
        If (length = 0) Then
            Return True
        End If
        If (length Mod 2 Or length > 256) Then
            Return False
        End If
        For i = 0 To length - 1
            If ((str(i) >= "0") And (str(i) <= "9")) Then
                Continue For
            ElseIf ((str(i) >= "a") And (str(i) <= "f")) Then
                Continue For
            ElseIf ((str(i) >= "A") And (str(i) <= "F")) Then
                Continue For
            Else
                Return False
            End If
        Next i
        Return True
    End Function

    Private Function IsDigit(ByVal str As String) As Boolean
        Dim length As Short
        Dim i As Short
        length = str.Length()
        If (length = 0) Then
            Return False
        End If
        For i = 0 To length - 1
            If ((str(i) >= "0") And (str(i) <= "9")) Then
                Continue For
            Else
                Return False
            End If
        Next i
        Return True
    End Function

    Private Function BcdToChar(ByVal iBcd As Byte) As Char
        Dim hexVar As String = "0123456789ABCDEF"
        Return hexVar(iBcd)
    End Function

    Private Function CharToBcd(ByVal ch As Char) As Byte
        Dim mBCD As Byte
        If ch >= "0" And ch <= "9" Then
            mBCD = AscW(ch) - AscW("0")
        ElseIf ch >= "a" And ch <= "f" Then
            mBCD = AscW(ch) - AscW("f") + 10
        ElseIf ch >= "A" And ch <= "F" Then
            mBCD = AscW(ch) - AscW("A") + 10
        End If
        Return (mBCD)
    End Function

    Private Sub Str2Val(ByRef bytes() As Byte, ByVal str As String)
        Dim length As UInteger
        Dim j, i As UInteger
        Dim chH, chL As Char
        length = str.Length()
        j = 0
        i = 0
        While (j < length)
            chH = str(j)
            If (chH = Chr(32) Or chH = Chr(13) Or chH = Chr(10)) Then
                j = j + 1
            Else
                j = j + 1
                chL = str(j)
                bytes(i) = (CharToBcd(chH) * 16 + CharToBcd(chL))
                j = j + 1
                i = i + 1
            End If

        End While

    End Sub

    'Val2Str(str, readBuff, length, richTextBox3.Text.Length Mod 50)
    Private Sub Val2Str(ByRef str As String, ByVal bytes() As Byte, ByVal len As UInteger, ByVal start As Byte)
        Dim i As UInteger
        Dim j As Byte
        j = start
        For i = 0 To len - 1
            str = str.Insert(str.Length, Chr(32))
            str = str.Insert(str.Length, bytes(i).ToString("X2"))
            j = j + 3
            If (j >= 48) Then
                str = str.Insert(str.Length, Chr(13))
                str = str.Insert(str.Length, Chr(10))
                j = 0
            End If
        Next

    End Sub

    Function HexToString(ByVal hex As String) As String
        Dim text As New System.Text.StringBuilder(hex.Length \ 2)
        For i As Integer = 0 To hex.Length - 2 Step 2
            text.Append(Chr(Convert.ToByte(hex.Substring(i, 2), 16)))
        Next
        Return text.ToString
    End Function





    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub MainForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        CloseDevice()
    End Sub

    Private Sub CloseDevice()
        If (byDeviceId <> 255) Then
            If (spiCfg And &H80) Then  'at slave mode ,first exit
                spiCfg = spiCfg And &H7F
                USBIO_SPISetConfig(byDeviceId, spiCfg, timeoutRW)
            End If
            'runThread.Abort()
            USBIO_CloseDevice(byDeviceId)
            byDeviceId = 255
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_USB_STATUS Then
            If (m.WParam = byDeviceId And m.LParam <> &H80) Then
                USBIO_CloseDevice(byDeviceId)
                byDeviceId = 255
                InitControl()
            End If
        End If
        If m.Msg = WM_TRIG_STATUS Then
            If (m.WParam = byDeviceId) Then
                handleTrig(m.LParam)
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Public Function USB_Removed(ByVal byIndex As Byte, ByVal dwUSBStatus As UInteger) As Boolean
        If (byIndex = byDeviceId And dwUSBStatus <> &H80) Then
            USBIO_CloseDevice(byDeviceId)
            byDeviceId = 255
            InitControl()
        End If
        Return True
    End Function

    Private Sub For_test_Click(sender As Object, e As EventArgs)

        Dim FileNum As Integer
        Dim strTemp As String

        FileNum = FreeFile()
        FileOpen(FileNum, ".\Test.txt", OpenMode.Output)

        strTemp = "ABCDEFRGHH" + vbCr
        PrintLine(FileNum, strTemp)

        strTemp = "asdbjwdh qwduiqwfdi qwdui" + vbCr
        PrintLine(FileNum, strTemp)

        FileClose(FileNum)

        MsgBox("Translate Hex to Verilog HDL.")

        'OpenFileDialog1.Filter = "HEX files (*.Hex)|*.hex"
        '   If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
        'fileReader = My.Computer.FileSystem.OpenTextFileReader(OpenFileDialog1.FileName)
        'InitControl()
        'fileReader.Close()
        'End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub EXE_1_Click(sender As Object, e As EventArgs)
        Dim command(256) As Byte
        Dim WriteBuff(4) As Byte
        Dim value, value_temp As UInt32
        'Dim value As Integer
        Dim readBuff(2048) As Byte

        Dim temp As Byte

        If (byDeviceId = 255) Then    'disconnected
            MsgBox("Please connect the device")
            Return
        End If
        If (workMode = 1) Then    'upgrade mode
            MsgBox("Please check jumpper and make sure it in normal mode")
            Return
        End If
        If (spiCfg And &H80) Then    'slaver mode
            MsgBox("SPI work as slaver")
            Return
        End If


        If (WriteCnt >= 65534) Then
            MsgBox("Write length must less than 65535")
            Return
        End If


        ''  address   DATA Low  DATA High
        ''  16bits   16bits     16bits
        ''  addr      DL        DH
        ''
        ''
        Dim addr As UInt16

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Dim command(16) As Byte
        Dim WriteBuff(8192) As Byte
        'Dim value As UInt32
        Dim i, j As Integer
        'Dim readBuff(2048) As Byte

        Dim count As Byte

        If (byDeviceId = 255) Then    'disconnected
            MsgBox("Please connect the device")
            Return
        End If
        If (workMode = 1) Then    'upgrade mode
            MsgBox("Please check jumpper and make sure it in normal mode")
            Return
        End If
        If (spiCfg And &H80) Then    'slaver mode
            MsgBox("SPI work as slaver")
            Return
        End If

        If (WriteCnt >= 65534) Then
            MsgBox("Write length must less than 65535")
            Return
        End If


        count = 0
        For j = 0 To 8191
            WriteBuff(j) = count
            ' command(j) = count
            If (count = 255) Then
                count = 0
            Else
                count = count + 1
            End If


        Next


        command(0) = &H55
        command(1) = &HAA


        USBIO_SPIWrite(byDeviceId, command, 2, WriteBuff, 8192)


    End Sub

    Private Sub CheckBox_NIOS_CheckedChanged(sender As Object, e As EventArgs)


    End Sub












    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click
    '    '48>32bits
    '    Dim command(256) As Byte
    '    Dim WriteBuff(256) As Byte
    '    Dim value, value_temp As UInt16

    '    'Dim value As Integer
    '    Dim readBuff(2048) As Byte

    '    Dim temp As Byte

    '    If (byDeviceId = 255) Then    'disconnected
    '        MsgBox("Please connect the device")
    '        Return
    '    End If

    '    If (workMode = 1) Then    'upgrade mode
    '        MsgBox("Please check jumpper and make sure it in normal mode")
    '        Return
    '    End If
    '    If (spiCfg And &H80) Then    'slaver mode
    '        MsgBox("SPI work as slaver")
    '        Return
    '    End If

    '    If (WriteCnt >= 65534) Then
    '        MsgBox("Write length must less than 65535")
    '        Return
    '    End If






    '    ''  address   DATA Low  DATA High
    '    ''  16bits   16bits     16bits
    '    ''  addr      DL        DH
    '    ''
    '    ''


    '    'Dim Reader(2048) As Byte

    '    'Reader = My.Computer.FileSystem.ReadAllBytes("C:\Users\user\Desktop\SPI_RW\test.txt")


    '    Dim fileReader As System.IO.StreamReader
    '    fileReader = My.Computer.FileSystem.OpenTextFileReader(".\read_write_command_file\send_command.txt")
    '    Dim stringReader As String

    '    stringReader = fileReader.ReadLine()

    '    Dim adder As Integer
    '    'Write-------------------------------------------------------------------------------------------------------

    '    While (stringReader IsNot Nothing) '48>32
    '        Dim Reader() As Byte = System.Text.Encoding.UTF8.GetBytes(stringReader)
    '        For index As Integer = 0 To 3
    '            If (Reader(2 * index) > 64) Then
    '                Reader(2 * index) = Reader(2 * index) - 87
    '            Else
    '                Reader(2 * index) = Reader(2 * index) - 48
    '            End If
    '            If (Reader(2 * index + 1) > 64) Then
    '                Reader(2 * index + 1) = Reader(2 * index + 1) - 87
    '            Else
    '                Reader(2 * index + 1) = Reader(2 * index + 1) - 48
    '            End If

    '            WriteBuff(index) = Reader(2 * index) * 16 + Reader(2 * index + 1)
    '        Next
    '        USBIO_SPITest(byDeviceId, WriteBuff, readBuff, 4)

    '        If (WriteBuff(1) >= 8) Then
    '            For adder = 2 To 3
    '                RichTextBox3.AppendText(readBuff(adder).ToString("X2"))
    '            Next
    '            RichTextBox3.AppendText(vbNewLine)
    '        End If


    '        stringReader = fileReader.ReadLine()

    '    End While
    '    ' Close the file reader
    '    fileReader.Close()

    '    ' Save the content of RichTextBox3 to result.txt
    '    Dim resultFilePath As String = Application.StartupPath & "\result.txt"
    '    Try
    '        System.IO.File.WriteAllText(resultFilePath, RichTextBox3.Text)
    '        MessageBox.Show("Content has been saved to " & resultFilePath)
    '    Catch ex As Exception
    '        MessageBox.Show("Error saving file: " & ex.Message)
    '    End Try
    'End Sub

    Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click
        Dim WriteBuff(256000) As Byte
        Dim readBuff(204800) As Byte

        ' 檢查設備狀態
        If byDeviceId = 255 Then
            MsgBox("Please connect the device")
            Return
        End If

        If workMode = 1 Then
            MsgBox("Please check jumper and make sure it is in normal mode")
            Return
        End If

        If (spiCfg And &H80) <> 0 Then
            MsgBox("SPI works as a slave")
            Return
        End If

        If WriteCnt >= 65534 Then
            MsgBox("Write length must be less than 65535")
            Return
        End If

        ' 讀取和處理文件
        Dim filePath As String = ".\read_write_command_file\send_command.txt"
        If Not System.IO.File.Exists(filePath) Then
            MsgBox("Command file not found.")
            Return
        End If

        Dim fileReader As System.IO.StreamReader = Nothing
        Try
            fileReader = New System.IO.StreamReader(filePath)
            Dim stringReader As String = fileReader.ReadLine()

            While stringReader IsNot Nothing
                Dim commandBytes As Byte() = HexStringToByteArray(stringReader.Trim())

                ' 判斷指令長度並填充 WriteBuff
                If commandBytes.Length <= WriteBuff.Length Then
                    Array.Copy(commandBytes, WriteBuff, commandBytes.Length)
                    USBIO_SPITest(byDeviceId, WriteBuff, readBuff, commandBytes.Length)

                    ' 處理結果
                    If WriteBuff.Length >= 8 Then
                        For adder As Integer = 2 To 3
                            RichTextBox3.AppendText(readBuff(adder).ToString("X2"))
                        Next
                        RichTextBox3.AppendText(vbNewLine)
                    End If
                Else
                    MsgBox("Command length exceeds buffer size.")
                End If

                stringReader = fileReader.ReadLine()
            End While

        Catch ex As Exception
            MsgBox("Error reading file: " & ex.Message)
        Finally
            If fileReader IsNot Nothing Then fileReader.Close()
        End Try

        ' 保存結果到 result.txt
        Dim resultFilePath As String = System.IO.Path.Combine(Application.StartupPath, "result.txt")
        Try
            System.IO.File.WriteAllText(resultFilePath, RichTextBox3.Text)
            MessageBox.Show("Content has been saved to " & resultFilePath)
        Catch ex As Exception
            MessageBox.Show("Error saving file: " & ex.Message)
        End Try
    End Sub

    ' 將十六進制字串轉換為字節數組
    Private Function HexStringToByteArray(hex As String) As Byte()
        Dim numOfChars As Integer = hex.Length
        Dim bytes(numOfChars / 2 - 1) As Byte
        For i As Integer = 0 To numOfChars - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function

    Private Sub RichTextBox2_TextChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RichTextBox3.Clear()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub RichTextBox3_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox3.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' 創建並配置 OpenFileDialog
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png"
            openFileDialog.Title = "Select an Image File"

            ' 顯示對話框
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' 取得選擇的檔案路徑
                Dim imagePath As String = openFileDialog.FileName
                ' 定義輸出檔案路徑，將檔案儲存在當前應用程序資料夾中
                Dim directoryPath As String = Application.StartupPath & "\read_write_command_file"
                Dim outputPath As String = directoryPath & "\send_command.txt"

                Try
                    ' 確保輸出資料夾存在
                    If Not Directory.Exists(directoryPath) Then
                        Directory.CreateDirectory(directoryPath)
                    End If

                    ' 讀取圖片檔案
                    Dim image As Bitmap = New Bitmap(imagePath)

                    ' 取得 TextBox10 的數值，作為每行顯示的十六進制數字數量
                    Dim numbersPerLine As Integer
                    If Integer.TryParse(TextBox10.Text, numbersPerLine) Then
                        ' 將圖片轉換為十六進制格式，根據 TextBox10 的值換行
                        Dim hexData As String = ImageToHexValue(image, numbersPerLine)

                        ' 取得 TextBox 中的數字並轉換為四位數十六進制
                        Dim hexNumbers As New StringBuilder()
                        Dim textBoxes As TextBox() = {TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9, TextBox10, TextBox11}

                        For i As Integer = 0 To textBoxes.Length - 1
                            Dim textBox As TextBox = textBoxes(i)
                            Dim inputText As String = textBox.Text
                            If IsNumeric(inputText) Then
                                Dim number As Integer = Convert.ToInt32(inputText)
                                ' 編號從 01 開始，轉換為四位數十六進制
                                Dim identifier As String = (i).ToString("X2") & "00"
                                hexNumbers.AppendLine(identifier & number.ToString("X4"))
                            Else
                                ' 顯示數字輸入錯誤的消息
                                MessageBox.Show("請輸入有效的數字。")
                                Return
                            End If
                        Next

                        ' 準備要寫入的文本
                        Dim fileContent As New StringBuilder()
                        fileContent.AppendFormat(hexNumbers.ToString())
                        fileContent.AppendFormat(hexData)

                        ' 將文本寫入檔案
                        File.WriteAllText(outputPath, fileContent.ToString())

                        ' 顯示成功消息
                        MessageBox.Show("圖片和數字已成功儲存到 " & outputPath)

                    Else
                        ' 顯示 TextBox10 無效數字的消息
                        MessageBox.Show("請輸入有效的數字在 TextBox10。")
                    End If

                Catch ex As Exception
                    ' 顯示錯誤消息
                    MessageBox.Show("發生錯誤: " & ex.Message)
                End Try
            End If
        End Using
    End Sub



    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    ' 將圖片轉換為十六進制格式的輔助方法
    Private Function ImageToHexValue(image As Bitmap, numbersPerLine As Integer) As String
        Dim hexString As New StringBuilder()
        Dim hexLine As New StringBuilder()

        Dim pixelCount As Integer = 0
        For y As Integer = 0 To image.Height - 1
            For x As Integer = 0 To image.Width - 1
                If pixelCount = 0 Then
                    hexString.AppendFormat("8500")
                End If

                Dim pixelColor As Color = image.GetPixel(x, y)
                Dim singleValue As Integer = CInt(Math.Round(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B))
                ' 將數字轉換為四位數的十六進制字符串
                Dim hexValue As String = singleValue.ToString("X4")
                hexLine.AppendFormat(hexValue)
                pixelCount += 1

                ' 每 numbersPerLine 個數字換行
                If pixelCount >= numbersPerLine Then
                    hexString.AppendLine(hexLine.ToString().TrimEnd())
                    hexLine.Clear()
                    pixelCount = 0
                End If
            Next
        Next

        ' 添加最後一行（如果有剩餘數據）
        If hexLine.Length > 0 Then
            hexString.AppendLine(hexLine.ToString().TrimEnd())
            hexString.AppendLine("81000000")
            hexString.AppendLine("8480")
            hexString.AppendLine("83000000")
            hexString.AppendLine("8480")
        End If

        Return hexString.ToString()
    End Function
    Private Sub RichTextBox1_TextChanged_1(sender As Object, e As EventArgs)

    End Sub


    Private Sub GroupBox9_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>


End Class