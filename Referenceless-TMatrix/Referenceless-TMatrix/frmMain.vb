Imports System.IO

Public Class frmMain
    Private Sub cmdMeasureTMatrix_Click(sender As Object, e As EventArgs) Handles cmdMeasureTMatrix.Click

        Dim HoloWidth As Integer = 512
        Dim NoMacropixels As Integer = CInt(cmbNoMacropixels.SelectedItem)
        Dim NoMeasurements As Integer = CInt(nudNoMeasurements.Value)

        Dim Holo As clsMacropixelHologram
        Dim writer As New StreamWriter("Raw Results/Input Holos.txt")

        'Startup
        MeasurementSetup = New clsMeasurementSetup
        MeasurementSetup.Startup()
        MeasurementSetup.SLM.lstHolograms.Clear()
        MeasurementSetup.Camera.CreateAVI("Raw Results/Output Images")

        'Set up object hologram
        Holo = New clsMacropixelHologram With {
            .bolCircularAperture = False,
            .bolVisible = True,
            .RawWidth = HoloWidth
        }
        ReDim Holo.arrMacroPixels(NoMacropixels - 1)
        Holo.LoadZernikes("C:\Instrument Setup\Tx1 Zernikes.txt")
        MeasurementSetup.SLM.lstHolograms.Add(Holo)

        For i = 1 To NoMeasurements

            Holo.SetToRandom()
            MeasurementSetup.SLM.Refresh()
            writer.WriteLine(TypeConversions.HoloToString(Holo.arrMacroPixels))
            MeasurementSetup.Camera.SaveImageToAVI()
            Console.WriteLine(i)
            Threading.Thread.Sleep(10)

        Next

        MeasurementSetup.Camera.CloseAVI()
        writer.Close()
        MeasurementSetup.Shutdown()

    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        cmbNoMacropixels.Items.Add(256)
        cmbNoMacropixels.Items.Add(1024)
        cmbNoMacropixels.Items.Add(2048)
        cmbNoMacropixels.Items.Add(4096)
        cmbNoMacropixels.SelectedIndex = 2

        nudNoMeasurements.Value = 1000

    End Sub

End Class
