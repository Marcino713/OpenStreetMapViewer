﻿Friend Module App
    Friend Const ILOSC_PEDZLI As Integer = 80
    Friend Const BRAK_PEDZLA As Byte = 255

    Friend Mapa As New MapaKlasa
    Friend Okno As wndOkno
    Friend Pedzle(ILOSC_PEDZLI - 1) As PedzleStr
    Friend PedzleWszystkie(ILOSC_PEDZLI - 1) As DaneMalowania

    Private czarny, czerwony, pomaranczowy, zielony, szary, szary_droga, motorway, trunk, primary, niebieski, las, raceway, barrier_wall, barrier_yes, nawierzchnia, landuse_railway, aeroway_apron, power_line, manmade_cutline, manmade_pier, leisure_garden, leisure_park, leisure_pitch, leisure_sportscentre, leisure_track, amenity_college, tourism_attraction As IntPtr
    Private allotments, brownfield, cemetery, commercial, construction, farmland, farmyard, forest, garages, grass, greenfield, greenhouse_horticulture, industrial, landfill, meadow, military, orchard, quarry, recreation_ground, reservoir, residential, retail, village_green, vineyard As IntPtr
    Private amenity_fastfood, building, power_tower1, power_tower2, aeroway_aerodrome, aeroway_helipad, amenity_bar, amenity_bbq, amenity_biergarten, amenity_cafe, amenity_drinkwater, amenity_icecream, amenity_pub, amenity_restaurant, barrier_gate, barrier_liftgate, highway_busstop, highway_elevator, highway_signals, leisure_golfcourse, leisure_minigolf, leisure_playground, leisure_waterpark, manmade_lighthouse, manmade_mast, manmade_watertower, manmade_windmill, railway_crossing, railway_subentrance As IntPtr
    Private railway_tram, secondary, bialy As IntPtr

    Friend Sub PrzeliczWspolrzedne(ByRef lat As Single, ByRef lon As Single)
        lat *= 1.852F
    End Sub

    Friend Sub InicjalizujProgram()

        'Utworzenie pedzli
        czarny = UtworzPedzelKolor(0, 0, 0)
        czerwony = UtworzPedzelKolor(1, 0, 0)
        pomaranczowy = UtworzPedzelKolor(1, 0.64706, 0)
        zielony = UtworzPedzelKolor(0, 1, 0)
        szary = UtworzPedzelKolor(0.36078, 0.36078, 0.36078)
        szary_droga = UtworzPedzelKolor(0.76863, 0.76863, 0.76863)
        motorway = UtworzPedzelKolor(0.91373, 0.56471, 0.62745)
        trunk = UtworzPedzelKolor(0.98431, 0.69804, 0.60392)
        primary = UtworzPedzelKolor(0.99216, 0.84314, 0.63137)
        niebieski = UtworzPedzelKolor(0.70589, 0.81569, 0.81961)
        las = UtworzPedzelObraz(105, TypGrafiki.bmp)
        raceway = UtworzPedzelKolor(1, 0.75294, 0.79216)
        barrier_wall = UtworzPedzelKolor(0.66667, 0.66275, 0.64706)
        barrier_yes = UtworzPedzelKolor(0.7098, 0.70196, 0.6901)
        nawierzchnia = UtworzPedzelKolor(0.77647, 0.61177, 0.28628)
        landuse_railway = UtworzPedzelKolor(0.92549, 0.84706, 1)
        aeroway_apron = UtworzPedzelKolor(0.91373, 0.81961, 1)
        power_line = UtworzPedzelKolor(0.53725, 0.53725, 0.53725)
        manmade_cutline = UtworzPedzelKolor(0.9451, 0.8902, 0.88627)
        manmade_pier = UtworzPedzelKolor(0.86667, 0.86667, 0.9098)
        leisure_garden = UtworzPedzelKolor(0.81176, 0.92549, 0.65882)
        leisure_park = UtworzPedzelKolor(0.80392, 0.96863, 0.78824)
        leisure_pitch = UtworzPedzelKolor(0.54118, 0.82745, 0.68627)
        leisure_sportscentre = UtworzPedzelKolor(0.13725, 0.80392, 0.59608)
        leisure_track = UtworzPedzelKolor(0.74118, 0.8902, 0.79608)
        amenity_college = UtworzPedzelKolor(0.94118, 0.9451, 0.84314)
        cemetery = UtworzPedzelObraz(107, TypGrafiki.bmp)
        tourism_attraction = UtworzPedzelKolor(0.94902, 0.79216, 0.91765)

        allotments = UtworzPedzelObraz(108, TypGrafiki.bmp)
        brownfield = UtworzPedzelKolor(0.71373, 0.71373, 0.56471)
        commercial = UtworzPedzelKolor(0.93333, 0.81176, 0.81176)
        construction = brownfield
        farmland = UtworzPedzelKolor(0.97647, 0.9098, 0.79608)
        farmyard = UtworzPedzelKolor(0.91765, 0.8, 0.64314)
        garages = UtworzPedzelKolor(0.87059, 0.86667, 0.8)
        grass = UtworzPedzelKolor(0.81176, 0.92941, 0.64706)
        greenfield = UtworzPedzelKolor(0.9451, 0.93333, 0.9098)
        greenhouse_horticulture = farmland
        industrial = UtworzPedzelKolor(0.90196, 0.81961, 0.8902)
        landfill = brownfield
        meadow = UtworzPedzelKolor(0.81176, 0.92549, 0.65882)
        military = UtworzPedzelObraz(109, TypGrafiki.bmp)
        orchard = UtworzPedzelObraz(110, TypGrafiki.bmp)
        quarry = UtworzPedzelObraz(111, TypGrafiki.bmp)
        recreation_ground = UtworzPedzelKolor(0.81176, 0.92941, 0.64706)
        reservoir = niebieski
        residential = UtworzPedzelKolor(0.8549, 0.8549, 0.8549)
        retail = UtworzPedzelKolor(0.99608, 0.79216, 0.77255)
        village_green = grass
        vineyard = UtworzPedzelObraz(112, TypGrafiki.bmp)

        amenity_fastfood = UtworzPedzelObraz(113, TypGrafiki.png)
        building = UtworzPedzelKolor(0.85098, 0.81569, 0.78824)
        power_tower1 = UtworzPedzelObraz(114, TypGrafiki.bmp)
        power_tower2 = UtworzPedzelObraz(115, TypGrafiki.bmp)
        aeroway_aerodrome = UtworzPedzelObraz(116, TypGrafiki.png)
        aeroway_helipad = UtworzPedzelObraz(117, TypGrafiki.png)
        amenity_bar = UtworzPedzelObraz(118, TypGrafiki.png)
        amenity_bbq = UtworzPedzelObraz(119, TypGrafiki.png)
        amenity_biergarten = UtworzPedzelObraz(120, TypGrafiki.png)
        amenity_cafe = UtworzPedzelObraz(121, TypGrafiki.png)
        amenity_drinkwater = UtworzPedzelObraz(122, TypGrafiki.png)
        amenity_icecream = UtworzPedzelObraz(123, TypGrafiki.png)
        amenity_pub = UtworzPedzelObraz(124, TypGrafiki.png)
        amenity_restaurant = UtworzPedzelObraz(125, TypGrafiki.png)
        barrier_gate = UtworzPedzelObraz(126, TypGrafiki.png)
        barrier_liftgate = UtworzPedzelObraz(127, TypGrafiki.png)
        highway_busstop = UtworzPedzelObraz(128, TypGrafiki.png)
        highway_elevator = UtworzPedzelObraz(129, TypGrafiki.png)
        highway_signals = UtworzPedzelObraz(130, TypGrafiki.png)
        leisure_golfcourse = UtworzPedzelObraz(131, TypGrafiki.png)
        leisure_minigolf = UtworzPedzelObraz(132, TypGrafiki.png)
        leisure_playground = UtworzPedzelObraz(133, TypGrafiki.png)
        leisure_waterpark = UtworzPedzelObraz(134, TypGrafiki.png)
        manmade_lighthouse = UtworzPedzelObraz(135, TypGrafiki.png)
        manmade_mast = UtworzPedzelObraz(136, TypGrafiki.png)
        manmade_watertower = UtworzPedzelObraz(137, TypGrafiki.png)
        manmade_windmill = UtworzPedzelObraz(138, TypGrafiki.png)
        railway_crossing = UtworzPedzelObraz(139, TypGrafiki.png)
        railway_subentrance = UtworzPedzelObraz(140, TypGrafiki.png)

        railway_tram = UtworzPedzelKolor(0.26667, 0.26667, 0.26667)
        secondary = UtworzPedzelKolor(0.96471, 0.98039, 0.73333)
        bialy = UtworzPedzelKolor(1, 1, 1)

        'Utworzenie danych malowania
        PedzleWszystkie(0) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, czarny))
        PedzleWszystkie(1) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, czerwony))
        PedzleWszystkie(2) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, pomaranczowy))
        PedzleWszystkie(3) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, zielony))
        PedzleWszystkie(4) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, szary))
        PedzleWszystkie(5) = New DaneMalowania(New DaneMalowaniaPoziom(1, 4, bialy, 4), New DaneMalowaniaPoziom(5, 10, szary_droga, 2))
        PedzleWszystkie(6) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, motorway, 7))
        PedzleWszystkie(7) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, trunk, 7))
        PedzleWszystkie(8) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, primary, 5))
        PedzleWszystkie(9) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, niebieski))
        PedzleWszystkie(10) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, las))
        PedzleWszystkie(11) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, raceway))
        PedzleWszystkie(12) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, barrier_wall))
        PedzleWszystkie(13) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, barrier_yes))
        PedzleWszystkie(14) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, nawierzchnia))
        PedzleWszystkie(15) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, landuse_railway))
        PedzleWszystkie(16) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, aeroway_apron))
        PedzleWszystkie(17) = New DaneMalowania(New DaneMalowaniaPoziom(1, 7, power_line))
        PedzleWszystkie(18) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, manmade_cutline))
        PedzleWszystkie(19) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, manmade_pier))
        PedzleWszystkie(20) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, leisure_garden))
        PedzleWszystkie(21) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, leisure_park))
        PedzleWszystkie(22) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, leisure_pitch))
        PedzleWszystkie(23) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, leisure_sportscentre))
        PedzleWszystkie(24) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, leisure_track))
        PedzleWszystkie(25) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, amenity_college))
        PedzleWszystkie(26) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, tourism_attraction))
        PedzleWszystkie(27) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, allotments))
        PedzleWszystkie(28) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, brownfield))
        PedzleWszystkie(29) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, commercial))
        PedzleWszystkie(30) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, construction))
        PedzleWszystkie(31) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, farmland))
        PedzleWszystkie(32) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, farmyard))
        PedzleWszystkie(33) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, garages))
        PedzleWszystkie(34) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, grass))
        PedzleWszystkie(35) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, greenfield))
        PedzleWszystkie(36) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, greenhouse_horticulture))
        PedzleWszystkie(37) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, industrial))
        PedzleWszystkie(38) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, landfill))
        PedzleWszystkie(39) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, meadow))
        PedzleWszystkie(40) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, military))
        PedzleWszystkie(41) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, orchard))
        PedzleWszystkie(42) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, quarry))
        PedzleWszystkie(43) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, recreation_ground))
        PedzleWszystkie(44) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, reservoir))
        PedzleWszystkie(45) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, residential))
        PedzleWszystkie(46) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, retail))
        PedzleWszystkie(47) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, village_green))
        PedzleWszystkie(48) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, vineyard))
        PedzleWszystkie(49) = New DaneMalowania(New DaneMalowaniaPoziom(1, 10, cemetery))
        PedzleWszystkie(50) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_fastfood))
        PedzleWszystkie(51) = New DaneMalowania(New DaneMalowaniaPoziom(1, 6, building))
        PedzleWszystkie(52) = New DaneMalowania(New DaneMalowaniaPoziom(1, 4, power_tower1), New DaneMalowaniaPoziom(5, 7, power_tower2))
        PedzleWszystkie(53) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, aeroway_aerodrome))
        PedzleWszystkie(54) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, aeroway_helipad))
        PedzleWszystkie(55) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_bar))
        PedzleWszystkie(56) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_bbq))
        PedzleWszystkie(57) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_biergarten))
        PedzleWszystkie(58) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_cafe))
        PedzleWszystkie(59) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_drinkwater))
        PedzleWszystkie(60) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_icecream))
        PedzleWszystkie(61) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_pub))
        PedzleWszystkie(62) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, amenity_restaurant))
        PedzleWszystkie(63) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, barrier_gate))
        PedzleWszystkie(64) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, barrier_liftgate))
        PedzleWszystkie(65) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, highway_busstop))
        PedzleWszystkie(66) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, highway_elevator))
        PedzleWszystkie(67) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, highway_signals))
        PedzleWszystkie(68) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, leisure_golfcourse))
        PedzleWszystkie(69) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, leisure_minigolf))
        PedzleWszystkie(70) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, leisure_playground))
        PedzleWszystkie(71) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, leisure_waterpark))
        PedzleWszystkie(72) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, manmade_lighthouse))
        PedzleWszystkie(73) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, manmade_mast))
        PedzleWszystkie(74) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, manmade_watertower))
        PedzleWszystkie(75) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, manmade_windmill))
        PedzleWszystkie(76) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, railway_crossing))
        PedzleWszystkie(77) = New DaneMalowania(New DaneMalowaniaPoziom(1, 2, railway_subentrance))
        PedzleWszystkie(78) = New DaneMalowania(New DaneMalowaniaPoziom(1, 9, railway_tram, 2))
        PedzleWszystkie(79) = New DaneMalowania(New DaneMalowaniaPoziom(1, 9, secondary, 4))
    End Sub

    Friend Sub UstawPedzle(Poziom As Integer)

        For i As Integer = 0 To ILOSC_PEDZLI - 1
            Pedzle(i) = PedzleWszystkie(i).PobierzPedzle(Poziom)
        Next

    End Sub

End Module