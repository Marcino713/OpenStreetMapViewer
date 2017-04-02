#pragma once
#ifndef funkcje_h
#define funkcje_h

#include <Windows.h>
#include <d2d1.h>
#include <wincodec.h>

HRESULT UtworzZasobyNiezalezne();
HRESULT UtworzZasoby(HWND Okno, UINT Szerokosc, UINT Wysokosc);

#endif