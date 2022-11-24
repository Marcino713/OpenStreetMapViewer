Imports System.IO
Imports System.Threading

Friend Class wndOkno

    Private Const NAZWA_PLIKU As String = "C:\Users\Marcin\Downloads\t.mapa" '"C:\Users\Marcin\Desktop\ZakopaneNowa.mapa" '"D:\Pliki\Mapy\Mapy\Zakopane.mapa" '"C:\Users\Marcin\Desktop\Pliki\Mapy\Mapy\Swarzędz.mapa"
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


        Dim p As New WczytywaniePedzli
        Dim kat As Kategoria() = p.WczytajPlik

        Dim sciezka As String
        If My.Application.CommandLineArgs.Count > 0 Then sciezka = My.Application.CommandLineArgs(My.Application.CommandLineArgs.Count - 1) Else sciezka = NAZWA_PLIKU
        Text = "Mapa - " & Path.GetFileName(sciezka)

        Dim t As New Thread(AddressOf OtworzMape)
        t.Start(New ParametryOtwierania With {.SciezkaPliku = sciezka, .Kategorie = kat})
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

    Friend Sub Rysuj()
        Dim d As Droga
        Dim w As Wezel
        Dim p As Point
        Dim macierz As Macierz3x3
        Dim ilosc As Integer = 0
        Dim wolne As Boolean() = Nothing
        Dim pocz_px As PointF
        Dim konc_px As PointF
        Dim kratki_x As Integer = 0
        Dim kratki_y As Integer = 0
        Dim dodaj_x As Single = POWIEKSZENIE_GRANICY_X * wsp
        Dim dodaj_y As Single = POWIEKSZENIE_GRANICY_Y * wsp
        Dim pocz As PointF = New PointF(poczatek.X - dodaj_x, poczatek.Y - dodaj_y)
        Dim konc As PointF = New PointF(koniec.X + dodaj_x, koniec.Y + dodaj_y)

        macierz = UtworzMacierzPrzesuniecia(-poczatek.X, -poczatek.Y) * UtworzMacierzSkalowania(1.0F / wsp, 1.0F / wsp)
        RozpocznijRysowanie
        UstawTransformacje(-poczatek.X, -poczatek.Y, 1.0F / wsp)

        If SprawdzajGestoscWezlow Then
            pocz_px = New PointF(-POWIEKSZENIE_GRANICY_X, -POWIEKSZENIE_GRANICY_Y)
            konc_px = New PointF(mapMapa.Width + POWIEKSZENIE_GRANICY_X, mapMapa.Height + POWIEKSZENIE_GRANICY_Y)
            kratki_x = CInt(Math.Ceiling((konc_px.X - pocz_px.X) / ROZMIAR_SIATKI_X))
            kratki_y = CInt(Math.Ceiling((konc_px.Y - pocz_px.Y) / ROZMIAR_SIATKI_Y))
            ReDim wolne(kratki_x * kratki_y - 1)
            For i As Integer = 0 To wolne.Length - 1
                wolne(i) = True
            Next
        End If

        'Rysuj krajobraz
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.danem.flagi And DROGA_KRAJOBRAZ) = DROGA_KRAJOBRAZ Then
                If d.pedzel.PedzelTlo <> IntPtr.Zero AndAlso d.CzyZawiera(pocz.X, pocz.Y, konc.X, konc.Y) Then
                    WypelnijSciezke(d.linia1, d.pedzel.PedzelTlo)
                    ilosc += 1
                End If

                If d.pedzel.PedzelRamka <> IntPtr.Zero Then
                    RysujSciezke(d.linia1, d.pedzel.PedzelRamka)
                End If

                If d.pedzel.PedzelTekst <> IntPtr.Zero AndAlso d.tekst <> "" AndAlso d.RysujTekst Then
                    Dim rys As Boolean = True
                    p = New PointF(d.min_x + (d.max_x - d.min_x) / 2, d.min_y + (d.max_y - d.min_y) / 2) * macierz

                    If SprawdzajGestoscWezlow Then
                        If p.X < konc_px.X And p.X > pocz_px.X And p.Y < konc_px.Y And p.Y > pocz_px.Y Then
                            Dim sx As Integer = CInt((p.X - pocz_px.X) / ROZMIAR_SIATKI_X)
                            Dim sy As Integer = CInt((p.Y - pocz_px.Y) / ROZMIAR_SIATKI_Y)

                            Dim ix As Integer = sy * kratki_x + sx
                            If Not wolne(ix) Then
                                rys = False
                            End If
                            wolne(ix) = False
                        Else
                            rys = False
                        End If
                    End If

                    If rys Then
                        RysujTekst(d.tekst, d.pedzel.PedzelTekst, p.X - 38, p.Y - 13)
                        ilosc += 1
                    End If
                End If
            End If
        Next

        'Rysuj pola
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.danem.flagi And DROGA_POLE) = DROGA_POLE AndAlso (d.danem.flagi And DROGA_KRAJOBRAZ) = 0 Then
                If d.pedzel.PedzelTlo <> IntPtr.Zero AndAlso d.CzyZawiera(pocz.X, pocz.Y, konc.X, konc.Y) Then
                    WypelnijSciezke(d.linia1, d.pedzel.PedzelTlo)
                    ilosc += 1

                    If d.pedzel.PedzelRamka <> IntPtr.Zero Then
                        RysujSciezke(d.linia1, d.pedzel.PedzelRamka)
                    End If

                    If d.pedzel.PedzelTekst <> IntPtr.Zero AndAlso d.tekst <> "" AndAlso d.RysujTekst Then
                        Dim rys As Boolean = True
                        p = New PointF(d.min_x + (d.max_x - d.min_x) / 2, d.min_y + (d.max_y - d.min_y) / 2) * macierz

                        If SprawdzajGestoscWezlow Then
                            If p.X < konc_px.X And p.X > pocz_px.X And p.Y < konc_px.Y And p.Y > pocz_px.Y Then
                                Dim sx As Integer = CInt((p.X - pocz_px.X) / ROZMIAR_SIATKI_X)
                                Dim sy As Integer = CInt((p.Y - pocz_px.Y) / ROZMIAR_SIATKI_Y)

                                Dim ix As Integer = sy * kratki_x + sx
                                If Not wolne(ix) Then
                                    rys = False
                                End If
                                wolne(ix) = False
                            Else
                                rys = False
                            End If
                        End If

                        If rys Then
                            RysujTekst(d.tekst, d.pedzel.PedzelTekst, p.X - 38, p.Y - 13)
                            ilosc += 1
                        End If
                    End If

                End If
            End If
        Next

        'Rysuj drogi
        For i As Integer = 0 To Mapa.drogi.Length - 1
            d = Mapa.drogi(i)

            If (d.danem.flagi And DROGA_POLE) = 0 Then
                If d.pedzel.PedzelTlo <> IntPtr.Zero AndAlso d.CzyZawiera(pocz.X, pocz.Y, konc.X, konc.Y) Then
                    Dim kropki As Integer = 0

                    If d.pedzel.Kropkowanie > 0 Then
                        kropki = Kropkowania(d.pedzel.Kropkowanie - 1)
                    End If

                    If d.pedzel.PedzelRamka <> IntPtr.Zero Then
                        RysujSciezke(d.linia1, d.pedzel.PedzelRamka, d.pedzel.Grubosc + 1)
                    End If

                    RysujSciezke(d.linia1, d.pedzel.PedzelTlo, d.pedzel.Grubosc, kropki)
                    ilosc += 1


                    'Tekst
                    If d.Punkty IsNot Nothing AndAlso d.pedzel.PedzelTekst <> IntPtr.Zero AndAlso d.tekst <> "" AndAlso d.RysujTekst Then
                        Dim ptl As New List(Of PointF)
                        Dim ptt As Point
                        For j As Integer = 0 To d.Punkty.Length - 1
                            ptt = d.Punkty(j) * macierz
                            ptl.Add(New PointF(ptt.X, ptt.Y))
                        Next

                        Dim pt As PointF() = ptl.ToArray()

                        Dim dl As Double
                        Dim dx, dy As Double
                        Dim dlx, dly As Double
                        Dim poz As Double = POCZATEK_TEKSTU
                        Dim ix As Integer = 0
                        Dim l As Integer = pt.Length - 1
                        Dim kat As Double

                        Dim pcz As Point = pocz * macierz
                        Dim knc As Point = konc * macierz

                        Do
                            dlx = pt(ix + 1).X - pt(ix).X
                            dly = pt(ix + 1).Y - pt(ix).Y
                            dl = Math.Sqrt(dlx * dlx + dly * dly)
                            dx = dlx / dl
                            dy = dly / dl
                            If Math.Abs(dlx) < 1.0 Then kat = 90 Else kat = Math.Atan(dly / dlx) * 57.2957795

                            Do
                                If poz < dl Then

                                    Dim pk As New PointF(CSng(pt(ix).X + dx * poz), CSng(pt(ix).Y + dy * poz)) ' - 13) 36
                                    Dim r As Boolean = True
                                    If pk.X < pcz.X Then r = False
                                    If pk.X > knc.X Then r = False
                                    If pk.Y < pcz.Y Then r = False
                                    If pk.Y > knc.Y Then r = False

                                    If r Then

                                        If d.pedzel.Grubosc < 7 Then
                                            pk.Y -= 10
                                            pk.X -= 10
                                        End If
                                        Dim rys As Boolean = True


                                        If SprawdzajGestoscWezlow Then
                                            If p.X < konc_px.X And p.X > pocz_px.X And p.Y < konc_px.Y And p.Y > pocz_px.Y Then
                                                Dim sx As Integer = CInt((p.X - pocz_px.X) / ROZMIAR_SIATKI_X)
                                                Dim sy As Integer = CInt((p.Y - pocz_px.Y) / ROZMIAR_SIATKI_Y)

                                                Dim ix2 As Integer = sy * kratki_x + sx
                                                If Not wolne(ix2) Then
                                                    rys = False
                                                End If
                                                wolne(ix2) = False
                                            Else
                                                rys = False
                                            End If
                                        End If



                                        If rys Then RysujTekst(d.tekst, d.pedzel.PedzelTekst, pk.X - 100, pk.Y, CSng(kat), 200, True)
                                        ilosc += 1
                                    End If

                                    poz += ODLEGLOSC_TEKSTU
                                Else
                                    poz -= dl
                                    Exit Do
                                End If
                            Loop

                            ix += 1
                            If ix >= l Then Exit Do
                        Loop
                    End If

                End If
            End If
        Next

        'Rysuj zaznaczenie
        If ZaznaczonyElement IsNot Nothing Then
            Dim tp As Type = ZaznaczonyElement.GetType()
            If tp.Name = "Droga" Then
                Dim dr As Droga = CType(ZaznaczonyElement, Droga)
                If (dr.danem.flagi And DROGA_POLE) = 0 Then
                    RysujSciezke(dr.linia1, PedzelZaznaczenie, 5)
                Else
                    WypelnijSciezke(dr.linia1, PedzelZaznaczenie)
                End If
            End If

            If tp.Name = "Wezel" Then
                Dim wz As Wezel = CType(ZaznaczonyElement, Wezel)
                p = wz * macierz
                RysujObraz(PedzelZaznaczeniePunkt, p.X, p.Y)
            End If
        End If

        'Rysuj punkty
        For i As Integer = 0 To Mapa.wezly.Length - 1
            w = Mapa.wezly(i)

            If w.CzyZawiera(pocz.X, pocz.Y, konc.X, konc.Y) Then
                p = w * macierz
                Dim przesun As Boolean = False
                Dim rys As Boolean = True

                If SprawdzajGestoscWezlow Then
                    If p.X < konc_px.X And p.X > pocz_px.X And p.Y < konc_px.Y And p.Y > pocz_px.Y Then
                        Dim sx As Integer = CInt((p.X - pocz_px.X) / ROZMIAR_SIATKI_X)
                        Dim sy As Integer = CInt((p.Y - pocz_px.Y) / ROZMIAR_SIATKI_Y)

                        Dim ix As Integer = sy * kratki_x + sx
                        If Not wolne(ix) Then
                            rys = False
                        End If
                        wolne(ix) = False
                    Else
                        rys = False
                    End If
                End If

                If w.pedzel.PedzelTlo <> IntPtr.Zero And rys Then
                    RysujObraz(w.pedzel.PedzelTlo, p.X, p.Y)
                    przesun = True
                    ilosc += 1
                End If

                If w.pedzel.PedzelTekst <> IntPtr.Zero AndAlso w.tekst <> "" And rys Then
                    p = New PointF(w.x, w.y) * macierz
                    If przesun Then p.Y += 10
                    RysujTekst(w.tekst, w.pedzel.PedzelTekst, p.X - 38, p.Y)
                    ilosc += 1
                End If

            End If
        Next

        ZakonczRysowanie
        lblIlosc.Text = ilosc.ToString
        stlElementy.Text = ilosc.ToString

    End Sub

    Private Sub UstawPoziom()
        Dim poz As Integer

        If wsp < 0.00000883 Then
            poz = 1
        ElseIf wsp < 0.0000153 Then
            poz = 2
        ElseIf wsp < 0.000022 Then
            poz = 3
        ElseIf wsp < 0.0000316 Then
            poz = 4
        ElseIf wsp < 0.0000456 Then
            poz = 5
        ElseIf wsp < 0.0000656 Then
            poz = 6
        ElseIf wsp < 0.0000945 Then
            poz = 7
        ElseIf wsp < 0.000163 Then
            poz = 8
        ElseIf wsp < 0.000235 Then
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
        stlPowiekszenie.Text = poziom.ToString
    End Sub

    Private Sub mapMapa_MouseClick(sender As Object, e As MouseEventArgs) Handles mapMapa.MouseClick
        If Not cbSzukaj.Checked Then Exit Sub

        Dim x As Single = poczatek.X + wsp * e.X
        Dim y As Single = (poczatek.Y + wsp * e.Y)
        Dim minx As Single = x - ZAKRES_X
        Dim maxx As Single = x + ZAKRES_X
        Dim miny As Single = y - ZAKRES_Y
        Dim maxy As Single = y + ZAKRES_Y

        Dim Wezly As New List(Of Wezel)
        Dim Drogi As New List(Of Droga)

        For i As Integer = 0 To Mapa.wezly.Length - 1
            If Mapa.wezly(i).CzyZawiera(minx, miny, maxx, maxy) AndAlso Mapa.wezly(i).Wartosci IsNot Nothing Then Wezly.Add(Mapa.wezly(i))
        Next

        For i As Integer = 0 To Mapa.drogi.Length - 1
            If Mapa.drogi(i).CzyZawiera(minx, miny, maxx, maxy) AndAlso Mapa.drogi(i).Wartosci IsNot Nothing Then Drogi.Add(Mapa.drogi(i))
        Next

        PokazOknoWyszukiwania(Wezly, Drogi)
        cbSzukaj.Checked = False

    End Sub

    Private Sub cbSzukaj_CheckedChanged(sender As Object, e As EventArgs) Handles cbSzukaj.CheckedChanged
        If cbSzukaj.Checked Then mapMapa.Cursor = Cursors.Help Else mapMapa.Cursor = New Cursor(My.Resources.grab.Handle)
    End Sub

    Private Sub mnuSzukajObiektu_Click() Handles mnuSzukajObiektu.Click
        PokazOknoWyszukiwania(New List(Of Wezel), New List(Of Droga))
    End Sub

    Private Sub mnuSzukajAdresu_Click() Handles mnuSzukajAdresu.Click
        PokazOknoWyszAdresu()
    End Sub
End Class