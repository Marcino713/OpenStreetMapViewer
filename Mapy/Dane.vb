Friend Module Dane

    Friend Const DROGA_POLE As Integer = 1
    Friend Const DROGA_KRAJOBRAZ As Integer = 2

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
        Friend szer_tekstu As Single = 0F
        Friend wys_tekstu As Single = 0F
        Friend tekst As String = ""
        Friend pedzel As Byte = BRAK_PEDZLA
        'Friend max_poziom As Byte

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
        Friend szer_tekstu As Single = 0F
        Friend wys_tekstu As Single = 0F
        Friend tekst As String = ""
        Friend pedzel As Byte = BRAK_PEDZLA
        Friend min_x As Single = 1000.0F
        Friend min_y As Single = 1000.0F
        Friend max_x As Single = -1000.0F
        Friend max_y As Single = -1000.0F
        Friend atrybuty As Integer = 0
        Friend max_poziom As Byte = 255

        Friend Function CzyZawiera(minx As Single, miny As Single, maxx As Single, maxy As Single) As Boolean
            If max_x < minx Then Return False
            If min_x > maxx Then Return False
            If max_y < miny Then Return False
            If min_y > maxy Then Return False

            Return True
        End Function
    End Class


    Friend Class DaneMalowaniaPoziom
        Friend min_poziom As Integer
        Friend max_poziom As Integer
        'Friend pedzel1 As IntPtr
		'Friend pedzel2 As IntPtr
		'Friend pedzel3 As IntPtr
		Friend pedzle As PedzleStr

        'Friend Sub New(min_poziom As Integer, max_poziom As Integer, id_zasobu_pedzla As Integer)
        '    min = min_poziom
        '    max = max_poziom
        '    pedzel = UtworzPedzelObraz(id_zasobu_pedzla)
        'End Sub

        'Friend Sub New(min_poziom As Integer, max_poziom As Integer, r As Single, g As Single, b As Single)
        '    min = min_poziom
        '    max = max_poziom
        '    pedzel = UtworzPedzelKolor(r, g, b)
        'End Sub

        Friend Sub New(min_poziom As Integer, max_poziom As Integer, pedzel1 As IntPtr, Optional gr1 As Single = 1.0F, Optional pedzel2 As Integer = 0, Optional pedzel3 As Integer = 0)
            Me.min_poziom = min_poziom
            Me.max_poziom = max_poziom
            Me.pedzle.Pedzel1 = pedzel1
            Me.pedzle.Grubosc1 = gr1
            Me.pedzle.Pedzel2 = IntPtr.op_Explicit(pedzel2)
            Me.pedzle.Pedzel3 = IntPtr.op_Explicit(pedzel3)
        End Sub

    End Class

    Friend Class DaneMalowania
        Friend max_poziom As Integer
        Friend poziomy As DaneMalowaniaPoziom()

        Friend Sub New(ParamArray Poziomy As DaneMalowaniaPoziom())
            Dim max As Integer = 0

            For i As Integer = 0 To Poziomy.Length - 1
                If Poziomy(i).max_poziom > max Then max = Poziomy(i).max_poziom
            Next

            max_poziom = max
            Me.poziomy = Poziomy
        End Sub

        Friend Function PobierzPedzle(Poziom As Integer) As PedzleStr
            For i As Integer = 0 To poziomy.Length - 1
                If poziomy(i).min_poziom <= Poziom AndAlso poziomy(i).max_poziom >= Poziom Then Return poziomy(i).pedzle
            Next

            Return New PedzleStr With {.Pedzel1 = IntPtr.Zero}
        End Function

    End Class
	
	Friend Structure PedzleStr
        Friend Pedzel1 As IntPtr
        Friend Grubosc1 As Single
        Friend Pedzel2 As IntPtr
		Friend Pedzel3 As IntPtr
	End Structure

End Module