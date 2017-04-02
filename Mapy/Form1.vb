Imports System.IO
Imports System.Threading

Friend Class wndOkno

    Private Const WSPOLCZYNNIK_POWIEKSZENIA As Single = 1.2F
    Friend wsp As Single
    Friend poziom As Integer = 0
    Private poczatek As New PointF(0F, 0F)
    Private koniec As New PointF(0F, 0F)
    Private mapa_wczytana As Boolean = False

    Private przesun_mape As Boolean = False
    Private przesun_pocz As Point
    Private rozmiar_okna As Size = New Size(0, 0)


    Private Sub wndOkno_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Okno = Me
        mapMapa.Cursor = New Cursor(My.Resources.grab.Handle)
        rozmiar_okna.Width = mapMapa.Width
        rozmiar_okna.Height = mapMapa.Height

        InicjalizujGrafike(mapMapa.Handle.ToInt32, mapMapa.Width, mapMapa.Height)
        InicjalizujProgram()

        If My.Application.CommandLineArgs.Count > 0 Then
            Dim sciezka As String = My.Application.CommandLineArgs(My.Application.CommandLineArgs.Count - 1)
            Me.Text = "Mapa - " & Path.GetFileName(sciezka)

            Dim t As New Thread(AddressOf OtworzMape)
            t.Start(sciezka)
        End If
    End Sub

    Friend UstawKontrolkiWczytywanie As New Action(AddressOf pUstawKontrolkiWczytywanie)
    Friend UstawKontrolkiPostep As New Action(Of Integer)(AddressOf pUstawKontrolkiPostep)
    Friend UstawKontrolkiKoniec As New Action(AddressOf pUstawKontrolkiKoniec)

    Private Sub pUstawKontrolkiWczytywanie()
        Me.lblWczytywanie.Visible = True
        Me.prgWczytywanie.Value = 0
        Me.prgWczytywanie.Visible = True
    End Sub

    Private Sub pUstawKontrolkiPostep(Postep As Integer)
        Me.prgWczytywanie.Value = Postep
        prgWczytywanie.Invalidate()
        'Application.DoEvents()
    End Sub

    Private Sub pUstawKontrolkiKoniec()
        Me.lblWczytywanie.Visible = False
        Me.prgWczytywanie.Value = 0
        Me.prgWczytywanie.Visible = False
        mapa_wczytana = True
        ObliczPowiekszenieWczytywanie()
        Rysuj()
    End Sub

    Private Sub ObliczPowiekszenieWczytywanie()
        wsp = Mapa.szerokosc / rozmiar_okna.Width

        If Mapa.wysokosc / wsp > rozmiar_okna.Height Then
            wsp = Mapa.wysokosc / rozmiar_okna.Height
            poczatek.X = -((rozmiar_okna.Width * wsp - Mapa.szerokosc) / 2)
            poczatek.Y = 0
            koniec.X = poczatek.X + wsp * rozmiar_okna.Width
            koniec.Y = Mapa.wysokosc
        Else
            poczatek.X = 0
            poczatek.Y = -((rozmiar_okna.Height * wsp - Mapa.wysokosc) / 2)
            koniec.X = Mapa.szerokosc
            koniec.Y = poczatek.Y + wsp * rozmiar_okna.Height
        End If

        UstawPoziom()

        PokazPoczatekKoniec()
        PokazWspolczynnik()
    End Sub

    Private Sub Rysuj()
        Dim d As Droga
        Dim w As Wezel
        Dim p As Point
        Dim macierz As Macierz3x3
        Dim ilosc As Integer = 0

        macierz = UtworzMacierzPrzesuniecia(-poczatek.X, -poczatek.Y) * UtworzMacierzSkalowania(1.0F / wsp, 1.0F / wsp)
        RozpocznijRysowanie
        UstawTransformacje(-poczatek.X, -poczatek.Y, 1.0F / wsp)

        'Rysuj krajobraz
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.atrybuty And DROGA_KRAJOBRAZ) = DROGA_KRAJOBRAZ Then
                If d.pedzel <> BRAK_PEDZLA AndAlso Pedzle(d.pedzel).Pedzel1 <> IntPtr.Zero AndAlso d.CzyZawiera(poczatek.X, poczatek.Y, koniec.X, koniec.Y) Then
                    WypelnijSciezke(d.linia1, Pedzle(d.pedzel).Pedzel1)
                    ilosc += 1
                End If
            End If
        Next

        'Rysuj pola
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.atrybuty And DROGA_POLE) = DROGA_POLE AndAlso (d.atrybuty And DROGA_KRAJOBRAZ) = 0 Then
                If d.pedzel <> BRAK_PEDZLA AndAlso Pedzle(d.pedzel).Pedzel1 <> IntPtr.Zero AndAlso d.CzyZawiera(poczatek.X, poczatek.Y, koniec.X, koniec.Y) Then
                    WypelnijSciezke(d.linia1, Pedzle(d.pedzel).Pedzel1)
                    ilosc += 1
                End If
            End If
        Next

        'Rysuj drogi
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.atrybuty And DROGA_POLE) = 0 Then
                If d.pedzel <> BRAK_PEDZLA AndAlso Pedzle(d.pedzel).Pedzel1 <> IntPtr.Zero AndAlso d.CzyZawiera(poczatek.X, poczatek.Y, koniec.X, koniec.Y) Then
                    RysujSciezke(d.linia1, Pedzle(d.pedzel).Pedzel1, Pedzle(d.pedzel).Grubosc1)
                    ilosc += 1
                End If
            End If
        Next

        'Rysuj punkty
        For i As Integer = 0 To Mapa.wezly.Length - 1
            w = Mapa.wezly(i)

            If w.pedzel <> BRAK_PEDZLA AndAlso Pedzle(w.pedzel).Pedzel1 <> IntPtr.Zero AndAlso w.CzyZawiera(poczatek.X, poczatek.Y, koniec.X, koniec.Y) Then
                p = w * macierz
                RysujObraz(Pedzle(w.pedzel).Pedzel1, p.X, p.Y)
                ilosc += 1
            End If
        Next

        ZakonczRysowanie
        lblIlosc.Text = ilosc.ToString

    End Sub

    Private Sub UstawPoziom()
        Dim poz As Integer

        If wsp < 0.00000883 Then
            poz = 1
        ElseIf wsp < 0.0000153
            poz = 2
        ElseIf wsp < 0.000022
            poz = 3
        ElseIf wsp < 0.0000316
            poz = 4
        ElseIf wsp < 0.0000456
            poz = 5
        ElseIf wsp < 0.0000656
            poz = 6
        ElseIf wsp < 0.0000945
            poz = 7
        ElseIf wsp < 0.000163
            poz = 8
        ElseIf wsp < 0.000235
            poz = 9
        Else
            poz = 10
        End If

        If poz <> poziom Then
            UstawPedzle(poz)
            poziom = poz
        End If

        PokazWspolczynnik()
    End Sub

    Private Sub Powieksz(x As Integer, y As Integer)
        Dim wsp2 As Single = wsp / WSPOLCZYNNIK_POWIEKSZENIA

        poczatek.X = poczatek.X + wsp * x - wsp2 * x
        poczatek.Y = poczatek.Y + wsp * y - wsp2 * y

        koniec.X = poczatek.X + wsp2 * mapMapa.Width
        koniec.Y = poczatek.Y + wsp2 * mapMapa.Height

        wsp = wsp2

        PokazPoczatekKoniec()
        PokazWspolczynnik()

        UstawPoziom()

        Rysuj()
    End Sub

    Private Sub Pomniejsz(x As Integer, y As Integer)
        Dim wsp2 As Single = wsp * WSPOLCZYNNIK_POWIEKSZENIA

        poczatek.X = poczatek.X + wsp * x - wsp2 * x
        poczatek.Y = poczatek.Y + wsp * y - wsp2 * y

        koniec.X = poczatek.X + wsp2 * mapMapa.Width
        koniec.Y = poczatek.Y + wsp2 * mapMapa.Height

        wsp = wsp2

        PokazPoczatekKoniec()
        PokazWspolczynnik()

        UstawPoziom()

        Rysuj()
    End Sub

    Private Sub btnPowieksz_Click() Handles btnPowieksz.Click
        Dim wsp2 As Single = wsp / WSPOLCZYNNIK_POWIEKSZENIA
        Dim rozmiar As Integer = mapMapa.Width

        poczatek.X += (wsp * rozmiar - wsp2 * rozmiar) / 2.0F
        rozmiar = mapMapa.Height
        poczatek.Y += (wsp * rozmiar - wsp2 * rozmiar) / 2.0F

        koniec.X = poczatek.X + wsp2 * mapMapa.Width
        koniec.Y = poczatek.Y + wsp2 * mapMapa.Height

        wsp = wsp2
        PokazPoczatekKoniec()
        PokazWspolczynnik()

        UstawPoziom()

        Rysuj()
    End Sub

    Private Sub btnPomniejsz_Click() Handles btnPomniejsz.Click
        Dim wsp2 As Single = wsp * WSPOLCZYNNIK_POWIEKSZENIA
        Dim rozmiar As Integer = mapMapa.Width

        poczatek.X -= (wsp2 * rozmiar - wsp * rozmiar) / 2.0F
        rozmiar = mapMapa.Height
        poczatek.Y -= (wsp2 * rozmiar - wsp * rozmiar) / 2.0F

        koniec.X = poczatek.X + wsp2 * mapMapa.Width
        koniec.Y = poczatek.Y + wsp2 * mapMapa.Height

        wsp = wsp2
        UstawPoziom()
        Rysuj()

        PokazPoczatekKoniec()
        PokazWspolczynnik()
    End Sub

    Private Sub pnlMapa_Resize() Handles pnlMapa.Resize
        If mapMapa.Width = 0 OrElse mapMapa.Height = 0 Then Exit Sub
        ZmienRozmiar(CUInt(mapMapa.Width), CUInt(mapMapa.Height))
        If Not mapa_wczytana Then Exit Sub

        Dim dx, dy As Integer
        dx = mapMapa.Width - rozmiar_okna.Width
        dy = mapMapa.Height - rozmiar_okna.Height

        If Not (rozmiar_okna.Width = 0 OrElse rozmiar_okna.Height = 0) Then
            poczatek.X -= Convert.ToSingle((wsp * dx) / 2.0F)
            poczatek.Y -= Convert.ToSingle((wsp * dy) / 2.0F)

            koniec.X = poczatek.X + wsp * mapMapa.Width
            koniec.Y = poczatek.Y + wsp * mapMapa.Height
        End If

        rozmiar_okna.Width = mapMapa.Width
        rozmiar_okna.Height = mapMapa.Height

        PrzesunPedzle(dx \ 2, dy \ 2)

        Rysuj()

        PokazPoczatekKoniec()
    End Sub

    Private Sub mapMapa_MouseDown(sender As Object, e As MouseEventArgs) Handles mapMapa.MouseDown
        mapMapa.Cursor = New Cursor(My.Resources.grabbing.Handle)
        przesun_pocz.X = e.X
        przesun_pocz.Y = e.Y
        przesun_mape = True
    End Sub

    Private Sub mapMapa_MouseUp(sender As Object, e As MouseEventArgs) Handles mapMapa.MouseUp
        mapMapa.Cursor = New Cursor(My.Resources.grab.Handle)
        PrzesunMape(New Point(e.X, e.Y))
        przesun_mape = False
    End Sub

    Private Sub mapMapa_MouseMove(sender As Object, e As MouseEventArgs) Handles mapMapa.MouseMove
        If przesun_mape Then PrzesunMape(New Point(e.X, e.Y))

        Dim x As Single = Mapa.poczatek_x + poczatek.X + wsp * e.X
        Dim y As Single = (Mapa.poczatek_y + poczatek.Y + wsp * e.Y) / 1.852F
        lblWspolrzedne.Text = y.ToString & ", " & x.ToString
    End Sub

    Private Sub PrzesunMape(PozycjaKurosora As Point)
        Dim x As Integer = PozycjaKurosora.X - przesun_pocz.X
        Dim y As Integer = PozycjaKurosora.Y - przesun_pocz.Y

        przesun_pocz.X = PozycjaKurosora.X
        przesun_pocz.Y = PozycjaKurosora.Y

        poczatek.X -= Convert.ToSingle(x * wsp)
        poczatek.Y -= Convert.ToSingle(y * wsp)

        koniec.X = poczatek.X + wsp * mapMapa.Width
        koniec.Y = poczatek.Y + wsp * mapMapa.Height

        PrzesunPedzle(x, y)
        Rysuj()

        PokazPoczatekKoniec()
    End Sub

    Private Sub pctMapa_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If e.Delta < 0 Then
            Pomniejsz(e.X, e.Y)
        Else
            Powieksz(e.X, e.Y)
        End If
    End Sub

    Private Sub wndOkno_FormClosed() Handles Me.FormClosed
        Posprzataj
    End Sub

    Private Sub pctMapa_Paint(sender As Object, e As PaintEventArgs) Handles mapMapa.Paint
        Rysuj()
    End Sub

    Private Sub PokazPoczatekKoniec()
        Dim l As Single
        Dim s As String = ""

        l = (Mapa.poczatek_y + poczatek.Y) / 1.852F
        s = "Początek: " & l.ToString & " "
        l = Mapa.poczatek_x + poczatek.X
        s &= l.ToString

        l = (Mapa.poczatek_y + koniec.Y) / 1.852F
        s &= " Koniec: " & l.ToString & " "
        l = Mapa.poczatek_x + koniec.X
        s &= l.ToString

        lblZakres.Text = s
    End Sub

    Private Sub PokazWspolczynnik()
        lblWsp.Text = "wsp: " & wsp.ToString & "/" & poziom.ToString
    End Sub
End Class