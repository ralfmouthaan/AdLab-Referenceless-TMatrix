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
        Me.cmdMeasureTMatrix = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdMeasureTMatrix
        '
        Me.cmdMeasureTMatrix.Location = New System.Drawing.Point(12, 35)
        Me.cmdMeasureTMatrix.Name = "cmdMeasureTMatrix"
        Me.cmdMeasureTMatrix.Size = New System.Drawing.Size(306, 30)
        Me.cmdMeasureTMatrix.TabIndex = 0
        Me.cmdMeasureTMatrix.Text = "Measure T-Matrix"
        Me.cmdMeasureTMatrix.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 450)
        Me.Controls.Add(Me.cmdMeasureTMatrix)
        Me.Name = "frmMain"
        Me.Text = "Referenceless T-Matrix"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdMeasureTMatrix As Button
End Class
