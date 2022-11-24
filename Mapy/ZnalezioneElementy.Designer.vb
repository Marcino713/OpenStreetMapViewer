<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndZnalezioneElementy
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lstElementy = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvWlasciwosci = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSzukaj = New System.Windows.Forms.TextBox()
        Me.btnSzukaj = New System.Windows.Forms.Button()
        Me.btnWikipedia = New System.Windows.Forms.Button()
        Me.btnStronaWWW = New System.Windows.Forms.Button()
        Me.btnWysrodkuj = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 37)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstElementy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lvWlasciwosci)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Size = New System.Drawing.Size(421, 412)
        Me.SplitContainer1.SplitterDistance = 146
        Me.SplitContainer1.TabIndex = 0
        '
        'lstElementy
        '
        Me.lstElementy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstElementy.FormattingEnabled = True
        Me.lstElementy.Location = New System.Drawing.Point(3, 25)
        Me.lstElementy.Name = "lstElementy"
        Me.lstElementy.Size = New System.Drawing.Size(415, 108)
        Me.lstElementy.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Elementy w pobliżu:"
        '
        'lvWlasciwosci
        '
        Me.lvWlasciwosci.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvWlasciwosci.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvWlasciwosci.Location = New System.Drawing.Point(3, 16)
        Me.lvWlasciwosci.Name = "lvWlasciwosci"
        Me.lvWlasciwosci.Size = New System.Drawing.Size(415, 243)
        Me.lvWlasciwosci.TabIndex = 1
        Me.lvWlasciwosci.UseCompatibleStateImageBehavior = False
        Me.lvWlasciwosci.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Klucz"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Wartość"
        Me.ColumnHeader2.Width = 200
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Właściwości elementu"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Nazwa:"
        '
        'txtSzukaj
        '
        Me.txtSzukaj.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSzukaj.Location = New System.Drawing.Point(49, 11)
        Me.txtSzukaj.Name = "txtSzukaj"
        Me.txtSzukaj.Size = New System.Drawing.Size(279, 20)
        Me.txtSzukaj.TabIndex = 2
        '
        'btnSzukaj
        '
        Me.btnSzukaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSzukaj.Location = New System.Drawing.Point(334, 9)
        Me.btnSzukaj.Name = "btnSzukaj"
        Me.btnSzukaj.Size = New System.Drawing.Size(75, 23)
        Me.btnSzukaj.TabIndex = 3
        Me.btnSzukaj.Text = "Szukaj"
        Me.btnSzukaj.UseVisualStyleBackColor = True
        '
        'btnWikipedia
        '
        Me.btnWikipedia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWikipedia.Enabled = False
        Me.btnWikipedia.Location = New System.Drawing.Point(3, 455)
        Me.btnWikipedia.Name = "btnWikipedia"
        Me.btnWikipedia.Size = New System.Drawing.Size(85, 23)
        Me.btnWikipedia.TabIndex = 4
        Me.btnWikipedia.Text = "Wikipedia"
        Me.btnWikipedia.UseVisualStyleBackColor = True
        '
        'btnStronaWWW
        '
        Me.btnStronaWWW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStronaWWW.Enabled = False
        Me.btnStronaWWW.Location = New System.Drawing.Point(94, 455)
        Me.btnStronaWWW.Name = "btnStronaWWW"
        Me.btnStronaWWW.Size = New System.Drawing.Size(85, 23)
        Me.btnStronaWWW.TabIndex = 5
        Me.btnStronaWWW.Text = "Strona WWW"
        Me.btnStronaWWW.UseVisualStyleBackColor = True
        '
        'btnWysrodkuj
        '
        Me.btnWysrodkuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWysrodkuj.Location = New System.Drawing.Point(185, 455)
        Me.btnWysrodkuj.Name = "btnWysrodkuj"
        Me.btnWysrodkuj.Size = New System.Drawing.Size(85, 23)
        Me.btnWysrodkuj.TabIndex = 6
        Me.btnWysrodkuj.Text = "Wyśrodkuj"
        Me.btnWysrodkuj.UseVisualStyleBackColor = True
        '
        'wndZnalezioneElementy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 481)
        Me.Controls.Add(Me.btnWysrodkuj)
        Me.Controls.Add(Me.btnStronaWWW)
        Me.Controls.Add(Me.btnWikipedia)
        Me.Controls.Add(Me.btnSzukaj)
        Me.Controls.Add(Me.txtSzukaj)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "wndZnalezioneElementy"
        Me.Text = "Znalezione elementy"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lstElementy As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lvWlasciwosci As ListView
    Friend WithEvents Label2 As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSzukaj As TextBox
    Friend WithEvents btnSzukaj As Button
    Friend WithEvents btnWikipedia As Button
    Friend WithEvents btnStronaWWW As Button
    Friend WithEvents btnWysrodkuj As Button
End Class
