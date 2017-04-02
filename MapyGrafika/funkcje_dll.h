#pragma once
#ifndef MapyGrafika_h
#define MapyGrafika_h

#define DLL __declspec(dllexport) _stdcall

#include <vector>

#include <stdlib.h>
#include <malloc.h>
#include <memory.h>
#include <wchar.h>
#include <math.h>

#include <d2d1.h>
#include <d2d1helper.h>
#include <dwrite.h>
#include <wincodec.h>

#include "makra.h"
#include "pedzle.h"

#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "Windowscodecs.lib")

using std::vector;

extern "C" {
	void DLL InicjalizujGrafike(HWND Okno, UINT Szerokosc, UINT Wysokosc);
	void DLL Posprzataj();
	int DLL UtworzPedzelKolor(float r, float g, float b);
	int DLL UtworzPedzelObraz(int Zasob, typ_grafiki Typ);
	void DLL UstawTransformacje(float dx, float dy, float s);
	void DLL RysujLinie(Pedzel* pedzel, int x1, int y1, int x2, int y2, float grubosc = 1.0F);
	void DLL RozpocznijRysowanie();
	void DLL ZakonczRysowanie();
	void DLL ZmienRozmiar(UINT szerokosc, UINT wysokosc);
	void DLL RysujObraz(PedzelObraz* pedzel, int x, int y);
	int DLL UtworzGeometrieSciezka(float x, float y);
	void DLL DodajLinie(float x, float y);
	void DLL ZakonczGeometrie(int zakonczenie);
	void DLL WypelnijSciezke(int geometria, int pedzel);
	void DLL RysujSciezke(int geometria, int pedzel, float grubosc = 0.0F);
	void DLL PrzesunPedzle(int dx, int dy);
	int DLL PobierzRozmiarPedzla(Pedzel* pedzel);
}

#endif