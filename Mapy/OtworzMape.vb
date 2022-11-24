Imports System.Xml

Partial Friend Module App

    'Private aaa As Boolean = False

    Friend Class ListaPunktow
        Friend Punkty As New List(Of Wezel)
        Friend Zamknieta As Boolean = True
    End Class

    '-------------------------------------------------------------
    'Dlugosc geograficzna     lon    x
    'Szerokosc geograficzna   lat    y
    '-------------------------------------------------------------

    Friend Sub OtworzMape(Parametry As Object)
        Dim par As ParametryOtwierania = CType(Parametry, ParametryOtwierania)

        Dim Sciezka As String = par.SciezkaPliku
        Okno.Invoke(Okno.UstawKontrolkiWczytywanie)
        Dim plik As New XmlDocument()
        plik.Load(Sciezka)

        Dim el As XmlNodeList = plik.FirstChild.NextSibling.ChildNodes
        Dim elem As XmlNode = el(0)
        Dim l As Integer = el.Count - 1
        Dim n As Wezel
        Dim d As Droga
        Dim atr As XmlAttributeCollection
        Dim wezly As New Hashtable
        Dim drogi As New Hashtable
        Dim id As String = ""
        Dim minx, miny, maxx, maxy, wys As Single

        'Zakres mapy
        For i As Integer = 0 To l

            If elem.Name = "bounds" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "minlat" : miny = Single.Parse(elem.Attributes(j).Value.Replace("."c, ","c))
                        Case "minlon" : minx = Single.Parse(elem.Attributes(j).Value.Replace("."c, ","c))
                        Case "maxlat" : maxy = Single.Parse(elem.Attributes(j).Value.Replace("."c, ","c))
                        Case "maxlon" : maxx = Single.Parse(elem.Attributes(j).Value.Replace("."c, ","c))
                    End Select
                Next

                PrzeliczWspolrzedne(miny, minx)
                PrzeliczWspolrzedne(maxy, maxx)

                Mapa.poczatek_x = minx
                Mapa.poczatek_y = miny
                Mapa.szerokosc = maxx - minx
                Mapa.wysokosc = maxy - miny
                wys = Mapa.wysokosc

                Exit For
            End If

            elem = elem.NextSibling
        Next

        Okno.Invoke(Okno.UstawKontrolkiPostep, 1)

        'Wezly
        elem = el(0)

        For i As Integer = 0 To l

            If elem.Name = "node" Then

                atr = elem.Attributes
                n = New Wezel

                For j As Integer = 0 To atr.Count - 1
                    Select Case atr(j).Name
                        Case "id" : id = atr(j).Value
                        Case "lat" : n.y = Single.Parse(atr(j).Value.Replace("."c, ","c))
                        Case "lon" : n.x = Single.Parse(atr(j).Value.Replace("."c, ","c))
                    End Select
                Next

                PrzeliczWspolrzedne(n.y, n.x)

                n.x = n.x - minx
                n.y = wys - (n.y - miny)
                n.id = id

                If elem.HasChildNodes Then
                    CzytajWezel(elem, n, par.Kategorie)
                End If

                wezly.Add(id, n)

            End If

            elem = elem.NextSibling
        Next

        'Okno.UstawKontrolkiPostep(50)

        'Drogi
        elem = el(0)

        For i As Integer = 0 To l

            If elem.Name = "way" Then
                d = New Droga
                Dim pkt As New ListaPunktow

                atr = elem.Attributes
                For j As Integer = 0 To atr.Count - 1
                    Select Case atr(j).Name
                        Case "id" : id = atr(j).Value
                    End Select
                Next

                'If id = "174899239" Then
                '    Dim a As Integer = 0
                '    a += 1
                'End If

                CzytajDroge(elem, d, wezly, par.Kategorie, pkt)
                drogi.Add(id, pkt)

            End If

            elem = elem.NextSibling

        Next

        'Relacje
        elem = el(0)

        For i As Integer = 0 To l

            If elem.Name = "relation" Then

                'Dim xm As XmlNode = elem.Attributes.GetNamedItem("id")
                'If xm.Value = "3351769" Then
                '    Dim a As Integer = 0
                '    a += 1
                '    aaa = True
                'End If

                d = New Droga
                CzytajRelacje(elem, d, wezly, drogi, par.Kategorie)
                'aaa = False
            End If

            elem = elem.NextSibling

        Next

        Okno.Invoke(Okno.UstawKontrolkiKoniec)

    End Sub

    Friend Sub CzytajWezel(ByRef Xml As XmlNode, ByRef Node As Wezel, Kategorie As Kategoria())
        Dim l As Integer = Xml.ChildNodes.Count - 1
        Dim elem As XmlNode = Xml.FirstChild
        Dim k As String = ""
        Dim v As String = ""
        Dim nazwa As String = ""
        Dim nr As String = ""
        Dim wys As String = ""
        Dim dm As New List(Of DaneMalowania)
        Dim wartosci As New List(Of KluczWartosc)

        For i As Integer = 0 To l

            If elem.Name = "tag" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "k" : k = elem.Attributes(j).Value
                        Case "v" : v = elem.Attributes(j).Value
                    End Select
                Next

                wartosci.Add(New KluczWartosc(k, v))

                For m As Integer = 0 To Kategorie.Length - 1
                    If Kategorie(m).Nazwa = k Then

                        For n As Integer = 0 To Kategorie(m).Typy.Length - 1
                            If Kategorie(m).Typy(n).typ = "*" OrElse Kategorie(m).Typy(n).typ = v Then
                                dm.Add(Kategorie(m).Typy(n))
                            End If
                        Next

                    End If
                Next

                Select Case k
                    Case "addr:housenumber"
                        nr = v
                        Node.NrDomu = v

                    Case "addr:street"
                        Node.Ulica = v

                    Case "addr:place"
                        If Node.Ulica = "" Then Node.Ulica = v

                    Case "name"
                        nazwa = v

                    Case "ele"
                        wys = v

                End Select

            End If

            elem = elem.NextSibling

        Next

        If nazwa <> "" Then Node.tekst = nazwa Else Node.tekst = nr
        If wys <> "" Then Node.tekst &= vbCrLf & wys & " m"
        If wartosci.Count > 0 Then Node.Wartosci = wartosci.ToArray()

        If dm.Count > 0 Then
            Node.danem = (From d In dm Order By d.priotytet Descending Select d).FirstOrDefault()
            Mapa.wezly.Dodaj(Node)
        End If

    End Sub

    Friend Sub CzytajDroge(ByRef Xml As XmlNode, ByRef Way As Droga, ByRef wezly As Hashtable, ByRef kategorie As Kategoria(), ByRef Punkty As ListaPunktow)
        Dim l As Integer = Xml.ChildNodes.Count - 1
        Dim elem As XmlNode = Xml.FirstChild
        Dim n As Wezel
        Dim pierwszy As Boolean = True
        Dim pierwszy_wezel As String = ""
        Dim ostatni_wezel As String = ""
        Dim k As String = ""
        Dim v As String = ""
        Dim sprawdz_rozmiar As Boolean = False
        Dim nazwa As String = ""
        Dim nr As String = ""
        Dim wartosci As New List(Of KluczWartosc)

        'Dane
        Dim dane As New List(Of DaneMalowania)

        For i As Integer = 0 To l

            If elem.Name = "tag" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "k" : k = elem.Attributes(j).Value
                        Case "v" : v = elem.Attributes(j).Value
                    End Select
                Next

                wartosci.Add(New KluczWartosc(k, v))

                Select Case k
                    Case "addr:housenumber"
                        nr = v
                        Way.NrDomu = v

                    Case "addr:street"
                        Way.Ulica = v

                    Case "addr:place"
                        If Way.Ulica = "" Then Way.Ulica = v

                    Case "name"
                        nazwa = v

                    Case "area"

                        Select Case v
                            Case "yes"
                                'Way.danem.flagi = Way.danem.flagi Or DROGA_POLE
                        End Select
                End Select

                'Przyporzadkuj pedzle
                For m As Integer = 0 To kategorie.Length - 1
                    If kategorie(m).Nazwa = k Then

                        For o As Integer = 0 To kategorie(m).Typy.Length - 1
                            If kategorie(m).Typy(o).typ = "*" OrElse kategorie(m).Typy(o).typ = v Then
                                dane.Add(kategorie(m).Typy(o))
                            End If

                        Next
                    End If
                Next

            End If

            elem = elem.NextSibling
        Next

        'Wezly
        'Dim Punkty As New List(Of Wezel)
        elem = Xml.FirstChild

        For i As Integer = 0 To l

            If elem.Name = "nd" Then
                ostatni_wezel = elem.Attributes(0).Value
                n = CType(wezly(ostatni_wezel), Wezel)
                Punkty.Punkty.Add(n)

                If n.x < Way.min_x Then Way.min_x = n.x
                If n.x > Way.max_x Then Way.max_x = n.x
                If n.y < Way.min_y Then Way.min_y = n.y
                If n.y > Way.max_y Then Way.max_y = n.y

                If pierwszy Then
                    Way.linia1 = UtworzGeometrieSciezka
                    UtworzFigure(n.x, n.y)
                    pierwszy_wezel = ostatni_wezel
                    pierwszy = False
                Else
                    DodajLinie(n.x, n.y)
                End If

            End If

            elem = elem.NextSibling
        Next

        Way.Punkty = Punkty.Punkty.ToArray()
        Punkty.Zamknieta = (pierwszy_wezel = ostatni_wezel)

        If dane.Count = 0 Then
            Exit Sub
        End If

        If nazwa <> "" Then
            Way.tekst = nazwa
        Else
            Way.tekst = nr
        End If

        If wartosci.Count > 0 Then Way.Wartosci = wartosci.ToArray()

        If Way.danem IsNot Nothing Then
            If (Way.danem.flagi And DROGA_POLE) = DROGA_POLE OrElse (Way.danem.flagi And DROGA_KRAJOBRAZ) = DROGA_KRAJOBRAZ OrElse pierwszy_wezel = ostatni_wezel Then
                ZakonczFigure(ZakonczenieFigury.Zamknieta)
            Else
                ZakonczFigure(ZakonczenieFigury.Otwarta)
            End If
        Else
            ZakonczFigure(ZakonczenieFigury.Otwarta)
        End If
        ZakonczGeometrie

        Dim punkt As DaneMalowania = (From p In dane Where (p.flagi And DROGA_PUNKT) = DROGA_PUNKT Order By p.priotytet Descending Select p).FirstOrDefault()
        Dim wz As Wezel = Nothing
        If punkt IsNot Nothing Then
            wz = New Wezel() With {.danem = punkt, .x = (Way.max_x - Way.min_x) / 2 + Way.min_x, .y = (Way.max_y - Way.min_y) / 2 + Way.min_y, .tekst = Way.tekst}
            Mapa.wezly.Dodaj(wz)
            Way.RysujTekst = False
        End If

        Dim dm As DaneMalowania()
        Dim priorytety As Integer() = (From p In dane Where (p.flagi And DROGA_PUNKT) = 0 Select p.priotytet Order By priotytet Descending).Distinct.ToArray()
        Dim dodano As Boolean = False
        If priorytety IsNot Nothing AndAlso priorytety.Length > 0 Then
            If priorytety.Length = 1 Then
                dm = dane.ToArray()
            Else
                dm = dane.Where(Function(p As DaneMalowania) p.priotytet = priorytety(0)).ToArray()
            End If

            Dim pr_kat As Integer = dm.Max(Function(d As DaneMalowania) d.PrKategorii)

            For i As Integer = 0 To dm.Length - 1
                If (dm(i).flagi And DROGA_PUNKT) = 0 Then
                    Dim dr As New Droga()
                    dr.min_x = Way.min_x
                    dr.max_x = Way.max_x
                    dr.min_y = Way.min_y
                    dr.max_y = Way.max_y
                    dr.tekst = Way.tekst
                    dr.linia1 = Way.linia1
                    dr.RysujTekst = Way.RysujTekst
                    dr.danem = dm(i)

                    If (Not dodano) And dm(i).PrKategorii = pr_kat Then
                        dodano = True
                        dr.Wartosci = Way.Wartosci
                        dr.NrDomu = Way.NrDomu
                        dr.Ulica = Way.Ulica
                        dr.Punkty = Way.Punkty
                    End If

                    Mapa.drogi.Dodaj(dr)
                End If
            Next

        End If

        If Not dodano Then
            If wz IsNot Nothing Then wz.Wartosci = Way.Wartosci
        End If

    End Sub

    Friend Sub CzytajRelacje(ByRef Xml As XmlNode, ByRef Relation As Droga, ByRef wezly As Hashtable, ByRef drogi As Hashtable, ByRef kategorie As Kategoria())
        Dim l As Integer = Xml.ChildNodes.Count - 1
        Dim elem As XmlNode = Xml.FirstChild
        Dim n As Wezel
        Dim pierwszy As Boolean = True
        Dim pierwszy_wezel As String = ""
        Dim ostatni_wezel As String = ""
        Dim k As String = ""
        Dim v As String = ""
        Dim sprawdz_rozmiar As Boolean = False
        Dim nazwa As String = ""
        Dim nr As String = ""
        Dim wartosci As New List(Of KluczWartosc)

        'Dane
        Dim dane As New List(Of DaneMalowania)

        For i As Integer = 0 To l

            If elem.Name = "tag" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "k" : k = elem.Attributes(j).Value
                        Case "v" : v = elem.Attributes(j).Value
                    End Select
                Next

                wartosci.Add(New KluczWartosc(k, v))

                Select Case k

                    Case "name"
                        nazwa = v


                End Select

                'Przyporzadkuj pedzle
                For m As Integer = 0 To kategorie.Length - 1
                    If kategorie(m).Nazwa = k Then

                        For o As Integer = 0 To kategorie(m).Typy.Length - 1
                            If kategorie(m).Typy(o).typ = "*" OrElse kategorie(m).Typy(o).typ = v Then
                                dane.Add(kategorie(m).Typy(o))
                            End If

                        Next
                    End If
                Next

            End If

            elem = elem.NextSibling
        Next

        If dane.Count = 0 Then
            'Way = Nothing
            Exit Sub
        End If

        'If nazwa <> "" Then
        Relation.tekst = nazwa
        'Else
        '    Way.tekst = nr
        'End If

        If wartosci.Count > 0 Then Relation.Wartosci = wartosci.ToArray()

        'Wezly
        Dim Punkty As New List(Of Wezel)
        elem = Xml.FirstChild

        Relation.linia1 = UtworzGeometrieSciezka

        Dim linie As New List(Of ListaPunktow)

        For i As Integer = 0 To l

            If elem.Name = "member" Then

                Dim typ, id, role As String
                typ = ""
                id = ""
                role = ""

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "type" : typ = elem.Attributes(j).Value
                        Case "ref" : id = elem.Attributes(j).Value
                        Case "role" : role = elem.Attributes(j).Value
                    End Select
                Next

                If typ = "way" Then    'And (role = "" Or role = "outer")


                    'If id = "174685402" And aaa Then
                    '    Dim a As Integer = 0
                    '    a += 1
                    'End If

                    'If aaa Then Debug.Print(id)

                    Dim d As ListaPunktow = CType(drogi(id), ListaPunktow)
                    If d Is Nothing Then GoTo petla_dalej
                    'If d.Punkty Is Nothing Then Continue For

                    linie.Add(d)

                    If d.Punkty.Count > 0 Then
                        '    UtworzFigure(d.Punkty(0).x, d.Punkty(0).y)


                        For j As Integer = 1 To d.Punkty.Count - 1
                            n = d.Punkty(j)


                            If n.x < Relation.min_x Then Relation.min_x = n.x
                            If n.x > Relation.max_x Then Relation.max_x = n.x
                            If n.y < Relation.min_y Then Relation.min_y = n.y
                            If n.y > Relation.max_y Then Relation.max_y = n.y

                            '        DodajLinie(n.x, n.y)
                        Next

                        '    If d.Zamknieta Then
                        '        ZakonczFigure(ZakonczenieFigury.Zamknieta)
                        '    Else
                        '        ZakonczFigure(ZakonczenieFigury.Otwarta)
                        '    End If

                    End If
                End If

            End If

petla_dalej:
            elem = elem.NextSibling
        Next





        While linie.Count > 0
            Dim lp As ListaPunktow = linie(0)
            linie.RemoveAt(0)
            Dim linia As New List(Of Wezel)

            Dim kont As Boolean = True
            Dim id As String = lp.Punkty(lp.Punkty.Count - 1).id
            For i As Integer = 0 To lp.Punkty.Count - 1
                linia.Add(lp.Punkty(i))
            Next

            While kont

                Dim a As ListaPunktow = (From p In linie Where p.Punkty(0).id = id OrElse p.Punkty(p.Punkty.Count - 1).id = id).FirstOrDefault
                If a Is Nothing Then
                    kont = False
                Else
                    linie.Remove(a)
                    Dim pocz, konc, zm As Integer
                    If a.Punkty(0).id = id Then
                        pocz = 1
                        konc = a.Punkty.Count - 1
                        zm = 1
                        id = a.Punkty(a.Punkty.Count - 1).id
                    Else
                        pocz = a.Punkty.Count - 2
                        konc = 0
                        zm = -1
                        id = a.Punkty(0).id
                    End If
                    For i As Integer = pocz To konc Step zm
                        linia.Add(a.Punkty(i))
                    Next
                End If

            End While

            kont = True
            Dim linia2 As New List(Of Wezel)
            id = lp.Punkty(0).id

            While kont

                Dim a As ListaPunktow = (From p In linie Where p.Punkty(0).id = id OrElse p.Punkty(p.Punkty.Count - 1).id = id).FirstOrDefault
                If a Is Nothing Then
                    kont = False
                Else
                    linie.Remove(a)
                    Dim pocz, konc, zm As Integer
                    If a.Punkty(0).id = id Then
                        pocz = 1
                        konc = a.Punkty.Count - 1
                        zm = 1
                        id = a.Punkty(a.Punkty.Count - 1).id
                    Else
                        pocz = a.Punkty.Count - 2
                        konc = 0
                        zm = -1
                        id = a.Punkty(0).id
                    End If
                    For i As Integer = pocz To konc Step zm
                        linia2.Add(a.Punkty(i))
                    Next
                End If

            End While

            Dim pcz As Wezel = Nothing
            Dim knc As Wezel = Nothing

            If linia2.Count > 0 Then
                pcz = linia2(linia2.Count - 1)
            Else
                pcz = linia(0)
            End If

            knc = linia(linia.Count - 1)



            'If d.Punkty.Count > 0 Then
            UtworzFigure(pcz.x, pcz.y)


            If linia2.Count > 0 Then
                For j As Integer = linia2.Count - 2 To 0 Step -1
                    DodajLinie(linia2(j).x, linia2(j).y)
                Next
            End If

            For j As Integer = 0 To linia.Count - 1
                DodajLinie(linia(j).x, linia(j).y)
            Next

            If pcz.id = knc.id Then
                ZakonczFigure(ZakonczenieFigury.Zamknieta)
            Else
                ZakonczFigure(ZakonczenieFigury.Otwarta)
            End If

            'End If

        End While







        'Relation.Punkty = Punkty.ToArray()

        'If Relation.danem IsNot Nothing Then
        '    If (Relation.danem.flagi And DROGA_POLE) = DROGA_POLE OrElse (Relation.danem.flagi And DROGA_KRAJOBRAZ) = DROGA_KRAJOBRAZ OrElse pierwszy_wezel = ostatni_wezel Then
        '        ZakonczFigure(ZakonczenieFigury.Zamknieta)
        '    Else
        '        ZakonczFigure(ZakonczenieFigury.Otwarta)
        '    End If
        'Else
        '    ZakonczFigure(ZakonczenieFigury.Otwarta)
        'End If
        ZakonczGeometrie

        'Dim punkt As DaneMalowania = (From p In dane Where (p.flagi And DROGA_PUNKT) = DROGA_PUNKT Order By p.priotytet Descending Select p).FirstOrDefault()
        'Dim wz As Wezel = Nothing
        'If punkt IsNot Nothing Then
        '    wz = New Wezel() With {.danem = punkt, .x = (Way.max_x - Way.min_x) / 2 + Way.min_x, .y = (Way.max_y - Way.min_y) / 2 + Way.min_y, .tekst = Way.tekst}
        '    Mapa.wezly.Dodaj(wz)
        'End If

        Dim dm As DaneMalowania()
        Dim priorytety As Integer() = (From p In dane Where (p.flagi And DROGA_PUNKT) = 0 Select p.priotytet Order By priotytet Descending).Distinct.ToArray()
        Dim dodano As Boolean = False
        If priorytety IsNot Nothing AndAlso priorytety.Length > 0 Then
            If priorytety.Length = 1 Then
                dm = dane.ToArray()
            Else
                dm = dane.Where(Function(p As DaneMalowania) p.priotytet = priorytety(0)).ToArray()
            End If

            Dim pr_kat As Integer = dm.Max(Function(d As DaneMalowania) d.PrKategorii)

            For i As Integer = 0 To dm.Length - 1
                If (dm(i).flagi And DROGA_PUNKT) = 0 Then
                    Dim dr As New Droga()
                    dr.min_x = Relation.min_x
                    dr.max_x = Relation.max_x
                    dr.min_y = Relation.min_y
                    dr.max_y = Relation.max_y
                    dr.tekst = Relation.tekst
                    dr.linia1 = Relation.linia1
                    dr.danem = dm(i)

                    If (Not dodano) And dm(i).PrKategorii = pr_kat Then
                        dodano = True
                        dr.Wartosci = Relation.Wartosci
                        dr.NrDomu = Relation.NrDomu
                        dr.Ulica = Relation.Ulica
                        dr.Punkty = Relation.Punkty
                    End If

                    Mapa.drogi.Dodaj(dr)
                End If
            Next

        End If

        'If Not dodano Then
        '    If wz IsNot Nothing Then wz.Wartosci = Way.Wartosci
        'End If
    End Sub

    Friend Class ParametryOtwierania
        Friend SciezkaPliku As String
        Friend Kategorie As Kategoria()
    End Class

End Module