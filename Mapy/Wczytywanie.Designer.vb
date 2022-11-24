<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndWczytywanie
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
        Me.lblPlik = New System.Windows.Forms.Label()
        Me.prgPostep = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'lblPlik
        '
        Me.lblPlik.AutoSize = True
        Me.lblPlik.Location = New System.Drawing.Point(12, 9)
        Me.lblPlik.Name = "lblPlik"
        Me.lblPlik.Size = New System.Drawing.Size(39, 13)
        Me.lblPlik.TabIndex = 0
        Me.lblPlik.Text = "Label1"
        '
        'prgPostep
        '
        Me.prgPostep.Location = New System.Drawing.Point(15, 25)
        Me.prgPostep.Name = "prgPostep"
        Me.prgPostep.Size = New System.Drawing.Size(446, 23)
        Me.prgPostep.TabIndex = 1
        '
        'wndWczytywanie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 67)
        Me.Controls.Add(Me.prgPostep)
        Me.Controls.Add(Me.lblPlik)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "wndWczytywanie"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wczytywanie"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblPlik As Label
    Friend WithEvents prgPostep As ProgressBar
End Class
