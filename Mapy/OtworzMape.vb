Imports System.Xml

Partial Friend Module App

    '-------------------------------------------------------------
    'Dlugosc geograficzna     lon    x
    'Szerokosc geograficzna   lat    y
    '-------------------------------------------------------------

    Friend Sub OtworzMape(SciezkaPliku As Object)
        Dim Sciezka As String = CType(SciezkaPliku, String)
        Okno.Invoke(Okno.UstawKontrolkiWczytywanie)
        Dim plik As New XmlDocument()
        plik.Load(Sciezka)

        Dim el As XmlNodeList = plik.FirstChild.NextSibling.ChildNodes
        Dim elem As XmlNode = el(0)
        'Dim elem2 As XmlNode
        Dim l As Integer = el.Count - 1
        'Dim l2 As Integer
        Dim n As Wezel
        Dim d As Droga
        Dim atr As XmlAttributeCollection
        Dim wezly As New Hashtable
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

                If elem.HasChildNodes Then
                    If CzytajWezel(elem, n) Then Mapa.wezly.Dodaj(n)
                End If

                wezly.Add(id, n)

            End If

            'Application.DoEvents()

            elem = elem.NextSibling
        Next

        'Okno.UstawKontrolkiPostep(50)

        'Drogi
        elem = el(0)

        For i As Integer = 0 To l

            If elem.Name = "way" Then
                d = New Droga
                CzytajDroge(elem, d, wezly)
                Mapa.drogi.Dodaj(d)
            End If

            elem = elem.NextSibling

        Next

        Okno.Invoke(Okno.UstawKontrolkiKoniec)

    End Sub

    Friend Function CzytajWezel(ByRef Xml As XmlNode, ByRef Node As Wezel) As Boolean
        Dim l As Integer = Xml.ChildNodes.Count - 1
        Dim elem As XmlNode = Xml.FirstChild
        Dim k As String = ""
        Dim v As String = ""

        For i As Integer = 0 To l

            If elem.Name = "tag" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "k" : k = elem.Attributes(j).Value
                        Case "v" : v = elem.Attributes(j).Value
                    End Select
                Next


                Select Case k
                    Case "aeroway"

                        Select Case v
                            Case "aerodrome"
                                Node.pedzel = 53
                                Return True

                            Case "helipad"
                                Node.pedzel = 54
                                Return True
                        End Select

                    Case "amenity"

                        Select Case v
                            Case "bar"
                                Node.pedzel = 55
                                Return True

                            Case "bbq"
                                Node.pedzel = 56
                                Return True

                            Case "biergarten"
                                Node.pedzel = 57
                                Return True

                            Case "cafe"
                                Node.pedzel = 58
                                Return True

                            Case "drinking_water"
                                Node.pedzel = 59
                                Return True

                            Case "fast_food"
                                Node.pedzel = 50
                                Return True

                            Case "food_court"
                                Node.pedzel = 62
                                Return True

                            Case "ice_cream"
                                Node.pedzel = 60
                                Return True

                            Case "pub"
                                Node.pedzel = 61
                                Return True

                            Case "restaurant"
                                Node.pedzel = 62
                                Return True

                        End Select

                    Case "barrier"

                        Select Case v
                            Case "gate"
                                Node.pedzel = 63
                                Return True

                            Case "lift_gate"
                                Node.pedzel = 64
                                Return True

                        End Select

                    Case "highway"

                        Select Case v
                            Case "bus_stop"
                                Node.pedzel = 65
                                Return True

                            Case "elevator"
                                Node.pedzel = 66
                                Return True

                            Case "traffic_signals"
                                Node.pedzel = 67
                                Return True

                        End Select

                    Case "leisure"

                        Select Case v
                            Case "golf_course"
                                Node.pedzel = 68
                                Return True

                            Case "miniature_golf"
                                Node.pedzel = 69
                                Return True

                            Case "playground"
                                Node.pedzel = 70
                                Return True

                            Case "water_park"
                                Node.pedzel = 71
                                Return True

                        End Select

                    Case "man_made"

                        Select Case v
                            Case "lighthouse"
                                Node.pedzel = 72
                                Return True

                            Case "mast"
                                Node.pedzel = 73
                                Return True

                            Case "water_tower"
                                Node.pedzel = 74
                                Return True

                            Case "windmill"
                                Node.pedzel = 75
                                Return True

                        End Select

                    Case "power"

                        Select Case v
                            Case "tower", "pole"
                                Node.pedzel = 52
                                Return True
                        End Select

                    Case "railway"

                        Select Case v
                            Case "level_crossing"
                                Node.pedzel = 76
                                Return True

                            Case "signal"
                                Node.pedzel = 67
                                Return True

                            Case "subway_entrance"
                                Node.pedzel = 77
                                Return True

                        End Select

                End Select

            End If

            elem = elem.NextSibling

        Next

        Return False
    End Function

    Friend Sub CzytajDroge(ByRef Xml As XmlNode, ByRef Way As Droga, ByRef wezly As Hashtable)
        Dim l As Integer = Xml.ChildNodes.Count - 1
        Dim elem As XmlNode = Xml.FirstChild
        Dim n As Wezel
        Dim pierwszy As Boolean = True
        Dim pierwszy_wezel As String = ""
        Dim ostatni_wezel As String = ""
        Dim k As String = ""
        Dim v As String = ""
        Dim sprawdz_rozmiar As Boolean = False

        'Dane
        For i As Integer = 0 To l

            If elem.Name = "tag" Then

                For j As Integer = 0 To elem.Attributes.Count - 1
                    Select Case elem.Attributes(j).Name
                        Case "k" : k = elem.Attributes(j).Value
                        Case "v" : v = elem.Attributes(j).Value
                    End Select
                Next


                'Przyporzadkuj pedzle
                Select Case k
                    Case "name"
                        Way.tekst = v

                    Case "area"

                        Select Case v
                            Case "yes"
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        End Select

                    Case "building"

                        Way.pedzel = 51
                        Way.atrybuty = Way.atrybuty Or DROGA_POLE

                    Case "railway"

                        Select Case v
                            Case "rail"
                                Way.pedzel = 0

                            Case "tram"
                                Way.pedzel = 78

                        End Select

                    Case "highway"

                        Select Case v
                            Case "motorway"
                                Way.pedzel = 6

                            Case "trunk"
                                Way.pedzel = 7

                            Case "primary"
                                Way.pedzel = 8

                            Case "secondary"
                                Way.pedzel = 79

                            Case "tertiary"
                                Way.pedzel = 5

                            Case "unclassified"
                                Way.pedzel = 5

                            Case "residential"
                                Way.pedzel = 5

                            Case "service"
                                Way.pedzel = 5

                            Case "secondary_link"
                                Way.pedzel = 79

                            Case "tertiary_link"
                                Way.pedzel = 5

                            Case "living_street"
                                Way.pedzel = 5

                            Case "road"
                                Way.pedzel = 5

                            Case "raceway"
                                Way.pedzel = 11

                        End Select

                    Case "barrier"

                        Select Case v
                            Case "wall"
                                Way.pedzel = 12

                            Case "yes"
                                Way.pedzel = 13
                        End Select

                    Case "power"

                        Select Case v
                            Case "line", "minor_line"
                                Way.pedzel = 17
                        End Select

                    Case "waterway"

                        Select Case v
                            Case "river", "stream", "canal", "drain", "ditch"
                                Way.pedzel = 9

                            Case "riverbank"
                                Way.pedzel = 9
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        End Select

                    Case "natural"

                        Way.atrybuty = Way.atrybuty Or DROGA_KRAJOBRAZ

                        Select Case v
                            Case "water"
                                Way.pedzel = 9
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        End Select

                    Case "landuse"

                        Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        Way.atrybuty = Way.atrybuty Or DROGA_KRAJOBRAZ

                        Select Case v
                            Case "allotments"
                                Way.pedzel = 27

                            Case "basin"
                                Way.pedzel = 9

                            Case "brownfield"
                                Way.pedzel = 28

                            Case "cemetery"
                                Way.pedzel = 49

                            Case "commercial"
                                Way.pedzel = 29

                            Case "construction"
                                Way.pedzel = 30

                            Case "farmland"
                                Way.pedzel = 31

                            Case "farmyard"
                                Way.pedzel = 32

                            Case "forest"
                                Way.pedzel = 10

                            Case "garages"
                                Way.pedzel = 33

                            Case "grass"
                                Way.pedzel = 34

                            Case "greenfield"
                                Way.pedzel = 35

                            Case "greenhouse_horticulture"
                                Way.pedzel = 36

                            Case "industrial"
                                Way.pedzel = 37

                            Case "landfill"
                                Way.pedzel = 38

                            Case "meadow"
                                Way.pedzel = 39

                            Case "military"
                                Way.pedzel = 40

                            Case "orchard"
                                Way.pedzel = 41

                            Case "quarry"
                                Way.pedzel = 42

                            Case "railway"
                                Way.pedzel = 15

                            Case "recreation_ground"
                                Way.pedzel = 43

                            Case "reservoir"
                                Way.pedzel = 44

                            Case "residential"
                                Way.pedzel = 45

                            Case "retail"
                                Way.pedzel = 46

                            Case "village_green"
                                Way.pedzel = 47

                            Case "vineyard"
                                Way.pedzel = 48
                        End Select

                    Case "aeroway"

                        Select Case v
                            Case "apron"
                                Way.pedzel = 16
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        End Select

                    Case "man_made"

                        Select Case v
                            Case "cutline"
                                Way.pedzel = 18
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                            Case "pier"
                                Way.pedzel = 19
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE
                        End Select

                    Case "leisure"

                        Select Case v
                            Case "garden"
                                Way.pedzel = 20
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                            Case "park"
                                Way.pedzel = 21
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                            Case "pitch"
                                Way.pedzel = 22
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                            Case "sports_centre", "stadium"
                                Way.pedzel = 23
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                            Case "track"
                                Way.pedzel = 24
                                Way.atrybuty = Way.atrybuty Or DROGA_POLE

                        End Select

                    Case "amenity"

                        Way.atrybuty = Way.atrybuty Or DROGA_POLE

                        Select Case v
                            Case "college", "kindergarten", "school", "university"
                                Way.pedzel = 25

                            Case "grave_yard"
                                Way.pedzel = 49

                        End Select

                    Case "tourism"

                        Way.atrybuty = Way.atrybuty Or DROGA_POLE

                        Select Case v
                            Case "attraction"
                                Way.pedzel = 26
                        End Select

                End Select


            End If

            elem = elem.NextSibling
        Next

        'Wezly
        elem = Xml.FirstChild

        For i As Integer = 0 To l

            If elem.Name = "nd" Then
                ostatni_wezel = elem.Attributes(0).Value
                n = CType(wezly(ostatni_wezel), Wezel)

                If n.x < Way.min_x Then Way.min_x = n.x
                If n.x > Way.max_x Then Way.max_x = n.x
                If n.y < Way.min_y Then Way.min_y = n.y
                If n.y > Way.max_y Then Way.max_y = n.y

                If pierwszy Then
                    Way.linia1 = UtworzGeometrieSciezka(n.x, n.y)
                    pierwszy_wezel = ostatni_wezel
                    pierwszy = False
                Else
                    DodajLinie(n.x, n.y)
                End If

            End If

            elem = elem.NextSibling
        Next

        If (Way.atrybuty And DROGA_POLE) = DROGA_POLE OrElse (Way.atrybuty And DROGA_KRAJOBRAZ) = DROGA_KRAJOBRAZ OrElse pierwszy_wezel = ostatni_wezel Then
            ZakonczGeometrie(ZakonczenieFigury.Zamknieta)
        Else
            ZakonczGeometrie(ZakonczenieFigury.Otwarta)
        End If
    End Sub

End Module