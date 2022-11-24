Friend Class wndSzukajAdresu
    Private Sub btnSzukaj_Click() Handles btnSzukaj.Click
        Dim nr As String = txtNrDomu.Text.ToLower
        Dim ul As String = txtUlica.Text.ToLower

        Dim wz As New List(Of Wezel)
        Dim dr As New List(Of Droga)

        For i As Integer = 0 To Mapa.wezly.Length - 1
            If Mapa.wezly(i).Wartosci IsNot Nothing AndAlso Mapa.wezly(i).NrDomu.ToLower = nr AndAlso Mapa.wezly(i).Ulica.ToLower.Contains(ul) Then wz.Add(Mapa.wezly(i))
        Next

        For i As Integer = 0 To Mapa.drogi.Length - 1
            If Mapa.drogi(i).Wartosci IsNot Nothing AndAlso Mapa.drogi(i).NrDomu.ToLower = nr AndAlso Mapa.drogi(i).Ulica.ToLower.Contains(ul) Then dr.Add(Mapa.drogi(i))
        Next

        If (wz.Count = 0 AndAlso dr.Count = 0) Then
            MessageBox.Show("Nie znaleziono elementów.")
        Else
            PokazOknoWyszukiwania(wz, dr)
        End If
    End Sub

    Private Sub wndSzukajAdresu_Closed() Handles Me.Closed
        UkryjOknoWyszAdresu()
    End Sub

    Private Sub wndSzukajAdresu_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyData = Keys.Enter Then btnSzukaj_Click()
    End Sub
End Class