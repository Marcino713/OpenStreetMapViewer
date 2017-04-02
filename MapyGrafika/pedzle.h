#pragma once
#ifndef pedzle_h
#define pedzle_h

#include <d2d1.h>
#include "makra.h"
#include <wincodec.h>

#pragma comment(lib, "Windowscodecs.lib")

const int PEDZEL_KOLOROWY = 1;
const int PEDZEL_OBRAZ = 2;

class Pedzel {
public:
	ID2D1Brush* pedzel;

	Pedzel();
	virtual ~Pedzel();
	virtual int PobierzRodzaj() = 0;
};

class PedzelKolorowy :public Pedzel {
public:
	PedzelKolorowy(float r, float g, float b);
	virtual ~PedzelKolorowy();
	virtual int PobierzRodzaj() { return PEDZEL_KOLOROWY; }

private:
	float r, g, b;
};

class PedzelObraz :public Pedzel {
public:
	PedzelObraz(int Zasob, typ_grafiki Typ);
	virtual ~PedzelObraz();
	int PobierzWysokosc() { return this->wys; }
	int PobierzSzerokosc() { return this->szer; }
	void Przesun(int dx, int dy);
	virtual int PobierzRodzaj() { return PEDZEL_OBRAZ; }

private:
	int wys, szer;
	int dx, dy;

	void UtworzPedzelBmp(int Zasob);
	void UtworzPedzelPng(int Zasob);
};

#endif