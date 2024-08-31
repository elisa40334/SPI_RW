Module userdefine
    Public WM_USB_STATUS = &H400 + 100
    Public WM_TRIG_STATUS = &H400 + 101
    Public mHandle As Integer
    Public sFormTitle As String
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Int32, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Long
    Private Declare Auto Function FindWindow Lib "user32" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Delegate Function CallBack(ByVal byIndex As Byte, ByVal dwStatus As UInteger) As Boolean

    Public Function USB_Event(ByVal byIndex As Byte, ByVal dwUSBStatus As UInteger) As Boolean
        mHandle = FindWindow(vbNullString, sFormTitle)
        PostMessage(mHandle, WM_USB_STATUS, byIndex, dwUSBStatus)
        Return True
    End Function
    Public Function Trig_Event(ByVal byIndex As Byte, ByVal dwTrigStatus As UInteger) As Boolean
        mHandle = FindWindow(vbNullString, sFormTitle)
        PostMessage(mHandle, WM_TRIG_STATUS, byIndex, dwTrigStatus)
        Return True
    End Function
End Module



Module ExtenalDll



    Public Declare Function USBIO_SetUSBNotify Lib "usb2uis.dll" Alias "_USBIO_SetUSBNotify@8" (ByVal bLoged As Boolean, ByVal pUSB_CallBack As CallBack) As Boolean
    Public Declare Function USBIO_SetTrigNotify Lib "usb2uis.dll" Alias "_USBIO_SetTrigNotify@8" (ByVal byIndex As Byte, ByVal pUSB_CallBack As CallBack) As Boolean

    Public Declare Function USBIO_OpenDevice Lib "usb2uis.dll" Alias "_USBIO_OpenDevice@0" () As Byte
    Public Declare Function USBIO_OpenDeviceByNumber Lib "usb2uis.dll" Alias "_USBIO_OpenDeviceByNumber@4" (ByVal pSerialString() As String) As Byte
    Public Declare Function USBIO_CloseDevice Lib "usb2uis.dll" Alias "_USBIO_CloseDevice@4" (ByVal byIndex As Byte) As Boolean
    Public Declare Function USBIO_CloseDeviceByNumber Lib "usb2uis.dll" Alias "_USBIO_CloseDeviceByNumber@4" (ByVal pSerialString As String) As Boolean
    Public Declare Function USBIO_ResetDevice Lib "usb2uis.dll" Alias "_USBIO_ResetDevice@8" (ByVal byIndex As Byte) As Boolean

    Public Declare Function USBIO_GetMaxNumofDev Lib "usb2uis.dll" Alias "_USBIO_GetMaxNumofDev@0" () As Byte
    Public Declare Function USBIO_GetVersion Lib "usb2uis.dll" Alias "_USBIO_GetVersion@12" (ByVal byIndex As Byte, ByVal byType As Byte, ByVal lpBuffer As String) As Boolean
    Public Declare Function USBIO_GetSerialNo Lib "usb2uis.dll" Alias "_USBIO_GetSerialNo@8" (ByVal byIndex As Byte, ByVal lpBuff As String) As Boolean
    Public Declare Function USBIO_GetWorkMode Lib "usb2uis.dll" Alias "_USBIO_GetWorkMode@8" (ByVal byIndex As Byte, ByRef lpMode As Byte) As Boolean

    Public Declare Function USBIO_I2cAutoGetAddress Lib "usb2uis.dll" Alias "_USBIO_I2cAutoGetAddress@8" (ByVal byIndex As Byte, ByRef pbyDevAddr As Byte) As Boolean
    Public Declare Function USBIO_I2cGetConfig Lib "usb2uis.dll" Alias "_USBIO_I2cGetConfig@16" (ByVal byIndex As Byte, ByRef pbyDevAddr As Byte, ByRef pbyRate As Byte, ByRef pdwMilliseconds As Integer) As Boolean
    Public Declare Function USBIO_I2cSetConfig Lib "usb2uis.dll" Alias "_USBIO_I2cSetConfig@16" (ByVal byIndex As Byte, ByVal byDevAddr As Byte, ByVal byRate As Byte, ByVal dwMilliseconds As Integer) As Boolean
    Public Declare Function USBIO_I2cRead Lib "usb2uis.dll" Alias "_USBIO_I2cRead@24" (ByVal byIndex As Byte, ByVal byDevAddr As Byte, ByVal lpParaBuffer() As Byte, ByVal byParaSize As Byte, ByVal lpReadBuffer() As Byte, ByVal wReadSize As Short) As Boolean
    Public Declare Function USBIO_I2cWrite Lib "usb2uis.dll" Alias "_USBIO_I2cWrite@24" (ByVal byIndex As Byte, ByVal byDevAddr As Byte, ByVal lpParaBuffer() As Byte, ByVal byParaSize As Byte, ByVal lpWriteBuffer() As Byte, ByVal wWriteSize As Short) As Boolean
    Public Declare Function USBIO_I2cReadEEProm Lib "usb2uis.dll" Alias "_USBIO_I2cReadEEProm@24" (ByVal byIndex As Byte, ByVal byDevAddr As Byte, ByVal byTypeIndex As Byte, ByVal dwOffset As Integer, ByVal lpReadBuffer() As Byte, ByVal wReadSize As Short) As Boolean
    Public Declare Function USBIO_I2cWriteEEProm Lib "usb2uis.dll" Alias "_USBIO_I2cWriteEEProm@24" (ByVal byIndex As Byte, ByVal byDevAddr As Byte, ByVal byTypeIndex As Byte, ByVal dwOffset As Integer, ByVal lpWriteBuffer() As Byte, ByVal wWriteSize As Short) As Boolean

    Public Declare Function USBIO_SPIGetConfig Lib "usb2uis.dll" Alias "_USBIO_SPIGetConfig@12" (ByVal byIndex As Byte, ByRef pbyRate As Byte, ByRef pdwMilliseconds As Integer) As Boolean
    Public Declare Function USBIO_SPISetConfig Lib "usb2uis.dll" Alias "_USBIO_SPISetConfig@12" (ByVal byIndex As Byte, ByVal byRate As Byte, ByVal dwMilliseconds As Integer) As Boolean
    Public Declare Function USBIO_SPIRead Lib "usb2uis.dll" Alias "_USBIO_SPIRead@20" (ByVal byIndex As Byte, ByVal lpParaBuffer() As Byte, ByVal byParaSize As Byte, ByVal lpReadBuffer() As Byte, ByVal wReadSize As Short) As Boolean
    Public Declare Function USBIO_SPIWrite Lib "usb2uis.dll" Alias "_USBIO_SPIWrite@20" (ByVal byIndex As Byte, ByVal lpParaBuffer() As Byte, ByVal byParaSize As Byte, ByVal lpWriteBuffer() As Byte, ByVal wWriteSize As Short) As Boolean
    Public Declare Function USBIO_SPITest Lib "usb2uis.dll" Alias "_USBIO_SPITest@16" (ByVal byIndex As Byte, ByVal lpWriteBuffer() As Byte, ByVal lpReadBuffer() As Byte, ByVal byTestSize As Byte) As Boolean


    Public Declare Function USBIO_GetADCConfig Lib "usb2uis.dll" Alias "_USBIO_GetADCConfig@12" (ByVal byIndex As Byte, ByRef pbyMask As Byte, ByVal pbyIOSelect() As Byte) As Boolean
    Public Declare Function USBIO_SetADCConfig Lib "usb2uis.dll" Alias "_USBIO_SetADCConfig@12" (ByVal byIndex As Byte, ByRef pbyMask As Byte, ByVal pbyIOSelect() As Byte) As Boolean
    Public Declare Function USBIO_ADCRead Lib "usb2uis.dll" Alias "_USBIO_ADCRead@12" (ByVal byIndex As Byte, ByVal lpReadBuffer() As Short, ByVal wBuffSize As Short) As Boolean

    Public Declare Function USBIO_GetGPIOConfig Lib "usb2uis.dll" Alias "_USBIO_GetGPIOConfig@8" (ByVal byIndex As Byte, ByRef pbyValue As Byte) As Boolean
    Public Declare Function USBIO_SetGPIOConfig Lib "usb2uis.dll" Alias "_USBIO_SetGPIOConfig@8" (ByVal byIndex As Byte, ByVal byValue As Byte) As Boolean
    Public Declare Function USBIO_GPIORead Lib "usb2uis.dll" Alias "_USBIO_GPIORead@8" (ByVal byIndex As Byte, ByRef pbyValue As Byte) As Byte
    Public Declare Function USBIO_GPIOWrite Lib "usb2uis.dll" Alias "_USBIO_GPIOWrite@12" (ByVal byIndex As Byte, ByVal byValue As Byte, ByVal byMask As Byte) As Boolean
    Public Declare Function USBIO_GetCE Lib "usb2uis.dll" Alias "_USBIO_GetCE@8" (ByVal byIndex As Byte, ByRef pbyLevel As Byte) As Boolean
    Public Declare Function USBIO_SetCE Lib "usb2uis.dll" Alias "_USBIO_SetCE@8" (ByVal byIndex As Byte, ByVal bHigh As Boolean) As Boolean

    Public Declare Function USBIO_GetPWMConfig Lib "usb2uis.dll" Alias "_USBIO_GetPWMConfig@16" (ByVal byIndex As Byte, ByRef pbyRate As Byte, ByRef pbyNum As Byte, ByVal pwDuty() As Short) As Boolean
    Public Declare Function USBIO_SetGPIOConfig Lib "usb2uis.dll" Alias "_USBIO_SetPWMConfig@16" (ByVal byIndex As Byte, ByVal byRate As Byte, ByVal byNum As Byte, ByVal pwDuty() As Short) As Boolean
    Public Declare Function USBIO_StartPWM Lib "usb2uis.dll" Alias "_USBIO_StartPWM@4" (ByVal byIndex As Byte) As Boolean
    Public Declare Function USBIO_StopPWM Lib "usb2uis.dll" Alias "_USBIO_StopPWM@4" (ByVal byIndex As Byte) As Boolean

    Public Declare Function USBIO_TrigGetConfig Lib "usb2uis.dll" Alias "_USBIO_TrigGetConfig@8" (ByVal byIndex As Byte, ByRef pbySelect As Byte) As Boolean
    Public Declare Function USBIO_TrigSetConfig Lib "usb2uis.dll" Alias "_USBIO_TrigSetConfig@8" (ByVal byIndex As Byte, ByVal bySelect As Byte) As Boolean
    Public Declare Function USBIO_ExitTrig Lib "usb2uis.dll" Alias "_USBIO_ExitTrig@4" (ByVal byIndex As Byte) As Boolean
    Public Declare Function USBIO_WaitForTrig Lib "usb2uis.dll" Alias "_USBIO_WaitForTrig@4" (ByVal byIndex As Byte) As Boolean
    Public Declare Function USBIO_ReadTrig Lib "usb2uis.dll" Alias "_USBIO_ReadTrig@12" (ByVal byIndex As Byte, ByRef pdwTrigFlag As UInteger, ByVal dwMilliseconds As UInteger) As Boolean











End Module
