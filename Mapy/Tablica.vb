Friend Class Tablica(Of T)
    Private tab As T()
    Private dl As Integer = 0
    'Private przydzielono As Integer = 0
    Private zmien_rozmiar As Integer = 20

    Public Sub New(ZmianaRozmiaru As Integer)
        zmien_rozmiar = ZmianaRozmiaru
        'przydzielono = ZmianaRozmiaru
        ReDim tab(zmien_rozmiar - 1)
    End Sub

    Public Sub New()
        ReDim tab(zmien_rozmiar - 1)
    End Sub

    Friend ReadOnly Property Length As Integer
        Get
            Return dl
        End Get
    End Property

    Default Friend ReadOnly Property Item(ix As Integer) As T
        Get
            Return tab(ix)
        End Get
    End Property

    Friend Sub Dodaj(el As T)
        If dl = tab.Length Then
            ReDim Preserve tab(tab.Length - 1 + zmien_rozmiar)
        End If

        tab(dl) = el
        dl += 1
    End Sub

End Class