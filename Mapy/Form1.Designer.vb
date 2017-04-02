<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndOkno
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
        Me.pnlMapa = New System.Windows.Forms.Panel()
        Me.lblIlosc = New System.Windows.Forms.Label()
        Me.lblWsp = New System.Windows.Forms.Label()
        Me.lblZakres = New System.Windows.Forms.Label()
        Me.lblWspolrzedne = New System.Windows.Forms.Label()
        Me.prgWczytywanie = New System.Windows.Forms.ProgressBar()
        Me.lblWczytywanie = New System.Windows.Forms.Label()
        Me.btnPomniejsz = New System.Windows.Forms.Button()
        Me.btnPowieksz = New System.Windows.Forms.Button()
        Me.mapMapa = New Mapy.MapaPanel()
        Me.pnlMapa.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMapa
        '
        Me.pnlMapa.Controls.Add(Me.lblIlosc)
        Me.pnlMapa.Controls.Add(Me.lblWsp)
        Me.pnlMapa.Controls.Add(Me.lblZakres)
        Me.pnlMapa.Controls.Add(Me.lblWspolrzedne)
        Me.pnlMapa.Controls.Add(Me.prgWczytywanie)
        Me.pnlMapa.Controls.Add(Me.lblWczytywanie)
        Me.pnlMapa.Controls.Add(Me.btnPomniejsz)
        Me.pnlMapa.Controls.Add(Me.btnPowieksz)
        Me.pnlMapa.Controls.Add(Me.mapMapa)
        Me.pnlMapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMapa.Location = New System.Drawing.Point(0, 0)
        Me.pnlMapa.Name = "pnlMapa"
        Me.pnlMapa.Size = New System.Drawing.Size(742, 581)
        Me.pnlMapa.TabIndex = 1
        '
        'lblIlosc
        '
        Me.lblIlosc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblIlosc.AutoSize = True
        Me.lblIlosc.Location = New System.Drawing.Point(577, 522)
        Me.lblIlosc.Name = "lblIlosc"
        Me.lblIlosc.Size = New System.Drawing.Size(0, 13)
        Me.lblIlosc.TabIndex = 9
        '
        'lblWsp
        '
        Me.lblWsp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWsp.AutoSize = True
        Me.lblWsp.Location = New System.Drawing.Point(443, 522)
        Me.lblWsp.Name = "lblWsp"
        Me.lblWsp.Size = New System.Drawing.Size(0, 13)
        Me.lblWsp.TabIndex = 8
        '
        'lblZakres
        '
        Me.lblZakres.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblZakres.AutoSize = True
        Me.lblZakres.Location = New System.Drawing.Point(151, 522)
        Me.lblZakres.Name = "lblZakres"
        Me.lblZakres.Size = New System.Drawing.Size(0, 13)
        Me.lblZakres.TabIndex = 7
        '
        'lblWspolrzedne
        '
        Me.lblWspolrzedne.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWspolrzedne.AutoSize = True
        Me.lblWspolrzedne.BackColor = System.Drawing.Color.Transparent
        Me.lblWspolrzedne.Location = New System.Drawing.Point(12, 522)
        Me.lblWspolrzedne.Name = "lblWspolrzedne"
        Me.lblWspolrzedne.Size = New System.Drawing.Size(0, 13)
        Me.lblWspolrzedne.TabIndex = 6
        '
        'prgWczytywanie
        '
        Me.prgWczytywanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prgWczytywanie.Location = New System.Drawing.Point(97, 546)
        Me.prgWczytywanie.Name = "prgWczytywanie"
        Me.prgWczytywanie.Size = New System.Drawing.Size(633, 23)
        Me.prgWczytywanie.TabIndex = 4
        Me.prgWczytywanie.Visible = False
        '
        'lblWczytywanie
        '
        Me.lblWczytywanie.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWczytywanie.AutoSize = True
        Me.lblWczytywanie.Location = New System.Drawing.Point(12, 551)
        Me.lblWczytywanie.Name = "lblWczytywanie"
        Me.lblWczytywanie.Size = New System.Drawing.Size(79, 13)
        Me.lblWczytywanie.TabIndex = 3
        Me.lblWczytywanie.Text = "Wczytywanie..."
        Me.lblWczytywanie.Visible = False
        '
        'btnPomniejsz
        '
        Me.btnPomniejsz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPomniejsz.Location = New System.Drawing.Point(707, 488)
        Me.btnPomniejsz.Name = "btnPomniejsz"
        Me.btnPomniejsz.Size = New System.Drawing.Size(23, 23)
        Me.btnPomniejsz.TabIndex = 2
        Me.btnPomniejsz.Text = "-"
        Me.btnPomniejsz.UseVisualStyleBackColor = True
        '
        'btnPowieksz
        '
        Me.btnPowieksz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPowieksz.Location = New System.Drawing.Point(707, 517)
        Me.btnPowieksz.Name = "btnPowieksz"
        Me.btnPowieksz.Size = New System.Drawing.Size(23, 23)
        Me.btnPowieksz.TabIndex = 1
        Me.btnPowieksz.Text = "+"
        Me.btnPowieksz.UseVisualStyleBackColor = True
        '
        'mapMapa
        '
        Me.mapMapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapMapa.Location = New System.Drawing.Point(0, 0)
        Me.mapMapa.Name = "mapMapa"
        Me.mapMapa.Size = New System.Drawing.Size(742, 581)
        Me.mapMapa.TabIndex = 5
        '
        'wndOkno
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 581)
        Me.Controls.Add(Me.pnlMapa)
        Me.Name = "wndOkno"
        Me.Text = "Mapa"
        Me.pnlMapa.ResumeLayout(False)
        Me.pnlMapa.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMapa As Panel
    Friend WithEvents btnPomniejsz As Button
    Friend WithEvents btnPowieksz As Button
    Friend WithEvents prgWczytywanie As ProgressBar
    Friend WithEvents lblWczytywanie As Label
    Friend WithEvents lblWspolrzedne As Label
    Friend WithEvents mapMapa As MapaPanel
    Friend WithEvents lblZakres As Label
    Friend WithEvents lblWsp As Label
    Friend WithEvents lblIlosc As Label
End Class
