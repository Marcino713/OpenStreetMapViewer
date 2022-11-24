Friend Module Grafika

    Friend Enum ZakonczenieFigury As Integer
        Otwarta = 0
        Zamknieta = 1
    End Enum

    Friend Declare Auto Sub InicjalizujGrafike Lib "mapgr.dll" (Okno As Integer, Szerokosc As Integer, Wysokosc As Integer)
    Friend Declare Auto Sub Posprzataj Lib "mapgr.dll" ()
    Friend Declare Auto Function UtworzPedzelKolor Lib "mapgr.dll" (r As Single, g As Single, b As Single, Optional a As Single = 1.0F) As IntPtr
    Friend Declare Auto Function UtworzPedzelZasob Lib "mapgr.dll" (Zasob As Byte(), Dlugosc As UInteger) As IntPtr
    Friend Declare Auto Sub UstawTransformacje Lib "mapgr.dll" (dx As Single, dy As Single, s As Single)
    Friend Declare Auto Sub RysujLinie Lib "mapgr.dll" (Pedzel As IntPtr, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, Optional grubosc As Single = 1.0, Optional styl As Integer = 0)
    Friend Declare Auto Sub RozpocznijRysowanie Lib "mapgr.dll" ()
    Friend Declare Auto Sub ZakonczRysowanie Lib "mapgr.dll" ()
    Friend Declare Auto Sub ZmienRozmiar Lib "mapgr.dll" (Szerokosc As UInteger, Wysokosc As UInteger)
    Friend Declare Auto Sub RysujObraz Lib "mapgr.dll" (pedzel As IntPtr, x As Integer, y As Integer)

    Friend Declare Auto Function UtworzGeometrieSciezka Lib "mapgr.dll" () As IntPtr
    Friend Declare Auto Sub UtworzFigure Lib "mapgr.dll" (x As Single, y As Single)
    Friend Declare Auto Sub DodajLinie Lib "mapgr.dll" (x As Single, y As Single)
    Friend Declare Auto Sub ZakonczFigure Lib "mapgr.dll" (zakonczenie As ZakonczenieFigury)
    Friend Declare Auto Sub ZakonczGeometrie Lib "mapgr.dll" ()
    Friend Declare Auto Sub WypelnijSciezke Lib "mapgr.dll" (geometria As IntPtr, pedzel As IntPtr)
    Friend Declare Auto Function UtworzKreskowanie Lib "mapgr.dll" (styl As Integer) As Integer
    Friend Declare Auto Sub RysujSciezke Lib "mapgr.dll" (geometria As IntPtr, pedzel As IntPtr, Optional grubosc As Single = 1.0F, Optional styl As Integer = 0)

    Friend Declare Auto Sub PrzesunPedzle Lib "mapgr.dll" (dx As Integer, dy As Integer)

    Friend Declare Auto Sub RysujTekst Lib "mapgr.dll" (tekst As String, pedzel As IntPtr, x As Single, y As Single, Optional kat As Single = 0.0F, Optional szerokosc As Single = 76.0F, Optional wysrodkuj As Boolean = False)
End Module