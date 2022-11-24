Friend Module Dane

    Friend Const DROGA_POLE As Integer = 1
    Friend Const DROGA_KRAJOBRAZ As Integer = 2
    Friend Const DROGA_PUNKT As Integer = 4

    Friend Const ZAKRES_X As Single = 0.0002F
    Friend ZAKRES_Y As Single = 0.0002F

    Friend ODLEGLOSC_TEKSTU As Double = 300.0
    Friend POCZATEK_TEKSTU As Double = 20.0

    Friend SprawdzajGestoscWezlow As Boolean = False
    Friend Const POWIEKSZENIE_GRANICY_X As Integer = 100
    Friend Const POWIEKSZENIE_GRANICY_Y As Integer = 100
    Friend Const ROZMIAR_SIATKI_X As Integer = 100
    Friend Const ROZMIAR_SIATKI_Y As Integer = 100

    Friend Class MapaKlasa
        Friend poczatek_x As Single
        Friend poczatek_y As Single
        Friend szerokosc As Single
        Friend wysokosc As Single
        Friend wezly As New Tablica(Of Wezel)(100)
        Friend drogi As New Tablica(Of Droga)(100)
    End Class

    Friend Class Wezel
        Friend x As Single
        Friend y As Single
        Friend tekst As String = ""
        Friend pedzel As PedzleStr
        Friend danem As New DaneMalowania
        Friend Wartosci As KluczWartosc() = Nothing
        'Friend Flagi As Integer = 0
        Friend Ulica As String = ""
        Friend NrDomu As String = ""
        Friend id As String = ""

        Friend Function CzyZawiera(minx As Single, miny As Single, maxx As Single, maxy As Single) As Boolean
            If x < minx Then Return False
            If x > maxx Then Return False
            If y < miny Then Return False
            If y > maxy Then Return False

            Return True
        End Function
    End Class

    Friend Class Droga
        Friend linia1 As IntPtr
        Friend tekst As String = ""
        Friend pedzel As PedzleStr
        Friend danem As New DaneMalowania
        Friend min_x As Single = 1000.0F
        Friend min_y As Single = 1000.0F
        Friend max_x As Single = -1000.0F
        Friend max_y As Single = -1000.0F
        Friend max_poziom As Byte = 255
        Friend Wartosci As KluczWartosc() = Nothing
        'Friend Flagi As Integer = 0
        Friend Ulica As String = ""
        Friend NrDomu As String = ""
        Friend Punkty As Wezel() = Nothing
        Friend RysujTekst As Boolean = True

        Friend Function CzyZawiera(minx As Single, miny As Single, maxx As Single, maxy As Single) As Boolean
            If max_x < minx Then Return False
            If min_x > maxx Then Return False
            If max_y < miny Then Return False
            If min_y > maxy Then Return False

            Return True
        End Function
    End Class


    Friend Class DaneMalowaniaPoziom
        Friend MinPoziom As Integer = 0
        Friend MaxPoziom As Integer = 0
        Friend Pedzle As PedzleStr

        Friend Sub New()
            Pedzle.Grubosc = 1.0F
            Pedzle.Kropkowanie = 0
        End Sub

        Friend Sub New(min_poziom As Integer, max_poziom As Integer, pedzel_tlo As IntPtr)
            MinPoziom = min_poziom
            MaxPoziom = max_poziom
            Pedzle.PedzelTlo = pedzel_tlo
            Pedzle.Grubosc = 1.0F
            Pedzle.Kropkowanie = 0
        End Sub

        Friend Sub New(min_poziom As Integer, max_poziom As Integer, pedzel_tlo As IntPtr, gr As Single)
            MinPoziom = min_poziom
            MaxPoziom = max_poziom
            Pedzle.PedzelTlo = pedzel_tlo
            Pedzle.Grubosc = gr
            Pedzle.Kropkowanie = 0
        End Sub

        Friend Sub New(min_poziom As Integer, max_poziom As Integer, pedzel_tlo As IntPtr, gr As Single, pedzel_ramka As IntPtr, pedzel_tekst As IntPtr)
            MinPoziom = min_poziom
            MaxPoziom = max_poziom
            Pedzle.PedzelTlo = pedzel_tlo
            Pedzle.Grubosc = gr
            Pedzle.PedzelRamka = pedzel_ramka
            Pedzle.PedzelTekst = pedzel_tekst
            Pedzle.Kropkowanie = 0
        End Sub

    End Class

    Friend Class DaneMalowania
        Friend typ As String
        Friend flagi As Integer
        Friend max_poziom As Integer
        Friend priotytet As Integer = 0
        Friend poziomy As DaneMalowaniaPoziom()
        Friend Nazwa As String
        Friend PrKategorii As Integer = 0

        Friend Sub New(ParamArray Poziomy As DaneMalowaniaPoziom())
            Dim max As Integer = 0

            For i As Integer = 0 To Poziomy.Length - 1
                If Poziomy(i).MaxPoziom > max Then max = Poziomy(i).MaxPoziom
            Next

            max_poziom = max
            Me.poziomy = Poziomy
        End Sub

        Friend Function PobierzPedzle(Poziom As Integer) As PedzleStr
            For i As Integer = 0 To poziomy.Length - 1
                If poziomy(i).MinPoziom <= Poziom AndAlso poziomy(i).MaxPoziom >= Poziom Then Return poziomy(i).Pedzle
            Next

            Return New PedzleStr
        End Function

        Public Overrides Function ToString() As String
            Dim s As String
            If Nazwa = "" Then s = typ Else s = Nazwa
            Return s & ", " & priotytet.ToString
        End Function

    End Class

    Friend Structure PedzleStr
        Friend PedzelTlo As IntPtr '= IntPtr.Zero
        Friend PedzelRamka As IntPtr '= IntPtr.Zero
        Friend PedzelTekst As IntPtr '= IntPtr.Zero
        Friend Grubosc As Single '= 1.0F
        Friend Kropkowanie As Integer '= 0
    End Structure

    Friend Structure KluczWartosc
        Friend Klucz As String
        Friend Wartosc As String
        Public Overrides Function ToString() As String
            Return Klucz & "=" & Wartosc
        End Function
        Friend Sub New(k As String, v As String)
            Klucz = k
            Wartosc = v
        End Sub
    End Structure

    'Friend Structure PunktTekstu
    '    Friend Punkt As PointF
    '    Friend Kat As Single
    'End Structure

End Module