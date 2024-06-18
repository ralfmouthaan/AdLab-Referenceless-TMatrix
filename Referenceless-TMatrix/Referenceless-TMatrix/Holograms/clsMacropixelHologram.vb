' Ralf Mouthaan
' University of Cambridge
' March 2020

Option Explicit On
Option Strict On

Public Class clsMacropixelHologram
    Inherits clsHologram

    Private _arrMacropixels As Double()
    Private _NoMacropixels As Integer
    Private _Holowidth_MPx As Integer
    Private _MacropixelWidth As Integer
    Private _arrHadamard(,) As Integer

    Public Property arrMacroPixels As Double()
        Get
            Return _arrMacropixels
        End Get
        Set(value As Double())
            If value Is Nothing Then Exit Property
            If value.Count = 0 Then Exit Property
            _arrMacropixels = value
            _NoMacropixels = arrMacroPixels.GetLength(0)
            _MacropixelWidth = CInt(Math.Round(RawWidth / Math.Sqrt(_NoMacropixels)))
            _Holowidth_MPx = CInt(Math.Sqrt(_NoMacropixels))
        End Set
    End Property
    Public Overrides Property RawWidth As Integer
        Get
            Return MyBase.RawWidth
        End Get
        Set(value As Integer)
            MyBase.RawWidth = value
            arrMacroPixels = _arrMacropixels
        End Set
    End Property
    Public Overrides Property arrRawHologram(i As Integer, j As Integer) As Double
        Get

            Dim idx As Integer

            Dim idx_x As Integer
            Dim idx_y As Integer

            idx_x = CInt(Math.Floor(j / _MacropixelWidth))
            idx_y = CInt(Math.Floor(i / _MacropixelWidth))

            If idx_x > _Holowidth_MPx - 1 Then idx_x = _Holowidth_MPx - 1
            If idx_y > _Holowidth_MPx - 1 Then idx_y = _Holowidth_MPx - 1

            idx = idx_x * _Holowidth_MPx + idx_y

            Return _arrMacropixels(idx)

        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property
    Public Sub SetToRandom()

        Static Rnd As New Random

        For i = 0 To arrMacroPixels.Length - 1
            arrMacroPixels(i) = 2 * Math.PI * Rnd.NextDouble
        Next

    End Sub
    Public Sub LoadFromFile(ByVal Filename As String)

        Dim reader As New System.IO.StreamReader(Filename)

        arrMacroPixels = TypeConversions.StringToHolo(reader.ReadLine)

        reader.Close()

    End Sub

End Class
