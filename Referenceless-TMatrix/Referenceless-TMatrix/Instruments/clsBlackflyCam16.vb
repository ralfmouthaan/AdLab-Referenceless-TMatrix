﻿' Ralf Mouthaan
' University of Adelaide
' August 2023
'
' Code to control Blacklfy camera

Imports SpinnakerNET
Imports SpinnakerNET.GenApi
Imports SpinnakerNET.Video

Public Class clsBlackflyCam16
    Inherits clsCamera16

    Private SerialNumber As String
    Private Cam As IManagedCamera
    Private ImgProcessor As IManagedImageProcessor
    Private NodeMap As INodeMap

    Private _Exposure As Double
    Private _Gain As Double

    Public Property intCameraID As Integer
    Public Property intTriggerMode As Integer
    Public Property portSLMClock As IO.Ports.SerialPort
    Public Property bolActive As Boolean

    Public Sub New(Optional ByVal _SerialNumber As String = "")
        MyBase.New

        _intCameraID = 1
        SerialNumber = _SerialNumber
        _strInstrumentName = "FLIR Blackfly"
        _Gain = 0

    End Sub

    Public Overrides Sub Startup(Optional bolShow As Boolean = False)

        If bolConnectionOpen = True Then Exit Sub
        Cam = Nothing

        Dim CameraSystem As ManagedSystem = New ManagedSystem()
        Dim InterfaceList As List(Of IManagedInterface) = CameraSystem.GetInterfaces()
        Dim CameraList As List(Of IManagedCamera) = CameraSystem.GetCameras()

        Dim idxInterface As Integer
        Dim idxCamera As Integer

        For idxInterface = 0 To InterfaceList.Count - 1

            Dim ManagedInterface As IManagedInterface = InterfaceList(idxInterface)
            ManagedInterface.UpdateCameras()

            For idxCamera = 0 To CameraList.Count - 1
                Dim _cam As IManagedCamera = CameraList(idxCamera)
                NodeMap = _cam.GetTLDeviceNodeMap()
                Dim _SerialNumber As String = NodeMap.GetNode(Of IString)("DeviceSerialNumber").Value

                If SerialNumber = SerialNumber Then
                    Cam = _cam
                    Exit For
                Else
                    Cam.Dispose()
                End If

            Next

            CameraList.Clear()
            If Cam IsNot Nothing Then Exit For

        Next

        InterfaceList.Clear()
        CameraSystem.Dispose()

        Cam.Init()
        NodeMap = Cam.GetNodeMap

        Dim ExposureAutoNode As IEnum = NodeMap.GetNode(Of IEnum)("ExposureAuto")
        Dim iExposureAutoOff As IEnumEntry = ExposureAutoNode.GetEntryByName("Off")
        ExposureAutoNode.Value = iExposureAutoOff.Value

        Dim GainAutoNode As IEnum = NodeMap.GetNode(Of IEnum)("GainAuto")
        Dim GainAutoOff As IEnumEntry = GainAutoNode.GetEntryByName("Off")
        GainAutoNode.Value = GainAutoOff.Value

        Dim GammaEnableNode As BoolNode = NodeMap.GetNode(Of BoolNode)("GammaEnable")
        GammaEnableNode.Value = False

        'Dim BalanceWhiteAutoNode As IEnum = NodeMap.GetNode(Of IEnum)("BalanceWhiteAuto")
        'Dim BalanceWhiteAutoOff As IEnumEntry = BalanceWhiteAutoNode.GetEntryByName("Off")
        'BalanceWhiteAutoNode.Value = BalanceWhiteAutoOff.Value
        'Dim BalanceRatioNode As IFloat = NodeMap.GetNode(Of IFloat)("BalanceRatio")
        'BalanceRatioNode.Value = 1

        Dim AcquisitionNode As IEnum = NodeMap.GetNode(Of IEnum)("AcquisitionMode")
        Dim AcquisitionContinuous As IEnumEntry = AcquisitionNode.GetEntryByName("Continuous")
        AcquisitionNode.Value = AcquisitionContinuous.Value
        Dim TriggerModeNode As IEnum = NodeMap.GetNode(Of IEnum)("TriggerMode")
        Dim TriggerModeOff As IEnumEntry = TriggerModeNode.GetEntryByName("Off")
        TriggerModeNode.Value = TriggerModeOff.Value
        Dim TriggerModeSelectorNode As IEnum = NodeMap.GetNode(Of IEnum)("TriggerSelector")
        Dim TriggerModeFrameStart As IEnumEntry = TriggerModeSelectorNode.GetEntryByName("FrameStart")
        TriggerModeSelectorNode.Value = TriggerModeFrameStart.Value
        Dim TriggerModeSourceNode As IEnum = NodeMap.GetNode(Of IEnum)("TriggerSource")
        Dim TriggerModeSourceSoftware As IEnumEntry = TriggerModeSourceNode.GetEntryByName("Software")
        TriggerModeSourceNode.Value = TriggerModeSourceSoftware.Value
        Dim TriggerModeOn As IEnumEntry = TriggerModeNode.GetEntryByName("On")
        TriggerModeNode.Value = TriggerModeOn.Value

        ImgProcessor = New ManagedImageProcessor()
        ImgProcessor.SetColorProcessing(ColorProcessingAlgorithm.HQ_LINEAR)

        MyBase.Startup(bolShow)
        Exposure = _Exposure
        Gain = _Gain

        Cam.BeginAcquisition()

    End Sub
    Public Overrides Sub Shutdown()
        MyBase.Shutdown()
        If Cam IsNot Nothing Then
            Cam.EndAcquisition()
            Cam.Dispose()
        End If
    End Sub

    Public Overrides ReadOnly Property ImageWidth As Integer
        Get
            Return CInt(NodeMap.GetNode(Of IInteger)("Width").Value)
        End Get
    End Property
    Public Overrides ReadOnly Property ImageHeight As Integer
        Get
            Dim nodemap As INodeMap = Cam.GetNodeMap()
            Return CInt(nodemap.GetNode(Of IInteger)("Height").Value)
        End Get
    End Property

    Public Overrides Property Exposure As Double
        Get
            If bolConnectionOpen = True Then
                Return CDbl(NodeMap.GetNode(Of IFloat)("ExposureTime").Value)
            Else
                Return _Exposure
            End If
        End Get
        Set(value As Double)

            _Exposure = value
            If bolConnectionOpen = False Then Exit Property

            Dim ExposureTimeNode As IFloat = NodeMap.GetNode(Of IFloat)("ExposureTime")
            ExposureTimeNode.Value = value
        End Set
    End Property
    Public Property Gain As Double
        Get
            If bolConnectionOpen = True Then
                Return CDbl(NodeMap.GetNode(Of IFloat)("Gain").Value)
            Else
                Return _Gain
            End If
        End Get
        Set(value As Double)

            _Gain = value
            If bolConnectionOpen = False Then Exit Property

            Dim GainNode As IFloat = NodeMap.GetNode(Of IFloat)("Gain")
            GainNode.Value = value
        End Set
    End Property
    Public ReadOnly Property FrameRate As Double
        Get
            Return NodeMap.GetNode(Of IFloat)("AcquisitionFrameRate").Value
        End Get
    End Property

    Public Overrides Sub SaveImage(Filename As String)

        ExecuteTrigger()
        Dim rawImage As IManagedImage = Cam.GetNextImage(1000)
        Dim convertedImage As IManagedImage = ImgProcessor.Convert(rawImage, PixelFormatEnums.Mono16)
        convertedImage.Save(Filename)
        rawImage.Dispose()
        convertedImage.Dispose()

    End Sub
    Public Overrides Function GetIntegerImage(Optional NoFrames As Integer = -1) As UInt16(,)

        ExecuteTrigger()

        Dim rawImage As IManagedImage
        Try
            rawImage = Cam.GetNextImage(1000)
        Catch
            Return Nothing
        End Try

        Dim convertedImage As IManagedImage = ImgProcessor.Convert(rawImage, PixelFormatEnums.Mono16)
        Dim RetVal(convertedImage.Height - 1, convertedImage.Width - 1) As UInt16

        For i = 0 To convertedImage.Height - 1
            Dim rowIndex As Integer = i * 2 * convertedImage.Width
            For j = 0 To convertedImage.Width - 1
                RetVal(i, j) = convertedImage.ManagedData(rowIndex + j * 2 + 1)
                RetVal(i, j) = RetVal(i, j) << 8
                RetVal(i, j) = RetVal(i, j) + convertedImage.ManagedData(rowIndex + j * 2)
            Next
        Next

        rawImage.Dispose()
        convertedImage.Dispose()

        Return RetVal

    End Function
    Public Function GetBitmapImage() As Bitmap
        ExecuteTrigger()
        Dim rawImage As IManagedImage = Cam.GetNextImage(1000)
        Dim convertedImage As IManagedImage = ImgProcessor.Convert(rawImage, PixelFormatEnums.Mono16)
        Return convertedImage.bitmap
    End Function

    Private video As IManagedSpinVideo
    Public Sub CreateAVI(Filename As String)

        Dim uncompressedOption As AviOption = New AviOption()
        uncompressedOption.frameRate = 2 'Makes viewing nice
        uncompressedOption.height = ImageHeight
        uncompressedOption.width = ImageWidth

        video = New ManagedSpinVideo
        video.Open(Filename, uncompressedOption)

    End Sub
    Public Sub SaveImageToAVI()

        ExecuteTrigger()
        Dim rawImage As IManagedImage = Cam.GetNextImage(1000)
        Dim convertedImage As IManagedImage = ImgProcessor.Convert(rawImage, PixelFormatEnums.Mono16)

        video.Append(convertedImage)

        rawImage.Dispose()
        convertedImage.Dispose()

    End Sub
    Public Sub CloseAVI()

        video.Close()
        video.Dispose()

    End Sub

    Private Sub ExecuteTrigger()
        If bolConnectionOpen = False Then Exit Sub
        Dim TriggerSoftwareNode As ICommand = NodeMap.GetNode(Of ICommand)("TriggerSoftware")
        TriggerSoftwareNode.Execute()
    End Sub
End Class
