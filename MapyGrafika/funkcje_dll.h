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
//#include <>

#include <d2d1.h>
#include <d2d1helper.h>
#include <dwrite.h>
#include <wincodec.h>

#include "makra.h"
#include "pedzle.h"

#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "Dwrite.lib")
#pragma comment(lib, "Windowscodecs.lib")

using std::vector;
using std::wstring;

const int KRESKOWANIE_KROPKI = 1;
const int KRESKOWANIE_TORY = 2;
const int KRESKOWANIE_SCHODY = 3;

class DaneTekstu {
public:
	wstring tekst;
	Pedzel* pedzel;
	float x, y, kat, szerokosc;
	bool wysrodkuj;
	DaneTekstu(wchar_t* tekst_, Pedzel* pedzel_, float x_, float y_, float kat_, float szerokosc_, bool wysrodkuj_);
};

extern "C" {
	void DLL InicjalizujGrafike(HWND Okno, UINT Szerokosc, UINT Wysokosc);
	void DLL Posprzataj();
	int DLL UtworzPedzelKolor(float r, float g, float b, float a = 1.0f);
	int DLL UtworzPedzelZasob(BYTE* Zasob, DWORD Dlugosc);
	void DLL UstawTransformacje(float dx, float dy, float s);
	void DLL RysujLinie(Pedzel* pedzel, int x1, int y1, int x2, int y2, float grubosc = 1.0F, ID2D1StrokeStyle* styl = NULL);
	void DLL RozpocznijRysowanie();
	void DLL ZakonczRysowanie();
	void DLL ZmienRozmiar(UINT szerokosc, UINT wysokosc);
	void DLL RysujObraz(Pedzel* pedz, int x, int y);
	int DLL UtworzGeometrieSciezka();
	void DLL UtworzFigure(float x, float y);
	void DLL DodajLinie(float x, float y);
	void DLL ZakonczFigure(int zakonczenie);
	void DLL ZakonczGeometrie();
	void DLL WypelnijSciezke(int geometria, int pedzel);
	int DLL UtworzKreskowanie(int Styl);
	void DLL RysujSciezke(int geometria, int pedzel, float grubosc = 0.0F, ID2D1StrokeStyle* styl = NULL);
	void DLL PrzesunPedzle(int dx, int dy);
	void DLL RysujTekst(wchar_t* tekst, Pedzel* pedzel, float x, float y, float kat = 0.0F, float szerokosc = 76.0F, bool wysrodkuj = false);
}

#endif