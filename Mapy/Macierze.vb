Friend Module Macierze

    Friend Class Macierz3x3

        Friend e11 As Single
        Friend e12 As Single
        Friend e13 As Single
        Friend e21 As Single
        Friend e22 As Single
        Friend e23 As Single
        Friend e31 As Single
        Friend e32 As Single
        Friend e33 As Single

        Friend Sub New()
        End Sub

        Friend Sub New(el11 As Single, el12 As Single, el13 As Single, el21 As Single, el22 As Single, el23 As Single, el31 As Single, el32 As Single, el33 As Single)
            e11 = el11
            e12 = el12
            e13 = el13
            e21 = el21
            e22 = el22
            e23 = el23
            e31 = el31
            e32 = el32
            e33 = el33
        End Sub

        Public Shared Operator *(Macierz1 As Macierz3x3, Macierz2 As Macierz3x3) As Macierz3x3
            Return New Macierz3x3(
                Macierz1.e11 * Macierz2.e11 + Macierz1.e12 * Macierz2.e21 + Macierz1.e13 * Macierz2.e31,
                Macierz1.e11 * Macierz2.e12 + Macierz1.e12 * Macierz2.e22 + Macierz1.e13 * Macierz2.e32,
                Macierz1.e11 * Macierz2.e13 + Macierz1.e12 * Macierz2.e23 + Macierz1.e13 * Macierz2.e33,
 _
                Macierz1.e21 * Macierz2.e11 + Macierz1.e22 * Macierz2.e21 + Macierz1.e23 * Macierz2.e31,
                Macierz1.e21 * Macierz2.e12 + Macierz1.e22 * Macierz2.e22 + Macierz1.e23 * Macierz2.e32,
                Macierz1.e21 * Macierz2.e13 + Macierz1.e22 * Macierz2.e23 + Macierz1.e23 * Macierz2.e33,
 _
                Macierz1.e31 * Macierz2.e11 + Macierz1.e32 * Macierz2.e21 + Macierz1.e33 * Macierz2.e31,
                Macierz1.e31 * Macierz2.e12 + Macierz1.e32 * Macierz2.e22 + Macierz1.e33 * Macierz2.e32,
                Macierz1.e31 * Macierz2.e13 + Macierz1.e32 * Macierz2.e23 + Macierz1.e33 * Macierz2.e33
)
        End Operator

        Public Shared Operator *(Wezel_ As Wezel, Macierz As Macierz3x3) As Point
            Dim p As New Point
            p.X = Convert.ToInt32(Wezel_.x * Macierz.e11 + Wezel_.y * Macierz.e21 + Macierz.e31)
            p.Y = Convert.ToInt32(Wezel_.x * Macierz.e12 + Wezel_.y * Macierz.e22 + Macierz.e32)
            Return p
        End Operator

        Public Shared Operator *(Wezel_ As PointF, Macierz As Macierz3x3) As Point
            Dim p As New Point
            p.X = Convert.ToInt32(Wezel_.X * Macierz.e11 + Wezel_.Y * Macierz.e21 + Macierz.e31)
            p.Y = Convert.ToInt32(Wezel_.X * Macierz.e12 + Wezel_.Y * Macierz.e22 + Macierz.e32)
            Return p
        End Operator
    End Class

    Friend Function UtworzMacierzPrzesuniecia(Dx As Single, Dy As Single) As Macierz3x3
        Return New Macierz3x3(1, 0, 0,
                              0, 1, 0,
                              Dx, Dy, 1)
    End Function

    Friend Function UtworzMacierzSkalowania(Sx As Single, Sy As Single) As Macierz3x3
        Return New Macierz3x3(Sx, 0, 0,
                              0, Sy, 0,
                              0, 0, 1)
    End Function

End Module