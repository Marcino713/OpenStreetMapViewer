#include "funkcje_dll.h"
#include "funkcje.h"

ID2D1Factory* Direct2dFactory;
ID2D1HwndRenderTarget* RenderTarget;
int wys = 0;
int szer = 0;
vector<Pedzel*> Pedzle;
vector<ID2D1PathGeometry*> Sciezki;
ID2D1GeometrySink* Sink = NULL;
D2D1::Matrix3x2F transformacja;
IWICImagingFactory* WicFactory;


void DLL InicjalizujGrafike(HWND Okno, UINT Szerokosc, UINT Wysokosc) {
	szer = Szerokosc;
	wys = Wysokosc;
	Direct2dFactory = NULL;
	RenderTarget = NULL;
	WicFactory = NULL;
	UtworzZasobyNiezalezne();
	UtworzZasoby(Okno, Szerokosc, Wysokosc);
}

void DLL Posprzataj() {
	for (int i = 0; i < Pedzle.size(); i++) {
		delete Pedzle[i];
	}

	for (int i = 0; i < Sciezki.size(); i++) {
		SafeRelease(&Sciezki[i]);
	}

	SafeRelease(&WicFactory);
	SafeRelease(&RenderTarget);
	SafeRelease(&Direct2dFactory);
}

int DLL UtworzPedzelKolor(float r, float g, float b) {
	PedzelKolorowy* p = new PedzelKolorowy(r, g, b);
	Pedzle.push_back(p);
	return (int)p;
}

int DLL UtworzPedzelObraz(int Zasob, typ_grafiki Typ) {
	PedzelObraz* p = new PedzelObraz(Zasob, Typ);
	Pedzle.push_back(p);
	return (int)p;
}

void DLL UstawTransformacje(float dx, float dy, float s) {
	transformacja = D2D1::Matrix3x2F::Translation(dx, dy) * D2D1::Matrix3x2F::Scale(s, s, D2D1::Point2F(0, 0));
}

void DLL RysujLinie(Pedzel* pedzel, int x1, int y1, int x2, int y2, float grubosc) {
	D2D_POINT_2F p1, p2;

	p1.x = (float)x1;
	p1.y = (float)y1;
	p2.x = (float)x2;
	p2.y = (float)y2;
	RenderTarget->DrawLine(p1, p2, pedzel->pedzel, grubosc);
}

void DLL RozpocznijRysowanie() {
	RenderTarget->BeginDraw();
	RenderTarget->SetTransform(D2D1::Matrix3x2F::Identity());
	RenderTarget->Clear(D2D1::ColorF(D2D1::ColorF(0.9451, 0.93333, 0.9098)));
}

void DLL ZakonczRysowanie() {
	RenderTarget->EndDraw();
}

void DLL ZmienRozmiar(UINT szerokosc, UINT wysokosc) {
	if (RenderTarget) RenderTarget->Resize(D2D1::SizeU(szerokosc, wysokosc));
	szer = szerokosc;
	wys = wysokosc;
}

void DLL RysujObraz(PedzelObraz* pedzel, int x, int y) {
	D2D1_RECT_F rect;
	D2D1_MATRIX_3X2_F t;
	int sz = pedzel->PobierzSzerokosc() / 2.0;
	int w = pedzel->PobierzWysokosc() / 2.0;
	
	rect.left = x - sz;
	rect.top = y - w;
	rect.right = x + sz;
	rect.bottom = y + w;

	if (pedzel->PobierzSzerokosc() % 2 == 1) rect.right++;
	if (pedzel->PobierzWysokosc() % 2 == 1) rect.bottom++;

	pedzel->pedzel->GetTransform(&t);
	pedzel->pedzel->SetTransform(D2D1::Matrix3x2F::Translation(D2D1::SizeF(rect.left, rect.top)));
	RenderTarget->FillRectangle(&rect, pedzel->pedzel);

	pedzel->pedzel->SetTransform(t);
}

int DLL UtworzGeometrieSciezka(float x, float y) {
	ID2D1PathGeometry* sciezka = NULL;

	Direct2dFactory->CreatePathGeometry(&sciezka);
	Sciezki.push_back(sciezka);

	sciezka->Open(&Sink);
	Sink->BeginFigure(D2D1::Point2F(x, y), D2D1_FIGURE_BEGIN_FILLED);

	return (int)sciezka;
}

void DLL DodajLinie(float x, float y) {
	Sink->AddLine(D2D1::Point2F(x, y));
}

void DLL ZakonczGeometrie(int zakonczenie) {
	Sink->EndFigure((D2D1_FIGURE_END)zakonczenie);
	Sink->Close();
	SafeRelease(&Sink);
}

void DLL WypelnijSciezke(int geometria, int pedzel) {
	ID2D1TransformedGeometry* tg = NULL;
	Direct2dFactory->CreateTransformedGeometry((ID2D1Geometry*)geometria, transformacja, &tg);
	RenderTarget->FillGeometry(tg, ((Pedzel*)pedzel)->pedzel);
	SafeRelease(&tg);
}

void DLL RysujSciezke(int geometria, int pedzel, float grubosc) {
	ID2D1TransformedGeometry* tg = NULL;
	Direct2dFactory->CreateTransformedGeometry((ID2D1Geometry*)geometria, transformacja, &tg);
	RenderTarget->DrawGeometry(tg, ((Pedzel*)pedzel)->pedzel, grubosc);
	SafeRelease(&tg);
}

void DLL PrzesunPedzle(int dx, int dy) {
	for (int i = 0; i < Pedzle.size(); i++) {
		if (Pedzle[i]->PobierzRodzaj() == PEDZEL_OBRAZ) {
			((PedzelObraz*)Pedzle[i])->Przesun(dx, dy);
		}
	}
}

int DLL PobierzRozmiarPedzla(Pedzel* pedzel) {
	if (pedzel->PobierzRodzaj() != PEDZEL_OBRAZ) return 0;

	int rozm = 0;
	rozm = ((PedzelObraz*)pedzel)->PobierzSzerokosc() << 16;
	rozm |= (((PedzelObraz*)pedzel)->PobierzWysokosc() & 0x0000FFFF);
	return rozm;
}