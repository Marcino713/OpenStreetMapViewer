Friend Module Grafika

    Friend Enum ZakonczenieFigury As Integer
        Otwarta = 0
        Zamknieta = 1
    End Enum

    Friend Enum TypGrafiki
        bmp = 1
        png = 2
    End Enum

    Friend Declare Auto Sub InicjalizujGrafike Lib "mapgr.dll" (Okno As Integer, Szerokosc As Integer, Wysokosc As Integer)
    Friend Declare Auto Sub Posprzataj Lib "mapgr.dll" ()
    Friend Declare Auto Function UtworzPedzelKolor Lib "mapgr.dll" (r As Single, g As Single, b As Single) As IntPtr
    Friend Declare Auto Function UtworzPedzelObraz Lib "mapgr.dll" (Zasob As Integer, Typ As TypGrafiki) As IntPtr
    Friend Declare Auto Sub UstawTransformacje Lib "mapgr.dll" (dx As Single, dy As Single, s As Single)
    Friend Declare Auto Sub RysujLinie Lib "mapgr.dll" (Pedzel As IntPtr, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, Optional grubosc As Single = 1.0)
    Friend Declare Auto Sub RozpocznijRysowanie Lib "mapgr.dll" ()
    Friend Declare Auto Sub ZakonczRysowanie Lib "mapgr.dll" ()
    Friend Declare Auto Sub ZmienRozmiar Lib "mapgr.dll" (Szerokosc As UInteger, Wysokosc As UInteger)
    Friend Declare Auto Sub RysujObraz Lib "mapgr.dll" (pedzel As IntPtr, x As Integer, y As Integer)

    Friend Declare Auto Function UtworzGeometrieSciezka Lib "mapgr.dll" (x As Single, y As Single) As IntPtr
    'Friend Declare Auto Function RozpocznijFigure Lib "mapgr.dll" (sciezka As IntPtr, x As Integer, y As Integer) As IntPtr
    Friend Declare Auto Sub DodajLinie Lib "mapgr.dll" (x As Single, y As Single)
    Friend Declare Auto Sub ZakonczGeometrie Lib "mapgr.dll" (zakonczenie As ZakonczenieFigury)
    Friend Declare Auto Sub WypelnijSciezke Lib "mapgr.dll" (geometria As IntPtr, pedzel As IntPtr)
    Friend Declare Auto Sub RysujSciezke Lib "mapgr.dll" (geometria As IntPtr, pedzel As IntPtr, Optional grubosc As Single = 1.0F)
    'Friend Declare Auto Sub UsunSciezke Lib "mapgr.dll" (geometria As IntPtr)

    Friend Declare Auto Sub PrzesunPedzle Lib "mapgr.dll" (dx As Integer, dy As Integer)
    Friend Declare Auto Function PobierzRozmiarPedzla Lib "mapgr.dll" (pedzel As IntPtr) As Integer
End Module