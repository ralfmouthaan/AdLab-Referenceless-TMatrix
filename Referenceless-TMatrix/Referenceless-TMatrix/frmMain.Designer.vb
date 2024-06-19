<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cmdMeasureTMatrix = New System.Windows.Forms.Button()
        Me.lblNoMacropixels = New System.Windows.Forms.Label()
        Me.cmbNoMacropixels = New System.Windows.Forms.ComboBox()
        Me.lblNoMeasurements = New System.Windows.Forms.Label()
        Me.nudNoMeasurements = New System.Windows.Forms.NumericUpDown()
        CType(Me.nudNoMeasurements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdMeasureTMatrix
        '
        Me.cmdMeasureTMatrix.Location = New System.Drawing.Point(12, 92)
        Me.cmdMeasureTMatrix.Name = "cmdMeasureTMatrix"
        Me.cmdMeasureTMatrix.Size = New System.Drawing.Size(306, 30)
        Me.cmdMeasureTMatrix.TabIndex = 0
        Me.cmdMeasureTMatrix.Text = "Measure T-Matrix"
        Me.cmdMeasureTMatrix.UseVisualStyleBackColor = True
        '
        'lblNoMacropixels
        '
        Me.lblNoMacropixels.AutoSize = True
        Me.lblNoMacropixels.Location = New System.Drawing.Point(34, 23)
        Me.lblNoMacropixels.Name = "lblNoMacropixels"
        Me.lblNoMacropixels.Size = New System.Drawing.Size(83, 13)
        Me.lblNoMacropixels.TabIndex = 1
        Me.lblNoMacropixels.Text = "No Macropixels:"
        '
        'cmbNoMacropixels
        '
        Me.cmbNoMacropixels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoMacropixels.FormattingEnabled = True
        Me.cmbNoMacropixels.Location = New System.Drawing.Point(136, 20)
        Me.cmbNoMacropixels.Name = "cmbNoMacropixels"
        Me.cmbNoMacropixels.Size = New System.Drawing.Size(120, 21)
        Me.cmbNoMacropixels.TabIndex = 2
        '
        'lblNoMeasurements
        '
        Me.lblNoMeasurements.AutoSize = True
        Me.lblNoMeasurements.Location = New System.Drawing.Point(34, 56)
        Me.lblNoMeasurements.Name = "lblNoMeasurements"
        Me.lblNoMeasurements.Size = New System.Drawing.Size(96, 13)
        Me.lblNoMeasurements.TabIndex = 3
        Me.lblNoMeasurements.Text = "No Measurements:"
        '
        'nudNoMeasurements
        '
        Me.nudNoMeasurements.Location = New System.Drawing.Point(136, 54)
        Me.nudNoMeasurements.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nudNoMeasurements.Name = "nudNoMeasurements"
        Me.nudNoMeasurements.Size = New System.Drawing.Size(120, 20)
        Me.nudNoMeasurements.TabIndex = 4
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 450)
        Me.Controls.Add(Me.nudNoMeasurements)
        Me.Controls.Add(Me.lblNoMeasurements)
        Me.Controls.Add(Me.cmbNoMacropixels)
        Me.Controls.Add(Me.lblNoMacropixels)
        Me.Controls.Add(Me.cmdMeasureTMatrix)
        Me.Name = "frmMain"
        Me.Text = "Referenceless T-Matrix"
        CType(Me.nudNoMeasurements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdMeasureTMatrix As Button
    Friend WithEvents lblNoMacropixels As Label
    Friend WithEvents cmbNoMacropixels As ComboBox
    Friend WithEvents lblNoMeasurements As Label
    Friend WithEvents nudNoMeasurements As NumericUpDown
End Class
