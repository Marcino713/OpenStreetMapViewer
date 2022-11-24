Imports System.Xml
Imports System.IO
Imports System.Reflection

Friend Class WczytywaniePedzli
    Private Shared Pedzle As New Dictionary(Of String, IntPtr)

    Friend Function WczytajPlik() As Kategoria()
        Dim str As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mapy.Pedzle.xml")
        Dim plik As New XmlDocument()
        plik.Load(str)
        str.Close()
        Dim Kategorie As New List(Of Kategoria)

        Dim dz As XmlNodeList = plik.FirstChild.NextSibling.ChildNodes
        Dim el As XmlNode = dz.Item(0)

        'Pedzle
        For i As Integer = 0 To dz.Count - 1
            If el.Name = "pedzle" Then

                Dim dzp As XmlNodeList = el.ChildNodes
                Dim elp As XmlNode = el.FirstChild

                For j As Integer = 0 To dzp.Count - 1
                    If elp.Name = "pedzel" Then
                        Dim nazwa As String = elp.Attributes.GetNamedItem("nazwa").Value
                        Dim kolor As String = elp.Attributes.GetNamedItem("kolor").Value

                        If Not Pedzle.ContainsKey(nazwa) Then
                            Dim pedzel As IntPtr = UtworzPedzelZKoloru(kolor)
                            If pedzel <> IntPtr.Zero Then Pedzle.Add(nazwa, pedzel)
                        End If
                    End If
                    elp = elp.NextSibling
                Next

                Exit For
            End If

            el = el.NextSibling
        Next


        'Kategorie
        dz = plik.FirstChild.NextSibling.ChildNodes
        el = dz.Item(0)

        For i As Integer = 0 To dz.Count - 1
            If el.Name = "kategorie" Then

                Dim dzk As XmlNodeList = el.ChildNodes
                Dim elk As XmlNode = el.FirstChild

                For j As Integer = 0 To dzk.Count - 1
                    If elk.Name = "kat" Then

                        Kategorie.Add(PrzetworzJednaKategorie(elk))

                    End If

                    elk = elk.NextSibling
                Next


            End If

            el = el.NextSibling
        Next

        Return Kategorie.ToArray()
    End Function

    'Friend Sub New()
    'End Sub

    Private Function PrzetworzJednaKategorie(element As XmlNode) As Kategoria
        Dim kat As New Kategoria
        'kat.Nazwa = element.Attributes.GetNamedItem("nazwa").Value
        'Dim f As XmlNode = element.Attributes.GetNamedItem("flagi")
        Dim flagi As Integer = -1
        'If f IsNot Nothing Then flagi = PobierzFlagi(f.Value)
        'Dim pl As String = ""
        Dim pr_kat As Integer = 0

        For i As Integer = 0 To element.Attributes.Count - 1
            Dim w As String = element.Attributes(i).Value
            Select Case element.Attributes(i).Name
                Case "nazwa" : kat.Nazwa = w
                Case "flagi" : flagi = PobierzFlagi(w)
                Case "prkat" : Integer.TryParse(w, pr_kat)
                    'Case "pl" : pl = w
            End Select
        Next

        'Dim nazwa_kat As Boolean = True
        'If pl <> "" Then
        '    nazwa_kat = False
        'Else
        '    pl = kat.Nazwa
        'End If
        Dim elementy As New List(Of DaneMalowania)

        Dim dz As XmlNodeList = element.ChildNodes
        Dim el As XmlNode = element.FirstChild

        For i As Integer = 0 To dz.Count - 1

            If el.Name = "el" Then
                elementy.Add(PrzetworzTypKategorii(el, flagi, kat.Nazwa, pr_kat))
            End If

            el = el.NextSibling
        Next

        kat.Typy = elementy.ToArray()
        Return kat
    End Function

    Friend Function PrzetworzTypKategorii(element As XmlNode, flagi_kat As Integer, nazwa As String, priorytet_kategorii As Integer) As DaneMalowania
        Dim dm As New DaneMalowania()
        'dm.typ = element.Attributes.GetNamedItem("typ").Value

        'Dim atr As XmlNode = element.Attributes.GetNamedItem("flagi")
        'If atr IsNot Nothing Then
        '    dm.flagi = PobierzFlagi(atr.Value)
        'Else
        '    If flagi_kat <> -1 Then dm.flagi = flagi_kat
        'End If
        If flagi_kat <> -1 Then dm.flagi = flagi_kat
        dm.PrKategorii = priorytet_kategorii

        'Dim priorytet As XmlNode = element.Attributes.GetNamedItem("priorytet")
        'If priorytet IsNot Nothing Then
        '    Integer.TryParse(priorytet.Value, dm.priotytet)
        'End If

        'Dim pl As XmlNode = element.Attributes.GetNamedItem("pl")
        'If pl Is Nothing Then
        '    dm.Nazwa = nazwa & "=" & dm.typ
        'Else
        '    dm.Nazwa = pl.Value
        'End If

        For i As Integer = 0 To element.Attributes.Count - 1
            Dim w As String = element.Attributes(i).Value
            Select Case element.Attributes(i).Name
                Case "typ" : dm.typ = w
                Case "flagi" : dm.flagi = PobierzFlagi(w)
                Case "priorytet" : Integer.TryParse(w, dm.priotytet)
                Case "pl" : dm.Nazwa = w
                Case "prkat" : Integer.TryParse(w, dm.PrKategorii)
            End Select
        Next

        If dm.Nazwa = "" Then dm.Nazwa = nazwa & "=" & dm.typ

        Dim poziomy As New List(Of DaneMalowaniaPoziom)
        Dim dz As XmlNode = element.FirstChild
        For j As Integer = 0 To element.ChildNodes.Count - 1

            Dim dmp As New DaneMalowaniaPoziom()

            For k As Integer = 0 To dz.Attributes.Count - 1
                Dim w As String = dz.Attributes(k).Value
                Select Case dz.Attributes(k).Name
                    Case "min" : dmp.MinPoziom = Integer.Parse(w)
                    Case "max" : dmp.MaxPoziom = Integer.Parse(w)
                    Case "grubosc" : dmp.Pedzle.Grubosc = Single.Parse(w.Replace("."c, ","c))
                    Case "tlo" : dmp.Pedzle.PedzelTlo = PobierzPedzelZZasobu(w)
                    Case "ramka" : dmp.Pedzle.PedzelRamka = PobierzPedzelZZasobu(w)
                    Case "tekst" : dmp.Pedzle.PedzelTekst = PobierzPedzelZZasobu(w)
                    Case "kreski" : dmp.Pedzle.Kropkowanie = Integer.Parse(w)
                End Select
            Next

            poziomy.Add(dmp)
            dz = dz.NextSibling
        Next


        dm.poziomy = poziomy.ToArray()
        Return dm
    End Function

    Private Function PobierzFlagi(flagi As String) As Integer
        Dim f As Integer = 0
        If flagi.Contains("pole") Then f = f Or DROGA_POLE
        If flagi.Contains("krajobraz") Then f = f Or DROGA_KRAJOBRAZ
        If flagi.Contains("punkt") Then f = f Or DROGA_PUNKT
        Return f
    End Function

    Private Shared Function PobierzZasob(NazwaPliku As String) As Byte()
        Dim str As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mapy." & NazwaPliku)
        If str Is Nothing Then
            MessageBox.Show("Nie znaleziono zasobu: " & NazwaPliku, "Błąd")
            Return Nothing
        End If
        Dim b(CInt(str.Length - 1)) As Byte
        str.Seek(0, SeekOrigin.Begin)
        str.Read(b, 0, b.Length)
        str.Close()
        Return b
    End Function

    Friend Shared Function PobierzPedzelZZasobu(Nazwa As String) As IntPtr
        If Pedzle.ContainsKey(Nazwa) Then Return Pedzle(Nazwa)
        Dim b As Byte() = PobierzZasob(Nazwa)
        If b Is Nothing Then Return IntPtr.Zero
        Dim p As IntPtr = UtworzPedzelZasob(b, CUInt(b.Length))
        Pedzle.Add(Nazwa, p)
        Return p
    End Function

    Friend Shared Function UtworzPedzelZKoloru(KolorHex As String) As IntPtr
        Dim r As Single
        Dim g As Single
        Dim b As Single
        Dim a As Single = 1.0F

        If Not (KolorHex.Length = 7 Or KolorHex.Length = 9 Or KolorHex.Length = 4 Or KolorHex.Length = 5) Then
            Return IntPtr.Zero
        End If

        Try
            If KolorHex.Length = 7 Or KolorHex.Length = 9 Then     'Liczby dwucyfrowe
                r = CSng(Integer.Parse(KolorHex(1) & KolorHex(2), Globalization.NumberStyles.HexNumber) / 255)
                g = CSng(Integer.Parse(KolorHex(3) & KolorHex(4), Globalization.NumberStyles.HexNumber) / 255)
                b = CSng(Integer.Parse(KolorHex(5) & KolorHex(6), Globalization.NumberStyles.HexNumber) / 255)
                If KolorHex.Length = 9 Then     'Z przezroczystością
                    a = r
                    r = g
                    g = b
                    b = CSng(Integer.Parse(KolorHex(7) & KolorHex(8), Globalization.NumberStyles.HexNumber) / 255)
                End If

            Else    'Liczby jednocyfrowe
                r = CSng(Integer.Parse(KolorHex(1), Globalization.NumberStyles.HexNumber) / 15)
                g = CSng(Integer.Parse(KolorHex(2), Globalization.NumberStyles.HexNumber) / 15)
                b = CSng(Integer.Parse(KolorHex(3), Globalization.NumberStyles.HexNumber) / 15)
                If KolorHex.Length = 5 Then
                    a = r
                    r = g
                    g = b
                    b = CSng(Integer.Parse(KolorHex(3), Globalization.NumberStyles.HexNumber) / 15)
                End If
            End If
        Catch
            Return IntPtr.Zero
        End Try

        Return UtworzPedzelKolor(r, g, b, a)

    End Function

End Class

Friend Class Kategoria
    Friend Nazwa As String
    Friend Typy As DaneMalowania()
End Class