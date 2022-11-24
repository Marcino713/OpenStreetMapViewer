Friend Module App

    Friend Mapa As New MapaKlasa
    Friend Okno As wndOkno
    Friend Kropkowania(2) As Integer
    'Friend PedzelBudynek As DaneMalowania
    'Friend PedzelCzarny As IntPtr
    Friend PedzelZaznaczenie As IntPtr
    Friend PedzelZaznaczeniePunkt As IntPtr
    Friend ZaznaczonyElement As Object = Nothing

    Private OknoWyszukiwania As wndZnalezioneElementy = Nothing
    Private OknoWyszAdresu As wndSzukajAdresu = Nothing

    Friend Sub PokazOknoWyszukiwania(Wezly As List(Of Wezel), Drogi As List(Of Droga))
        If OknoWyszukiwania Is Nothing Then
            OknoWyszukiwania = New wndZnalezioneElementy()
            OknoWyszukiwania.Show()
        End If

        OknoWyszukiwania.PokazElementy(Wezly, Drogi)
        OknoWyszukiwania.Focus()
    End Sub

    Friend Sub UkryjOknoWyszukiwania()
        OknoWyszukiwania = Nothing
    End Sub

    Friend Sub PokazOknoWyszAdresu()
        If OknoWyszAdresu Is Nothing Then
            OknoWyszAdresu = New wndSzukajAdresu()
            OknoWyszAdresu.Show()
        Else
            OknoWyszAdresu.Focus()
        End If
    End Sub

    Friend Sub UkryjOknoWyszAdresu()
        OknoWyszAdresu = Nothing
    End Sub

    Friend Sub UstawZaznaczonyElement(Element As Object)
        ZaznaczonyElement = Element
        Okno.Rysuj()
    End Sub

    Friend Sub PrzeliczWspolrzedne(ByRef lat As Single, ByRef lon As Single)
        lat *= 1.852F
    End Sub

    Friend Sub InicjalizujProgram()

        Kropkowania(0) = UtworzKreskowanie(1)
        Kropkowania(1) = UtworzKreskowanie(2)
        Kropkowania(2) = UtworzKreskowanie(3)
        'PedzelBudynek = New DaneMalowania(New DaneMalowaniaPoziom(1, 4, WczytywaniePedzli.UtworzPedzelZKoloru("#BBB")))
        'PedzelBudynek.flagi = DROGA_POLE
        'PedzelCzarny = UtworzPedzelKolor(0, 0, 0)
        PedzelZaznaczenie = WczytywaniePedzli.UtworzPedzelZKoloru("#FF6200")
        PedzelZaznaczeniePunkt = WczytywaniePedzli.PobierzPedzelZZasobu("zaznaczenie.png")
    End Sub

    Friend Sub UstawPedzle(Poziom As Integer)
        For i As Integer = 0 To Mapa.drogi.Length - 1
            If Mapa.drogi(i).danem Is Nothing Then Continue For
            Mapa.drogi(i).pedzel = Mapa.drogi(i).danem.PobierzPedzle(Poziom)
        Next

        For i As Integer = 0 To Mapa.wezly.Length - 1
            Mapa.wezly(i).pedzel = Mapa.wezly(i).danem.PobierzPedzle(Poziom)
        Next

        'For i As Integer = 0 To ILOSC_PEDZLI - 1
        '    Pedzle(i) = PedzleWszystkie(i).PobierzPedzle(Poziom)
        'Next

    End Sub

End Module