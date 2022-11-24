<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndSzukajAdresu
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUlica = New System.Windows.Forms.TextBox()
        Me.txtNrDomu = New System.Windows.Forms.TextBox()
        Me.btnSzukaj = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ulica:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Numer:"
        '
        'txtUlica
        '
        Me.txtUlica.Location = New System.Drawing.Point(59, 12)
        Me.txtUlica.Name = "txtUlica"
        Me.txtUlica.Size = New System.Drawing.Size(213, 20)
        Me.txtUlica.TabIndex = 3
        '
        'txtNrDomu
        '
        Me.txtNrDomu.Location = New System.Drawing.Point(59, 38)
        Me.txtNrDomu.Name = "txtNrDomu"
        Me.txtNrDomu.Size = New System.Drawing.Size(213, 20)
        Me.txtNrDomu.TabIndex = 4
        '
        'btnSzukaj
        '
        Me.btnSzukaj.Location = New System.Drawing.Point(197, 64)
        Me.btnSzukaj.Name = "btnSzukaj"
        Me.btnSzukaj.Size = New System.Drawing.Size(75, 23)
        Me.btnSzukaj.TabIndex = 5
        Me.btnSzukaj.Text = "Szukaj"
        Me.btnSzukaj.UseVisualStyleBackColor = True
        '
        'wndSzukajAdresu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 99)
        Me.Controls.Add(Me.btnSzukaj)
        Me.Controls.Add(Me.txtNrDomu)
        Me.Controls.Add(Me.txtUlica)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "wndSzukajAdresu"
        Me.Text = "Szukaj adresu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUlica As TextBox
    Friend WithEvents txtNrDomu As TextBox
    Friend WithEvents btnSzukaj As Button
End Class
