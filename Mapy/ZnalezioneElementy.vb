Imports System.Text

Friend Class wndZnalezioneElementy
    Private Wikipedia As String = ""
    Private StronaWWW As String = ""

    Friend Sub PokazElementy(Wezly As List(Of Wezel), Drogi As List(Of Droga))
        UstawZaznaczonyElement(Nothing)
        lvWlasciwosci.Items.Clear()
        lstElementy.Items.Clear()
        Dim t As New StringBuilder

        For i As Integer = 0 To Wezly.Count - 1
            t.Append(Wezly(i).danem.Nazwa)
            If Wezly(i).tekst <> "" Then
                t.Append(": ")
                t.Append(Wezly(i).tekst)
            End If
            lstElementy.Items.Add(New ElementListy(t.ToString(), Wezly(i).Wartosci, Wezly(i)))
            t.Clear()
        Next

        For i As Integer = 0 To Drogi.Count - 1
            t.Append(Drogi(i).danem.Nazwa)
            If Drogi(i).tekst <> "" Then
                t.Append(": ")
                t.Append(Drogi(i).tekst)
            End If
            lstElementy.Items.Add(New ElementListy(t.ToString(), Drogi(i).Wartosci, Drogi(i)))
            t.Clear()
        Next
    End Sub

    Private Sub lstElementy_SelectedValueChanged(sender As Object, e As EventArgs) Handles lstElementy.SelectedValueChanged
        If lstElementy.SelectedItem Is Nothing Then
            UstawZaznaczonyElement(Nothing)
            Exit Sub
        End If

        Wikipedia = ""
        StronaWWW = ""

        lvWlasciwosci.Items.Clear()
        Dim el As ElementListy = CType(lstElementy.SelectedItem, ElementListy)
        UstawZaznaczonyElement(el.Element)
        For i As Integer = 0 To el.Wartosci.Length - 1
            Dim lvi As New ListViewItem({el.Wartosci(i).Klucz, el.Wartosci(i).Wartosc})
            lvWlasciwosci.Items.Add(lvi)

            If el.Wartosci(i).Klucz.StartsWith("wikipedia") Then
                Wikipedia = el.Wartosci(i).Wartosc
                If el.Wartosci(i).Klucz.Contains(":") Then
                    Try
                        Wikipedia = el.Wartosci(i).Klucz.Split(":"c)(1) & ":" & Wikipedia
                    Catch
                    End Try
                End If
            End If

            If el.Wartosci(i).Klucz = "website" Then StronaWWW = el.Wartosci(i).Wartosc

        Next

        btnWikipedia.Enabled = (Wikipedia <> "")
        btnStronaWWW.Enabled = (StronaWWW <> "")
    End Sub

    Private Sub wndZnalezioneElementy_Closed() Handles Me.Closed
        UstawZaznaczonyElement(Nothing)
        UkryjOknoWyszukiwania()
    End Sub

    Private Class ElementListy
        Private Nazwa As String
        Friend Wartosci As KluczWartosc()
        Friend Element As Object
        Friend Sub New(Nazwa As String, Wartosci As KluczWartosc(), Element As Object)
            Me.Nazwa = Nazwa
            Me.Wartosci = Wartosci
            Me.Element = Element
        End Sub
        Public Overrides Function ToString() As String
            Return Nazwa
        End Function
    End Class

    Private Sub btnSzukaj_Click() Handles btnSzukaj.Click
        If txtSzukaj.Text = "" Then Exit Sub
        Dim t As String = txtSzukaj.Text.ToLower

        Dim wz As New List(Of Wezel)
        Dim dr As New List(Of Droga)

        For i As Integer = 0 To Mapa.wezly.Length - 1
            If Mapa.wezly(i).Wartosci IsNot Nothing AndAlso Mapa.wezly(i).tekst.ToLower.Contains(t) Then wz.Add(Mapa.wezly(i))
        Next

        For i As Integer = 0 To Mapa.drogi.Length - 1
            If Mapa.drogi(i).Wartosci IsNot Nothing AndAlso Mapa.drogi(i).tekst.ToLower.Contains(t) Then dr.Add(Mapa.drogi(i))
        Next

        If wz.Count = 0 AndAlso dr.Count = 0 Then
            MessageBox.Show("Nie znaleziono żadnych elementów.")
        Else
            PokazElementy(wz, dr)
        End If

    End Sub

    Private Sub txtSzukaj_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSzukaj.KeyUp
        If e.KeyData = Keys.Enter Then btnSzukaj_Click()
    End Sub

    Private Sub btnWikipedia_Click(sender As Object, e As EventArgs) Handles btnWikipedia.Click
        If Wikipedia = "" Then Exit Sub
        Process.Start("https://wikipedia.org/wiki/" + Wikipedia)
    End Sub

    Private Sub btnStronaWWW_Click() Handles btnStronaWWW.Click
        If StronaWWW = "" Then Exit Sub
        Process.Start(StronaWWW)
    End Sub

    Private Sub btnWysrodkuj_Click() Handles btnWysrodkuj.Click

    End Sub
End Class