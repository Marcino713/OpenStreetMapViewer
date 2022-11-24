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
        Me.cbSzukaj = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stlPowiekszenie = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stlElementy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuNarzedzia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSzukajObiektu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSzukajAdresu = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMapa
        '
        Me.pnlMapa.Controls.Add(Me.cbSzukaj)
        Me.pnlMapa.Controls.Add(Me.StatusStrip1)
        Me.pnlMapa.Controls.Add(Me.MenuStrip1)
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
        'cbSzukaj
        '
        Me.cbSzukaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSzukaj.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbSzukaj.AutoSize = True
        Me.cbSzukaj.Location = New System.Drawing.Point(716, 475)
        Me.cbSzukaj.Name = "cbSzukaj"
        Me.cbSzukaj.Size = New System.Drawing.Size(23, 23)
        Me.cbSzukaj.TabIndex = 12
        Me.cbSzukaj.Text = "?"
        Me.cbSzukaj.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.stlPowiekszenie, Me.ToolStripStatusLabel3, Me.stlElementy})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 559)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(742, 22)
        Me.StatusStrip1.TabIndex = 10
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel1.Text = "Powiększenie:"
        '
        'stlPowiekszenie
        '
        Me.stlPowiekszenie.Name = "stlPowiekszenie"
        Me.stlPowiekszenie.Size = New System.Drawing.Size(13, 17)
        Me.stlPowiekszenie.Text = "0"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Margin = New System.Windows.Forms.Padding(20, 3, 0, 2)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel3.Text = "Wyświetlane elementy:"
        '
        'stlElementy
        '
        Me.stlElementy.Name = "stlElementy"
        Me.stlElementy.Size = New System.Drawing.Size(13, 17)
        Me.stlElementy.Text = "0"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNarzedzia})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(742, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuNarzedzia
        '
        Me.mnuNarzedzia.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSzukajObiektu, Me.mnuSzukajAdresu})
        Me.mnuNarzedzia.Name = "mnuNarzedzia"
        Me.mnuNarzedzia.Size = New System.Drawing.Size(70, 20)
        Me.mnuNarzedzia.Text = "Narzędzia"
        '
        'mnuSzukajObiektu
        '
        Me.mnuSzukajObiektu.Name = "mnuSzukajObiektu"
        Me.mnuSzukajObiektu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.mnuSzukajObiektu.Size = New System.Drawing.Size(199, 22)
        Me.mnuSzukajObiektu.Text = "Szukaj obiektu..."
        '
        'mnuSzukajAdresu
        '
        Me.mnuSzukajAdresu.Name = "mnuSzukajAdresu"
        Me.mnuSzukajAdresu.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.mnuSzukajAdresu.Size = New System.Drawing.Size(199, 22)
        Me.mnuSzukajAdresu.Text = "Szukaj adresu..."
        '
        'lblIlosc
        '
        Me.lblIlosc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblIlosc.AutoSize = True
        Me.lblIlosc.Location = New System.Drawing.Point(577, 509)
        Me.lblIlosc.Name = "lblIlosc"
        Me.lblIlosc.Size = New System.Drawing.Size(25, 13)
        Me.lblIlosc.TabIndex = 9
        Me.lblIlosc.Text = "aaa"
        '
        'lblWsp
        '
        Me.lblWsp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWsp.AutoSize = True
        Me.lblWsp.Location = New System.Drawing.Point(442, 509)
        Me.lblWsp.Name = "lblWsp"
        Me.lblWsp.Size = New System.Drawing.Size(25, 13)
        Me.lblWsp.TabIndex = 8
        Me.lblWsp.Text = "aaa"
        '
        'lblZakres
        '
        Me.lblZakres.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblZakres.AutoSize = True
        Me.lblZakres.Location = New System.Drawing.Point(152, 509)
        Me.lblZakres.Name = "lblZakres"
        Me.lblZakres.Size = New System.Drawing.Size(25, 13)
        Me.lblZakres.TabIndex = 7
        Me.lblZakres.Text = "aaa"
        '
        'lblWspolrzedne
        '
        Me.lblWspolrzedne.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWspolrzedne.AutoSize = True
        Me.lblWspolrzedne.BackColor = System.Drawing.Color.Transparent
        Me.lblWspolrzedne.Location = New System.Drawing.Point(3, 509)
        Me.lblWspolrzedne.Name = "lblWspolrzedne"
        Me.lblWspolrzedne.Size = New System.Drawing.Size(25, 13)
        Me.lblWspolrzedne.TabIndex = 6
        Me.lblWspolrzedne.Text = "aaa"
        '
        'prgWczytywanie
        '
        Me.prgWczytywanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prgWczytywanie.Location = New System.Drawing.Point(106, 533)
        Me.prgWczytywanie.Name = "prgWczytywanie"
        Me.prgWczytywanie.Size = New System.Drawing.Size(604, 23)
        Me.prgWczytywanie.TabIndex = 4
        Me.prgWczytywanie.Visible = False
        '
        'lblWczytywanie
        '
        Me.lblWczytywanie.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWczytywanie.AutoSize = True
        Me.lblWczytywanie.Location = New System.Drawing.Point(3, 538)
        Me.lblWczytywanie.Name = "lblWczytywanie"
        Me.lblWczytywanie.Size = New System.Drawing.Size(79, 13)
        Me.lblWczytywanie.TabIndex = 3
        Me.lblWczytywanie.Text = "Wczytywanie..."
        Me.lblWczytywanie.Visible = False
        '
        'btnPomniejsz
        '
        Me.btnPomniejsz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPomniejsz.Location = New System.Drawing.Point(716, 504)
        Me.btnPomniejsz.Name = "btnPomniejsz"
        Me.btnPomniejsz.Size = New System.Drawing.Size(23, 23)
        Me.btnPomniejsz.TabIndex = 2
        Me.btnPomniejsz.Text = "-"
        Me.btnPomniejsz.UseVisualStyleBackColor = True
        '
        'btnPowieksz
        '
        Me.btnPowieksz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPowieksz.Location = New System.Drawing.Point(716, 533)
        Me.btnPowieksz.Name = "btnPowieksz"
        Me.btnPowieksz.Size = New System.Drawing.Size(23, 23)
        Me.btnPowieksz.TabIndex = 1
        Me.btnPowieksz.Text = "+"
        Me.btnPowieksz.UseVisualStyleBackColor = True
        '
        'mapMapa
        '
        Me.mapMapa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mapMapa.Location = New System.Drawing.Point(0, 27)
        Me.mapMapa.Name = "mapMapa"
        Me.mapMapa.Size = New System.Drawing.Size(742, 532)
        Me.mapMapa.TabIndex = 5
        '
        'wndOkno
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 581)
        Me.Controls.Add(Me.pnlMapa)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "wndOkno"
        Me.Text = "Mapa"
        Me.pnlMapa.ResumeLayout(False)
        Me.pnlMapa.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
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
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents stlPowiekszenie As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents stlElementy As ToolStripStatusLabel
    Friend WithEvents cbSzukaj As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents mnuNarzedzia As ToolStripMenuItem
    Friend WithEvents mnuSzukajObiektu As ToolStripMenuItem
    Friend WithEvents mnuSzukajAdresu As ToolStripMenuItem
End Class
