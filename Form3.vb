Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.IO

Public Class Form3
    'arr arr2 兩個set1 set2
    Public arr(51, 41) As Integer '2d-Array with y-axis: Address select line(A), x-axis: Primitive select line(P).(Start from 1. ex. arr[1][1])
    Public arr2(51, 41) As Integer '2d-Array with y-axis: Address select line(A), x-axis: Primitive select line(P).(Start from 1. ex. arr2[1][1])
    Public NAPList(1000, 3) As Integer 'Three columns for  Address select line(A),  Primitive select line(P), and Inkjet select (offset). (Start from 1. exNAPList[1][0]=>第1點的A, NAPList[1][1]=>第1點的P, NAPList[1][2]=>第1點的offset)
    Public nozzleNum As Integer  'the number of the nozzle numbers
    Public addrNum As Integer 'the number of the address lines
    Public primNum As Integer 'the number of the primitive lines
    Public inkjetNum As Integer 'the number of inkjet
    Public bmpNo As Integer = 0 'record the number of the result.bmp
    Public stype As Integer = 0 '存standard type
    Private Sub read_standard(ByVal standard_type As Integer)
        RichTextBox2.Text = ""
        Dim i, j As Integer
        Dim fileName As String = "BRYCE_standard.txt"

        If standard_type = 1 Then
            fileName = "BISCAYNE_standard.txt"
        ElseIf standard_type = 2 Then
            fileName = "HP_standard.txt"
        ElseIf standard_type = 3 Then
            fileName = "IUT_standard.txt"
        End If

        'Initialize Address select line & Primitive line 2D-Array 
        For i = 1 To 50
            For j = 1 To 40
                arr(i, j) = -1
            Next
        Next

        RichTextBox2.AppendText("Initialize Address select line & Primitive line 2D-Array successfully." + vbCrLf)


        Dim dialog As New OpenFileDialog
        dialog.InitialDirectory = ".\"
        Dim paths As String = My.Computer.FileSystem.ReadAllText(fileName)

        '讀取第一列 : Nozzle數、Address select line數、Primitive select line數、pinterType
        Dim firstLine As String = ""
        i = 0
        While paths(i) <> vbLf
            firstLine += paths(i)
            i += 1
        End While
        i += 1 'jump '\n'

        'napt[0]=nozzleNum, napt[1]=addrNum, napt[2]=primNum, napt[3]=inkjetNum
        Dim napt As String() = firstLine.Split(" "c)
        nozzleNum = Int32.Parse(napt(0))    'HP-300 總共幾點
        addrNum = Int32.Parse(napt(1))      'HP-22  address數
        primNum = Int32.Parse(napt(2))      'HP-7   pdata一個set數
        inkjetNum = Int32.Parse(napt(3))    'HP-2?
        RichTextBox2.AppendText("nozzleNum: " + nozzleNum.ToString + ", addrNum:" + addrNum.ToString + ", primNum: " + primNum.ToString + ", inkjetNum: " + inkjetNum.ToString + vbCrLf)

        '略過座標列
        While paths(i) <> vbLf
            i += 1
        End While
        i += 1  'jump '\n'

        '讀取表格資料 - 第一個噴寫頭
        For addressIndex As Integer = 0 To addrNum - 1
            Dim s As String = ""

            '吃一整行資料
            While paths(i) <> vbLf  '換行
                s += paths(i)
                i += 1
            End While
            i += 1 'jump '\n'

            Dim tmp As String() = s.Split(CChar(vbTab), vbCr)

            'set1 - offset為0
            For primIndex As Integer = 1 To primNum 'HP-7
                'Build the convert table.
                '-1表沒有點
                If Int32.Parse(tmp(primIndex)) = -1 Then Continue For
                'NAPList用來查表
                'ex: 45號 NAPList(45)=> 放在a1的第二個col的set1
                NAPList(Int32.Parse(tmp(primIndex)), 0) = addressIndex + 1 ' the nozzle number's address line
                NAPList(Int32.Parse(tmp(primIndex)), 1) = primIndex 'the nozzle number's primitive line
                NAPList(Int32.Parse(tmp(primIndex)), 2) = 0 'the nozzle number's inkjet offset (first inkjet)

                'Build the mapping table.
                'standard本身
                arr(addressIndex + 1, primIndex) = Int32.Parse(tmp(primIndex)) '(first inkjet)
            Next
        Next

        If inkjetNum > 1 Then
            While paths(i) <> vbLf
                i += 1
            End While
            i += 1  'jump '\n'
            '略過座標列
            While paths(i) <> vbLf
                i += 1
            End While
            i += 1  'jump '\n'

            '讀取表格資料 - 第二個噴寫頭
            For addressIndex As Integer = 0 To addrNum - 1
                Dim s As String = ""

                While paths(i) <> vbLf
                    s += paths(i)
                    i += 1
                End While

                i += 1
                Dim tmp As String() = s.Split(CChar(vbTab), vbCr)

                'set2 - offset為primNum
                For primIndex As Integer = 1 To primNum
                    If Int32.Parse(tmp(primIndex)) = -1 Then Continue For
                    NAPList(Int32.Parse(tmp(primIndex)), 0) = addressIndex + 1 'the nozzle number's address line
                    NAPList(Int32.Parse(tmp(primIndex)), 1) = primIndex ' the nozzle number's primitive line
                    NAPList(Int32.Parse(tmp(primIndex)), 2) = primNum ' the nozzle number's inkjet offset (second inkjet)

                    'Build the mapping table.
                    arr(addressIndex + 1, primIndex + primNum) = Int32.Parse(tmp(primIndex)) '(second inkjet)
                Next
            Next
        End If

        RichTextBox2.AppendText("Build the tables successfully!" + vbCrLf)

        'Show in the richTextBox1
        'For index As Integer = -1 To primNum * 2 - 1
        '    RichTextBox1.AppendText((index + 1) & vbTab)
        'Next

        'RichTextBox1.AppendText(vbLf)

        'For index As Integer = 1 To addrNum
        '    RichTextBox1.AppendText(index & vbTab)

        '    For j = 1 To primNum * 2
        '        RichTextBox1.AppendText(arr(index, j) & vbTab)
        '    Next

        '    RichTextBox1.AppendText(vbLf)
        'Next
        RichTextBox2.ScrollToCaret()
    End Sub

    Private Sub reset_arr()
        '把arr清空，讀下一個stepcol
        Array.Clear(arr, 0, arr.Length)
        Array.Clear(arr2, 0, arr2.Length)
    End Sub

    Private Sub fill_arr(ByVal AData As Integer, ByVal P1 As String, ByVal P2 As String)
        Dim addressIndex As Integer = AData
        Dim strIndex As Integer = 32

        For primIndex As Integer = 1 To primNum 'HP-7 (+1因為從1開始放) 

            If stype! = 2 Then
                'p7放最後一個數
                arr(addressIndex, (primNum + 1) - primIndex) = Convert.ToInt32(P1.Substring(strIndex - primIndex, 1))   'p1
                arr2(addressIndex, (primNum + 1) - primIndex) = Convert.ToInt32(P2.Substring(strIndex - primIndex, 1))  'p2
            Else        '我加的 HP的pdata最後一位數是p1
                arr(addressIndex, primIndex) = Convert.ToInt32(P1.Substring(strIndex - primIndex, 1))   'p1
                arr2(addressIndex, primIndex) = Convert.ToInt32(P2.Substring(strIndex - primIndex, 1))  'p2
            End If

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog1 As OpenFileDialog = New OpenFileDialog()
        '選擇print data檔案
        dialog1.Title = "Select print data file"
        dialog1.InitialDirectory = ".\"
        dialog1.Filter = "Text documents (.txt)| *.txt"
        Dim PData1 As Queue(Of String) = New Queue(Of String)()
        Dim PData2 As Queue(Of String) = New Queue(Of String)()
        Dim stepCol As Integer = 0
        Dim type As Integer = 0
        Dim upDown As Integer = 0
        Dim pic_width As Integer = 0
        RichTextBox2.Text = ""
        bmpNo += 1

        If dialog1.ShowDialog() = DialogResult.OK Then
            RichTextBox2.AppendText("Open print_data successfully." & vbLf)
            Dim filename As String = Path.GetDirectoryName(dialog1.FileName) & "\" + Path.GetFileName(dialog1.FileName)
            'path是讀進來的print_data
            Dim paths As String = System.IO.File.ReadAllText(filename)
            Dim i As Integer
            Dim start As Integer = 0
            Dim stepSeg As Integer = -1

            '取第1列 : stepCol
            Dim firstLine As String = ""
            i = 0
            While paths(i) <> vbLf
                firstLine += paths(i)
                i += 1
            End While
            i += 1
            stepCol = Int32.Parse(firstLine)
            RichTextBox2.AppendText("stepCol = " & stepCol & vbLf)

            '取第2列 : type(哪種廠牌)
            firstLine = ""
            While paths(i) <> vbLf
                firstLine += paths(i)
                i += 1
            End While
            i += 1
            type = Int32.Parse(firstLine)
            '我加的
            stype = type
            If type = 0 Then
                RichTextBox2.AppendText("type = BRYCE" & vbLf)
                read_standard(0)
                stepSeg = 10
            ElseIf type = 1 Then
                RichTextBox2.AppendText("type = BISCAYNE" & vbLf)
                read_standard(1)
                stepSeg = 10
            ElseIf type = 2 Then
                RichTextBox2.AppendText("type = HP" & vbLf)
                read_standard(2)
                stepSeg = 22
            ElseIf type = 3 Then
                RichTextBox2.AppendText("type = IUT" & vbLf)
                read_standard(3)
                stepSeg = 24
            ElseIf type = 4 Then
                RichTextBox2.AppendText("type = SICPA" & vbLf)
            Else
                RichTextBox2.AppendText("Wrong Type!!" & vbLf)
            End If

            '取第3列 : upDown
            'up(1) - 左到右，down(0) - 右到左
            firstLine = ""
            While paths(i) <> vbLf
                firstLine += paths(i)
                i += 1
            End While
            i += 1
            upDown = Int32.Parse(firstLine)
            If upDown = 1 Then
                RichTextBox2.AppendText("upDown = up" & vbLf)
            ElseIf upDown = 0 Then
                RichTextBox2.AppendText("upDown = down" & vbLf)
            Else
                RichTextBox2.AppendText("Wrong Order!!" & vbLf)
            End If

            '當讀到問號，或是以讀滿stepCol就停
            Dim counter As Integer
            For counter = 0 To stepCol - 1
ReadAData:
                'HP的print_data一行是一個stepcol
                'HP的一個stepcol有300個點
                Dim col1 As String = ""     '存Pdata1
                Dim col2 As String = ""     '存Pdata2
                Dim cnt As Integer = 0
                If type = 1 Then 'Biscayne
                    start = 0
                End If
                reset_arr()     '清空arr陣列放下一個stepcol

                '讀到問號就不用再讀
                If paths(i) = "?"c Then
                    GoTo showPic
                End If

                'PData1 Odd Set1 
                While cnt < stepSeg 'HP-address 22
                    Dim addrHexToBin As String = ""
                    Dim hexToBin1 As String = ""
                    Dim hexToBin2 As String = ""
                    'AData
                    'ex: 00000016 => address: 16 * 1 + 6 = 22
                    For fetchCounter As Integer = 0 To 8 - 1
                        addrHexToBin += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ',' or '\n'
                    '從16進位轉成10進位
                    Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr

                    If AData > 0 And type <= 1 Then 'FUNAI(BRYCE/BISCAYNE)
                        If type = 1 Then 'Biscayne
                            cnt += 1
                        End If
                        start = 1
                    End If

                    'PData1
                    'ex: 00000024 => 00 0000 0010 0100 (HP是倒過來，所以14個點，第3和6點要噴)
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin1 += paths(i)
                        i += 1
                    Next
                    i += 1  'jump ','
                    'PData2
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin2 += paths(i)
                        i += 1 'jump ' '
                    Next
                    i += 1 'jump ',' or '\n'
                    Dim tmp1, tmp2 As Integer
                    If type = 1 Then 'Biscayne
                        If paths(i) = vbCr And start = 0 Then
                            Dim P As String = "00000000000000000000000000000000"
                            fill_arr(AData, P, P)
                            GoTo ChangeToLine
                        End If
                    End If
                    '16進位轉成10進位
                    tmp1 = Convert.ToInt32(hexToBin1, 16)   'p1
                    tmp2 = Convert.ToInt32(hexToBin2, 16)   'p2
                    If type = 3 Then 'IUT
                        For kk As Integer = 0 To 7 - 1
                            tmp1 = tmp1 * 2
                        Next
                    End If
                    '轉成2進位
                    Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                    Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                    If type = 1 And AData > 0 Then 'Biscayne
                        fill_arr(AData, P1, P2)
                    ElseIf type = 0 Or type >= 2 Then 'Bryce, HP, IUT
                        fill_arr(AData, P1, P2)
                        cnt += 1
                    End If
                End While

                If type > 0 And type < 4 Then 'Biscayne, HP, IUT
                    GoTo ChangeToLine
                End If


                While paths(i) <> vbLf
                    i += 1
                End While
                i += 1
                If type = 0 And start = 0 Then 'Bryce
                    GoTo ReadAData
                End If

                pic_width += 1
                If paths(i) = "?"c Then
                    GoTo showPic
                End If

                cnt = 0

                'PData2 Odd Set  ??
                While cnt < stepSeg
                    Dim addrHexToBin As String = ""
                    Dim hexToBin1 As String = ""
                    Dim hexToBin2 As String = ""
                    'AData
                    For fetchCounter As Integer = 0 To 8 - 1
                        addrHexToBin += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ',' or '\n'
                    Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                    'PData1
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin1 += paths(i)
                        i += 1
                    Next
                    i += 1  'jump ','
                    'PData2
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin2 += paths(i)
                        i += 1 'jump ' '
                    Next
                    i += 1 'jump ',' or '\n'
                    Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                    Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                    Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                    Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                    fill_arr(AData, P1, P2)
                    cnt += 1
                End While


                While paths(i) <> vbLf
                    i += 1
                End While
                i += 1
                pic_width += 1

                'Readcol1: '查表變一行
                For nozzleIndex As Integer = 1 To nozzleNum
                    Dim pixel_x As Integer = NAPList(nozzleIndex, 0)
                    Dim pixel_y As Integer = NAPList(nozzleIndex, 1)

                    'pdata1
                    Select Case arr(pixel_x, pixel_y)
                        Case 0
                            col1 += "0"
                        Case 1
                            col1 += "1"
                        Case Else
                            RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select

                    'pdata2
                    Select Case arr2(pixel_x, pixel_y)
                        Case 0
                            col2 += "0"
                        Case 1
                            col2 += "1"
                        Case Else
                            RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select

                Next
                PData1.Enqueue(col1)
                PData2.Enqueue(col2)

                If paths(i) = "?"c Then
                    GoTo showPic
                End If

                reset_arr()
                'Pdata1 Even Set
                cnt = 0
                While cnt < stepSeg
                    Dim addrHexToBin As String = ""
                    Dim hexToBin1 As String = ""
                    Dim hexToBin2 As String = ""
                    'AData
                    For fetchCounter As Integer = 0 To 8 - 1
                        addrHexToBin += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ',' or '\n'
                    Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                    'PData1
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin1 += paths(i)
                        i += 1
                    Next
                    i += 1  'jump ' '
                    'PData2
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin2 += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ' '
                    Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                    Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                    Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                    Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                    fill_arr(AData, P1, P2)
                    cnt += 1
                End While

                While paths(i) <> vbLf
                    i += 1
                End While
                i += 1 'jump '\n'
                pic_width += 1

                If paths(i) = "?"c Then
                    GoTo showPic
                End If

                'Even2:
                'Even Set2 
                cnt = 0
                While cnt < stepSeg
                    Dim addrHexToBin As String = ""
                    Dim hexToBin1 As String = ""
                    Dim hexToBin2 As String = ""
                    'AData
                    For fetchCounter As Integer = 0 To 8 - 1
                        addrHexToBin += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ',' or '\n'
                    Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                    'PData1
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin1 += paths(i)
                        i += 1
                    Next
                    i += 1  'jump ' '
                    'PData2
                    For fetchCounter As Integer = 0 To 8 - 1
                        hexToBin2 += paths(i)
                        i += 1
                    Next
                    i += 1 'jump ' '
                    Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                    Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                    Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                    Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                    fill_arr(AData, P1, P2)
                    cnt += 1
                End While

ChangeToLine:
                'arr印出來看

                '超過stepSeg的資料忽略
                While paths(i) <> vbLf
                    i += 1
                End While
                i += 1 'jump '\n'
                pic_width += 1

                'If type = 1 And paths(i) = "?"c Then 'Biscayne
                'GoTo showPic
                'End If

                'Bryce->Readcol2: '查表變一行 / Biscayne, IUT, HP->Readcol1: '查表變一行 
                col1 = ""
                col2 = ""

                For nozzleIndex As Integer = 1 To nozzleNum     'HP-300
                    Dim pixel_x As Integer = NAPList(nozzleIndex, 0)    '第一點的address
                    Dim pixel_y As Integer = NAPList(nozzleIndex, 1)    '第一點在p幾

                    'offset = 0
                    'arr
                    If type = 2 And NAPList(nozzleIndex, 2) = 0 Then 'HP pdata1
                        Select Case arr(pixel_x, pixel_y)
                            Case 0
                                col1 += "0"
                            Case 1
                                col1 += "1"
                            Case Else
                                RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                        End Select
                        '我改的
                    ElseIf type = 2 And NAPList(nozzleIndex, 2) = primNum Then 'HP pdata2
                        'ElseIf type = 2 And NAPList(nozzleIndex, 2) = 1 Then 'HP pdata2
                        Select Case arr2(pixel_x, pixel_y)
                            Case 0
                                col2 += "0"
                            Case 1
                                col2 += "1"
                            Case Else
                                RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                        End Select
                    Else 'FUNAI, IUT
                        Select Case arr(pixel_x, pixel_y) 'pdata1
                            Case 0
                                col1 += "0"
                            Case 1
                                col1 += "1"
                            Case Else
                                RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                        End Select

                        Select Case arr2(pixel_x, pixel_y) 'pdata2
                            Case 0
                                col2 += "0"
                            Case 1
                                col2 += "1"
                            Case Else
                                RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                        End Select
                    End If
                Next
                PData1.Enqueue(col1)
                PData2.Enqueue(col2)

                If paths(i) = "?"c Then
                    GoTo showPic
                End If
            Next

showPic:
            '轉成bmp，顯示並匯出bmp
            Dim PData1OddXCount As Integer = 0
            Dim PData1EvenXCount As Integer = 0
            Dim PData2OddXCount As Integer = 0
            Dim PData2EvenXCount As Integer = 0

            If type = 0 And upDown = 1 Then 'Bryce && 上數
                PData1OddXCount = 21
                PData1EvenXCount = 20
                PData2OddXCount = 2
                PData2EvenXCount = 1
            ElseIf type = 0 And upDown = 0 Then 'Bryce && 下數
                PData1OddXCount = 1
                PData1EvenXCount = 2
                PData2OddXCount = 20
                PData2EvenXCount = 21
            ElseIf type = 1 And upDown = 1 Then 'Biscayne && 上數
                PData1OddXCount = 7
                PData1EvenXCount = 7
                PData2OddXCount = 0
                PData2EvenXCount = 0
            ElseIf type = 1 And upDown = 0 Then 'Biscayne && 下數
                PData1OddXCount = 0
                PData1EvenXCount = 0
                PData2OddXCount = 7
                PData2EvenXCount = 7
            ElseIf type = 2 And upDown = 1 Then 'HP && 上數
                '因為有物理間距，所以se1和set2不會同時噴
                PData1OddXCount = 98
                PData1EvenXCount = 98
                PData2OddXCount = 1
                PData2EvenXCount = 1
            ElseIf type = 2 And upDown = 0 Then 'HP && 下數
                PData1OddXCount = 1
                PData1EvenXCount = 1
                PData2OddXCount = 98
                PData2EvenXCount = 98
            ElseIf type = 3 Then 'IUT
                PData1OddXCount = 0
                PData1EvenXCount = 0
                PData2OddXCount = 0
                PData2EvenXCount = 0
            End If

            Dim bmpData As Queue(Of String) = New Queue(Of String)()
            Dim width As Integer
            If type <= 1 Then 'FUNAI
                width = pic_width / 2 + 500
            Else 'HP, IUT
                width = stepCol
            End If
            Dim bmp As Bitmap = New Bitmap(width, nozzleNum + 1) 'default
            Dim outstr1 As String = "" 'PData1
            Dim outstr2 As String = "" 'PData2
            Dim NozzleCnt As Integer = nozzleNum
            If type = 2 Then 'HP
                NozzleCnt = nozzleNum / 2
            End If

            '我加的
            RichTextBox2.AppendText("PData1.Count : " & PData1.Count & "  PData2.Count : " & PData2.Count & vbLf)
            While PData1.Count > 0  'PData1
                Dim Xcount As Integer
                Dim Ycount As Integer
                outstr1 = PData1.Dequeue()
                'RichTextBox1.AppendText(outstr1 & vbLf)

                'Xcount Ycount??
                For nozzleIndex As Integer = 0 To NozzleCnt - 1 'HP-150
                    If type <= 1 Then 'FUNAI
                        Ycount = nozzleIndex
                    ElseIf type = 2 Then 'HP
                        Ycount = nozzleIndex * 2
                    ElseIf type = 3 Then 'IUT
                        Ycount = nozzleNum - nozzleIndex
                    End If
                    If nozzleIndex Mod 2 = 1 Then 'even (0-indexed)
                        Xcount = PData1EvenXCount   'HP-01
                    Else
                        Xcount = PData1OddXCount    'HP-01
                    End If

                    Select Case outstr1(nozzleIndex)
                        Case "0"c
                        Case "1"c
                            If ColorCheckBox.Checked Then  'Different Colors
                                'error
                                If nozzleIndex Mod 2 = 0 Then 'even
                                    bmp.SetPixel(Xcount, Ycount, Color.Black)
                                Else
                                    bmp.SetPixel(Xcount, Ycount, Color.LawnGreen)
                                End If
                            Else 'Same Color
                                bmp.SetPixel(Xcount, Ycount, Color.Black)
                            End If
                        Case Else
                            RichTextBox2.AppendText("nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select
                Next
                PData1OddXCount += 1
                PData1EvenXCount += 1
            End While

            'PData2
            While PData2.Count > 0
                Dim Xcount As Integer
                Dim Ycount As Integer
                outstr2 = PData2.Dequeue()
                'RichTextBox1.AppendText(ErrorCount + 1 & vbLf)
                'RichTextBox1.AppendText(outstr2 & vbLf)

                For nozzleIndex As Integer = 0 To NozzleCnt - 1
                    If type <= 1 Then 'FUNAI
                        Ycount = nozzleIndex
                    ElseIf type = 2 Then 'HP
                        Ycount = nozzleIndex * 2 + 1
                    ElseIf type = 3 Then 'IUT
                        Ycount = nozzleNum - nozzleIndex
                    End If
                    If nozzleIndex Mod 2 = 1 Then 'even (0-indexed)
                        Xcount = PData2EvenXCount
                    Else
                        Xcount = PData2OddXCount
                    End If

                    Select Case outstr2(nozzleIndex)
                        Case "0"c
                        Case "1"c
                            If ColorCheckBox.Checked Then  'Different Colors
                                If nozzleIndex Mod 2 = 0 Then 'even
                                    bmp.SetPixel(Xcount, Ycount, Color.Aqua) 'blue
                                Else
                                    bmp.SetPixel(Xcount, Ycount, Color.Red)
                                End If
                            Else 'Same Color
                                bmp.SetPixel(Xcount, Ycount, Color.Black)
                            End If
                        Case Else
                            RichTextBox2.AppendText("nozzle : (" & nozzleIndex & ") has a problem!" & vbLf)
                    End Select
                Next
                PData2OddXCount += 1
                PData2EvenXCount += 1
            End While

            bmp.Save(Path.GetDirectoryName(dialog1.FileName) + "\" + " result" + bmpNo.ToString + ".bmp")
            RichTextBox2.AppendText("The 'result" + bmpNo.ToString + ".bmp' is generated." + vbLf)
            PictureBox2.Image = Image.FromFile(Path.GetDirectoryName(dialog1.FileName) + "\" + " result" + bmpNo.ToString + ".bmp")
            RichTextBox2.ScrollToCaret()
        End If
    End Sub

    Private Sub button_Read_BMP_Click(sender As Object, e As EventArgs) Handles button_Read_BMP.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = ".\"
        OpenFileDialog.Filter = "BMP File| *.bmp"

        RichTextBox2.Text = ""

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim myBmp As New Bitmap(OpenFileDialog.FileName)

            Dim Height As Integer = myBmp.Height
            Dim Height_temp As Integer = myBmp.Height
            Dim Width As Integer = myBmp.Width
            Dim rgbData(Width + 100, Height + 400, 3) As Integer
            Dim pv(Width + 1, Height + 1) As Integer  'Pixel_value

            Dim Print_Head_Odd_Data(Width, 23) As Integer
            Dim Print_Head_Even_Data(Width, 23) As Integer

            Dim w As Double = PictureBox2.Width * 1.1F
            Dim h As Double = PictureBox2.Height * 1.1F

            'pictureBox2.Size = Size.Ceiling(New SizeF(w, h))

            PictureBox2.Image = CType(myBmp, Image)

            Dim filename_w As String = Path.ChangeExtension(OpenFileDialog.FileName, ".txt")
            Dim image_path As String = Path.GetDirectoryName(OpenFileDialog.FileName)
            Dim image_file_name As String = Path.GetFileNameWithoutExtension(OpenFileDialog.FileName)

            Dim filename_testbench_32_w As String = image_path + "\\" + image_file_name + "_testbench_32.txt"
            Dim filename_testbench_16_w As String = image_path + "\\" + image_file_name + "_testbench_16.txt"
            Dim filename_testbench_8_w As String = image_path + "\\" + image_file_name + "_testbench_8.txt"

            If Height <= 30 Then
                Height_temp = 32
            ElseIf Height <= 48 Then
                Height_temp = 48
            ElseIf Height <= 64 Then
                Height_temp = 64
            ElseIf Height <= 80 Then
                Height_temp = 80
            ElseIf Height <= 160 Then
                Height_temp = 160
            ElseIf Height <= 320 Then
                Height_temp = 320
            ElseIf Height <= 640 Then
                Height_temp = 640
            End If

            RichTextBox2.AppendText("讀取影像檔 : " + OpenFileDialog.FileName + vbCrLf)

            Dim line As String = ""
            Dim fs As FileStream

            Try
                fs = New FileStream(filename_w, FileMode.Create)
                Using writer As New StreamWriter(fs)
                    writer.Write(Width.ToString() + " " + Height_temp.ToString() + vbCrLf)

                    Dim x, y As Integer
                    For x = 0 To Width - 1
                        line = ""
                        For y = 0 To Height_temp - 1
                            Dim color As Color
                            If y < Height Then
                                color = myBmp.GetPixel(x, y)
                                rgbData(x, y, 0) = color.R
                                rgbData(x, y, 1) = color.G
                                rgbData(x, y, 2) = color.B
                            Else
                                rgbData(x, y, 0) = 200
                            End If
                            If rgbData(x, y, 0) > 128 Then
                                rgbData(x, y, 0) = 0
                            Else
                                rgbData(x, y, 0) = 1
                            End If

                            line = line + rgbData(x, y, 0).ToString()
                        Next
                        writer.Write(line + vbCrLf)
                    Next
                End Using
            Finally
                If fs IsNot Nothing Then
                    fs.Dispose()
                End If
            End Try
            RichTextBox2.AppendText("建立影像之文字檔 : " + filename_w + vbCrLf)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim fileReader As System.IO.StreamReader
        'fileReader = My.Computer.FileSystem.OpenTextFileReader(".\output_obuffer_file\print_data.txt")
        Dim dialog1 As OpenFileDialog = New OpenFileDialog()
        dialog1.Title = "Select print data file"
        dialog1.InitialDirectory = ".\output_obuffer_file"
        dialog1.Filter = "Text documents (.txt)| *.txt"
        dialog1.FileName = ".\output_obuffer_file\print_data.txt"

        Dim PData1 As Queue(Of String) = New Queue(Of String)()
        Dim PData2 As Queue(Of String) = New Queue(Of String)()
        Dim stepCol As Integer = 0
        Dim type As Integer = 0
        Dim upDown As Integer = 0
        Dim pic_width As Integer = 0
        RichTextBox2.Text = ""
        bmpNo += 1


        'If dialog1.ShowDialog() = DialogResult.OK Then
        RichTextBox2.AppendText("Open print_data successfully." & vbLf)
        Dim filename As String = Path.GetDirectoryName(dialog1.FileName) & "\" + Path.GetFileName(dialog1.FileName)
        Dim paths As String = System.IO.File.ReadAllText(filename)
        Dim i As Integer
        Dim start As Integer = 0
        Dim stepSeg As Integer = -1

        '取第1列 : stepCol
        Dim firstLine As String = ""
        i = 0
        While paths(i) <> vbLf
            firstLine += paths(i)
            i += 1
        End While
        i += 1
        stepCol = Int32.Parse(firstLine)
        RichTextBox2.AppendText("stepCol = " & stepCol & vbLf)

        '取第2列 : type
        firstLine = ""
        While paths(i) <> vbLf
            firstLine += paths(i)
            i += 1
        End While
        i += 1
        type = Int32.Parse(firstLine)
        If type = 0 Then
            RichTextBox2.AppendText("type = BRYCE" & vbLf)
            read_standard(0)
            stepSeg = 10
        ElseIf type = 1 Then
            RichTextBox2.AppendText("type = BISCAYNE" & vbLf)
            read_standard(1)
            stepSeg = 10
        ElseIf type = 2 Then
            RichTextBox2.AppendText("type = HP" & vbLf)
            read_standard(2)
            stepSeg = 22
        ElseIf type = 3 Then
            RichTextBox2.AppendText("type = IUT" & vbLf)
            read_standard(3)
            stepSeg = 24
        ElseIf type = 4 Then
            RichTextBox2.AppendText("type = SICPA" & vbLf)
        Else
            RichTextBox2.AppendText("Wrong Type!!" & vbLf)
        End If

        '取第3列 : upDown
        firstLine = ""
        While paths(i) <> vbLf
            firstLine += paths(i)
            i += 1
        End While
        i += 1
        upDown = Int32.Parse(firstLine)
        If upDown = 1 Then
            RichTextBox2.AppendText("upDown = up" & vbLf)
        ElseIf upDown = 0 Then
            RichTextBox2.AppendText("upDown = down" & vbLf)
        Else
            RichTextBox2.AppendText("Wrong Order!!" & vbLf)
        End If

        Dim counter As Integer
        For counter = 0 To stepCol - 1
ReadAData:
            Dim col1 As String = ""
            Dim col2 As String = ""
            Dim cnt As Integer = 0
            If type = 1 Then 'Biscayne
                start = 0
            End If
            reset_arr()

            If paths(i) = "?"c Then
                GoTo showPic
            End If

            'PData1 Odd Set1 
            While cnt < stepSeg
                Dim addrHexToBin As String = ""
                Dim hexToBin1 As String = ""
                Dim hexToBin2 As String = ""
                'AData
                For fetchCounter As Integer = 0 To 8 - 1
                    addrHexToBin += paths(i)
                    i += 1
                Next
                i += 1 'jump ',' or '\n'
                Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr

                If AData > 0 And type <= 1 Then 'FUNAI
                    If type = 1 Then 'Biscayne
                        cnt += 1
                    End If
                    start = 1
                End If

                'PData1
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin1 += paths(i)
                    i += 1
                Next
                i += 1  'jump ','
                'PData2
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin2 += paths(i)
                    i += 1 'jump ' '
                Next
                i += 1 'jump ',' or '\n'
                Dim tmp1, tmp2 As Integer
                If type = 1 Then 'Biscayne
                    If paths(i) = vbCr And start = 0 Then
                        Dim P As String = "00000000000000000000000000000000"
                        fill_arr(AData, P, P)
                        GoTo ChangeToLine
                    End If
                End If
                tmp1 = Convert.ToInt32(hexToBin1, 16)
                tmp2 = Convert.ToInt32(hexToBin2, 16)
                If type = 3 Then 'IUT
                    For kk As Integer = 0 To 7 - 1
                        tmp1 = tmp1 * 2
                    Next
                End If
                Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                If type = 1 And AData > 0 Then 'Biscayne
                    fill_arr(AData, P1, P2)
                ElseIf type = 0 Or type >= 2 Then 'Bryce, HP, IUT
                    fill_arr(AData, P1, P2)
                    cnt += 1
                End If
            End While

            If type > 0 And type < 4 Then 'Biscayne, HP, IUT
                GoTo ChangeToLine
            End If


            While paths(i) <> vbLf
                i += 1
            End While
            i += 1
            If type = 0 And start = 0 Then 'Bryce
                GoTo ReadAData
            End If

            pic_width += 1
            If paths(i) = "?"c Then
                GoTo showPic
            End If

            cnt = 0

            'PData2 Odd Set 
            While cnt < stepSeg
                Dim addrHexToBin As String = ""
                Dim hexToBin1 As String = ""
                Dim hexToBin2 As String = ""
                'AData
                For fetchCounter As Integer = 0 To 8 - 1
                    addrHexToBin += paths(i)
                    i += 1
                Next
                i += 1 'jump ',' or '\n'
                Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                'PData1
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin1 += paths(i)
                    i += 1
                Next
                i += 1  'jump ','
                'PData2
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin2 += paths(i)
                    i += 1 'jump ' '
                Next
                i += 1 'jump ',' or '\n'
                Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                fill_arr(AData, P1, P2)
                cnt += 1
            End While


            While paths(i) <> vbLf
                i += 1
            End While
            i += 1
            pic_width += 1

            'Readcol1: '查表變一行
            For nozzleIndex As Integer = 1 To nozzleNum
                Dim pixel_x As Integer = NAPList(nozzleIndex, 0)
                Dim pixel_y As Integer = NAPList(nozzleIndex, 1)

                'pdata1
                Select Case arr(pixel_x, pixel_y)
                    Case 0
                        col1 += "0"
                    Case 1
                        col1 += "1"
                    Case Else
                        RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                End Select

                'pdata2
                Select Case arr2(pixel_x, pixel_y)
                    Case 0
                        col2 += "0"
                    Case 1
                        col2 += "1"
                    Case Else
                        RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                End Select

            Next
            PData1.Enqueue(col1)
            PData2.Enqueue(col2)

            If paths(i) = "?"c Then
                GoTo showPic
            End If

            reset_arr()
            'Pdata1 Even Set
            cnt = 0
            While cnt < stepSeg
                Dim addrHexToBin As String = ""
                Dim hexToBin1 As String = ""
                Dim hexToBin2 As String = ""
                'AData
                For fetchCounter As Integer = 0 To 8 - 1
                    addrHexToBin += paths(i)
                    i += 1
                Next
                i += 1 'jump ',' or '\n'
                Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                'PData1
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin1 += paths(i)
                    i += 1
                Next
                i += 1  'jump ' '
                'PData2
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin2 += paths(i)
                    i += 1
                Next
                i += 1 'jump ' '
                Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                fill_arr(AData, P1, P2)
                cnt += 1
            End While

            While paths(i) <> vbLf
                i += 1
            End While
            i += 1 'jump '\n'
            pic_width += 1

            If paths(i) = "?"c Then
                GoTo showPic
            End If

            'Even2:
            'Even Set2 
            cnt = 0
            While cnt < stepSeg
                Dim addrHexToBin As String = ""
                Dim hexToBin1 As String = ""
                Dim hexToBin2 As String = ""
                'AData
                For fetchCounter As Integer = 0 To 8 - 1
                    addrHexToBin += paths(i)
                    i += 1
                Next
                i += 1 'jump ',' or '\n'
                Dim AData As Integer = Convert.ToInt32(addrHexToBin, 16) 'Addr
                'PData1
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin1 += paths(i)
                    i += 1
                Next
                i += 1  'jump ' '
                'PData2
                For fetchCounter As Integer = 0 To 8 - 1
                    hexToBin2 += paths(i)
                    i += 1
                Next
                i += 1 'jump ' '
                Dim tmp1 As Integer = Convert.ToInt32(hexToBin1, 16)
                Dim tmp2 As Integer = Convert.ToInt32(hexToBin2, 16)
                Dim P1 As String = Convert.ToString(tmp1, 2).PadLeft(32, "0"c)
                Dim P2 As String = Convert.ToString(tmp2, 2).PadLeft(32, "0"c)
                fill_arr(AData, P1, P2)
                cnt += 1
            End While

ChangeToLine:
            While paths(i) <> vbLf
                i += 1
            End While
            i += 1 'jump '\n'
            pic_width += 1

            'If type = 1 And paths(i) = "?"c Then 'Biscayne
            'GoTo showPic
            'End If

            'Bryce->Readcol2: '查表變一行 / Biscayne, IUT, HP->Readcol1: '查表變一行 
            col1 = ""
            col2 = ""

            For nozzleIndex As Integer = 1 To nozzleNum 'HP-300
                Dim pixel_x As Integer = NAPList(nozzleIndex, 0)    'address
                Dim pixel_y As Integer = NAPList(nozzleIndex, 1)    'p幾

                If type = 2 And NAPList(nozzleIndex, 2) = 0 Then 'HP pdata1
                    Select Case arr(pixel_x, pixel_y)
                        Case 0
                            col1 += "0"
                        Case 1
                            col1 += "1"
                        Case Else
                            RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select
                    '我改的
                ElseIf type = 2 And NAPList(nozzleIndex, 2) = primNum Then
                    'ElseIf type = 2 And NAPList(nozzleIndex, 2) = 1 Then 'HP pdata2
                    'NAPList(Int32.Parse(tmp(primIndex)), 2) = primNum??
                    Select Case arr2(pixel_x, pixel_y)
                        Case 0
                            col2 += "0"
                        Case 1
                            col2 += "1"
                        Case Else
                            RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select
                Else 'FUNAI, IUT
                    Select Case arr(pixel_x, pixel_y) 'pdata1
                        Case 0
                            col1 += "0"
                        Case 1
                            col1 += "1"
                        Case Else
                            RichTextBox2.AppendText("col1 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select

                    Select Case arr2(pixel_x, pixel_y) 'pdata2
                        Case 0
                            col2 += "0"
                        Case 1
                            col2 += "1"
                        Case Else
                            RichTextBox2.AppendText("col2 nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                    End Select
                End If
            Next
            PData1.Enqueue(col1)
            PData2.Enqueue(col2)

            If paths(i) = "?"c Then
                GoTo showPic
            End If
        Next

showPic:
        '轉成bmp，顯示並匯出bmp
        Dim PData1OddXCount As Integer = 0
        Dim PData1EvenXCount As Integer = 0
        Dim PData2OddXCount As Integer = 0
        Dim PData2EvenXCount As Integer = 0

        If type = 0 And upDown = 1 Then 'Bryce && 上數
            PData1OddXCount = 21
            PData1EvenXCount = 20
            PData2OddXCount = 2
            PData2EvenXCount = 1
        ElseIf type = 0 And upDown = 0 Then 'Bryce && 下數
            PData1OddXCount = 1
            PData1EvenXCount = 2
            PData2OddXCount = 20
            PData2EvenXCount = 21
        ElseIf type = 1 And upDown = 1 Then 'Biscayne && 上數
            PData1OddXCount = 7
            PData1EvenXCount = 7
            PData2OddXCount = 0
            PData2EvenXCount = 0
        ElseIf type = 1 And upDown = 0 Then 'Biscayne && 下數
            PData1OddXCount = 0
            PData1EvenXCount = 0
            PData2OddXCount = 7
            PData2EvenXCount = 7
        ElseIf type = 2 And upDown = 1 Then 'HP && 上數
            PData1OddXCount = 98
            PData1EvenXCount = 98
            PData2OddXCount = 1
            PData2EvenXCount = 1
        ElseIf type = 2 And upDown = 0 Then 'HP && 下數
            PData1OddXCount = 1
            PData1EvenXCount = 1
            PData2OddXCount = 98
            PData2EvenXCount = 98
        ElseIf type = 3 Then 'IUT
            PData1OddXCount = 0
            PData1EvenXCount = 0
            PData2OddXCount = 0
            PData2EvenXCount = 0
        End If

        Dim bmpData As Queue(Of String) = New Queue(Of String)()
        Dim width As Integer
        If type <= 1 Then 'FUNAI
            width = pic_width / 2 + 500
        Else 'HP, IUT
            width = stepCol
        End If
        Dim bmp As Bitmap = New Bitmap(width, nozzleNum + 1) 'default
        Dim outstr1 As String = "" 'PData1
        Dim outstr2 As String = "" 'PData2
        Dim NozzleCnt As Integer = nozzleNum
        If type = 2 Then 'HP
            NozzleCnt = nozzleNum / 2
        End If

        'PData1
        While PData1.Count > 0
            Dim Xcount As Integer
            Dim Ycount As Integer
            outstr1 = PData1.Dequeue()
            'RichTextBox1.AppendText(outstr1 & vbLf)

            For nozzleIndex As Integer = 0 To NozzleCnt - 1
                If type <= 1 Then 'FUNAI
                    Ycount = nozzleIndex
                ElseIf type = 2 Then 'HP
                    Ycount = nozzleIndex * 2
                ElseIf type = 3 Then 'IUT
                    Ycount = nozzleNum - nozzleIndex
                End If
                If nozzleIndex Mod 2 = 1 Then 'even (0-indexed)
                    Xcount = PData1EvenXCount
                Else
                    Xcount = PData1OddXCount
                End If

                Select Case outstr1(nozzleIndex)
                    Case "0"c
                    Case "1"c
                        If ColorCheckBox.Checked Then  'Different Colors
                            If nozzleIndex Mod 2 = 0 Then 'even
                                bmp.SetPixel(Xcount, Ycount, Color.Black)
                            Else
                                bmp.SetPixel(Xcount, Ycount, Color.LawnGreen)
                            End If
                        Else 'Same Color
                            bmp.SetPixel(Xcount, Ycount, Color.Black)
                        End If
                    Case Else
                        RichTextBox2.AppendText("nozzle : " & nozzleIndex & ") has a problem!" & vbLf)
                End Select
            Next
            PData1OddXCount += 1
            PData1EvenXCount += 1
        End While

        'PData2
        While PData2.Count > 0
            Dim Xcount As Integer
            Dim Ycount As Integer
            outstr2 = PData2.Dequeue()
            'RichTextBox1.AppendText(ErrorCount + 1 & vbLf)
            'RichTextBox1.AppendText(outstr2 & vbLf)

            For nozzleIndex As Integer = 0 To NozzleCnt - 1
                If type <= 1 Then 'FUNAI
                    Ycount = nozzleIndex
                ElseIf type = 2 Then 'HP
                    Ycount = nozzleIndex * 2 + 1
                ElseIf type = 3 Then 'IUT
                    Ycount = nozzleNum - nozzleIndex
                End If
                If nozzleIndex Mod 2 = 1 Then 'even (0-indexed)
                    Xcount = PData2EvenXCount
                Else
                    Xcount = PData2OddXCount
                End If

                Select Case outstr2(nozzleIndex)
                    Case "0"c
                    Case "1"c
                        If ColorCheckBox.Checked Then  'Different Colors
                            If nozzleIndex Mod 2 = 0 Then 'even
                                bmp.SetPixel(Xcount, Ycount, Color.Aqua) 'blue
                            Else
                                bmp.SetPixel(Xcount, Ycount, Color.Red)
                            End If
                        Else 'Same Color
                            bmp.SetPixel(Xcount, Ycount, Color.Black)
                        End If
                    Case Else
                        RichTextBox2.AppendText("nozzle : (" & nozzleIndex & ") has a problem!" & vbLf)
                End Select
            Next
            PData2OddXCount += 1
            PData2EvenXCount += 1
        End While

        bmp.Save(Path.GetDirectoryName(dialog1.FileName) + "\" + " result" + bmpNo.ToString + ".bmp")
        RichTextBox2.AppendText("The 'result" + bmpNo.ToString + ".bmp' is generated." + vbLf)
        PictureBox2.Image = Image.FromFile(Path.GetDirectoryName(dialog1.FileName) + "\" + " result" + bmpNo.ToString + ".bmp")
        RichTextBox2.ScrollToCaret()
        'End If
    End Sub
End Class