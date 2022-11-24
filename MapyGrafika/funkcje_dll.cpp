#include "funkcje_dll.h"
#include "funkcje.h"

ID2D1Factory* Direct2dFactory = NULL;
ID2D1HwndRenderTarget* RenderTarget = NULL;
int wys = 0;
int szer = 0;
vector<Pedzel*> Pedzle;
vector<ID2D1PathGeometry*> Sciezki;
vector<ID2D1StrokeStyle*> Kreskowania;
ID2D1GeometrySink* Sink = NULL;
D2D1::Matrix3x2F transformacja;
IWICImagingFactory* WicFactory = NULL;
IDWriteFactory* DirectWriteFactory = NULL;
IDWriteTextFormat* FormatTekstu = NULL;
vector<DaneTekstu> Teksty;



//PedzelKolorowy* PedzZiel = NULL;




void DLL InicjalizujGrafike(HWND Okno, UINT Szerokosc, UINT Wysokosc) {
	szer = Szerokosc;
	wys = Wysokosc;
	UtworzZasobyNiezalezne();
	UtworzZasoby(Okno, Szerokosc, Wysokosc);
}

void DLL Posprzataj() {
	for (unsigned i = 0; i < Pedzle.size(); i++) {
		delete Pedzle[i];
	}

	for (unsigned i = 0; i < Sciezki.size(); i++) {
		SafeRelease(&Sciezki[i]);
	}

	for (unsigned i = 0; i < Kreskowania.size(); i++) {
		SafeRelease(&Kreskowania[i]);
	}

	SafeRelease(&FormatTekstu);
	SafeRelease(&DirectWriteFactory);
	SafeRelease(&WicFactory);
	SafeRelease(&RenderTarget);
	SafeRelease(&Direct2dFactory);
}

int DLL UtworzPedzelKolor(float r, float g, float b, float a) {
	PedzelKolorowy* p = new PedzelKolorowy(r, g, b, a);
	Pedzle.push_back(p);
	return (int)p;
}

int DLL UtworzPedzelZasob(BYTE* Zasob, DWORD Dlugosc) {
	PedzelObraz* p = new PedzelObraz(Zasob, Dlugosc);
	Pedzle.push_back(p);
	return (int)p;
}

void DLL UstawTransformacje(float dx, float dy, float s) {
	transformacja = D2D1::Matrix3x2F::Translation(dx, dy) * D2D1::Matrix3x2F::Scale(s, s, D2D1::Point2F(0, 0));
}

void DLL RysujLinie(Pedzel* pedzel, int x1, int y1, int x2, int y2, float grubosc, ID2D1StrokeStyle* styl) {
	D2D_POINT_2F p1, p2;

	p1.x = (float)x1;
	p1.y = (float)y1;
	p2.x = (float)x2;
	p2.y = (float)y2;
	RenderTarget->DrawLine(p1, p2, pedzel->pedzel, grubosc, styl);
}

void DLL RozpocznijRysowanie() {
	RenderTarget->BeginDraw();
	RenderTarget->SetTransform(D2D1::Matrix3x2F::Identity());
	RenderTarget->Clear(D2D1::ColorF(D2D1::ColorF(0.9451f, 0.93333f, 0.9098f)));
}

void DLL ZakonczRysowanie() {


	/*
	ID2D1PathGeometry* g = NULL;
	Direct2dFactory->CreatePathGeometry(&g);
	ID2D1GeometrySink* gs = NULL;
	g->Open(&gs);

	gs->BeginFigure(D2D1::Point2F(10, 10), D2D1_FIGURE_BEGIN_FILLED);
	gs->AddLine(D2D1::Point2F(100, 10));
	gs->AddLine(D2D1::Point2F(10, 100));
	gs->EndFigure(D2D1_FIGURE_END_CLOSED);

	gs->BeginFigure(D2D1::Point2F(20, 20), D2D1_FIGURE_BEGIN_FILLED);
	gs->AddLine(D2D1::Point2F(70, 20));
	gs->AddLine(D2D1::Point2F(20, 70));
	gs->EndFigure(D2D1_FIGURE_END_CLOSED);

	gs->Close();

	RenderTarget->SetTransform(D2D1::Matrix3x2F::Identity());
	ID2D1SolidColorBrush* br;
	RenderTarget->CreateSolidColorBrush(D2D1::ColorF::ColorF(0, 1, 0, 1), &br);
	RenderTarget->FillGeometry(g, br);

	SafeRelease(&gs);
	SafeRelease(&g);
	SafeRelease(&br);
	*/

	for (int i = 0; i < Teksty.size(); i++) {
		float pozx = Teksty[i].x;
		float pozy = Teksty[i].y;
		float wys_sr = 0;

		if (Teksty[i].wysrodkuj) {
			DWRITE_TEXT_METRICS m;
			IDWriteTextLayout* tl = NULL;
			DirectWriteFactory->CreateTextLayout(Teksty[i].tekst.c_str(), Teksty[i].tekst.length(), FormatTekstu, Teksty[i].szerokosc, 200, &tl);
			tl->GetMetrics(&m);
			pozy -= (m.height / 2.0);
			wys_sr = (m.height / 2.0);
			SafeRelease(&tl);
		}

		D2D1_MATRIX_3X2_F t;
		if (Teksty[i].kat != 0.0F) {
			RenderTarget->GetTransform(&t);
			RenderTarget->SetTransform(D2D1::Matrix3x2F::Rotation(Teksty[i].kat, D2D1::Point2F(pozx + (Teksty[i].szerokosc / 2.0), Teksty[i].y)));
		}


		RenderTarget->DrawTextW(Teksty[i].tekst.c_str(), Teksty[i].tekst.length(), FormatTekstu, D2D1::RectF(pozx, Teksty[i].y - wys_sr, pozx + Teksty[i].szerokosc, pozy + 200), Teksty[i].pedzel->pedzel);

		RenderTarget->SetTransform(D2D1::Matrix3x2F::Identity());
		//if (PedzZiel == NULL) PedzZiel = (PedzelKolorowy*)UtworzPedzelKolor(0, 1.0F, 0, 1.0);
		//RenderTarget->DrawEllipse(D2D1::Ellipse(D2D1::Point2F(pozx + (szerokosc / 2.0), y), 10, 10), PedzZiel->pedzel);

		if (Teksty[i].kat != 0.0F) RenderTarget->SetTransform(t);
	}

	Teksty.clear();

	RenderTarget->EndDraw();
}

void DLL ZmienRozmiar(UINT szerokosc, UINT wysokosc) {
	if (RenderTarget) RenderTarget->Resize(D2D1::SizeU(szerokosc, wysokosc));
	szer = szerokosc;
	wys = wysokosc;
}

void DLL RysujObraz(Pedzel* pedz, int x, int y) {
	if (pedz->PobierzRodzaj() != PEDZEL_OBRAZ) return;

	PedzelObraz* pedzel = (PedzelObraz*)pedz;
	D2D1_RECT_F rect;
	D2D1_MATRIX_3X2_F t;
	int sz = (int)(pedzel->PobierzSzerokosc() / 2.0);
	int w = (int)(pedzel->PobierzWysokosc() / 2.0);

	rect.left = (float)(x - sz);
	rect.top = (float)(y - w);
	rect.right = (float)(x + sz);
	rect.bottom = (float)(y + w);

	if (pedzel->PobierzSzerokosc() % 2 == 1) rect.right++;
	if (pedzel->PobierzWysokosc() % 2 == 1) rect.bottom++;

	pedzel->pedzel->GetTransform(&t);
	pedzel->pedzel->SetTransform(D2D1::Matrix3x2F::Translation(D2D1::SizeF(rect.left, rect.top)));
	RenderTarget->FillRectangle(&rect, pedzel->pedzel);

	pedzel->pedzel->SetTransform(t);
}

int DLL UtworzGeometrieSciezka() {
	ID2D1PathGeometry* sciezka = NULL;

	Direct2dFactory->CreatePathGeometry(&sciezka);
	Sciezki.push_back(sciezka);

	sciezka->Open(&Sink);
	return (int)sciezka;
}

void DLL UtworzFigure(float x, float y) {
	Sink->BeginFigure(D2D1::Point2F(x, y), D2D1_FIGURE_BEGIN_FILLED);
}

void DLL DodajLinie(float x, float y) {
	Sink->AddLine(D2D1::Point2F(x, y));
}

void DLL ZakonczFigure(int zakonczenie) {
	Sink->EndFigure((D2D1_FIGURE_END)zakonczenie);
}

void DLL ZakonczGeometrie() {
	Sink->Close();
	SafeRelease(&Sink);
}

void DLL WypelnijSciezke(int geometria, int pedzel) {
	ID2D1TransformedGeometry* tg = NULL;
	Direct2dFactory->CreateTransformedGeometry((ID2D1Geometry*)geometria, transformacja, &tg);
	RenderTarget->FillGeometry(tg, ((Pedzel*)pedzel)->pedzel);
	SafeRelease(&tg);
}

int DLL UtworzKreskowanie(int Styl) {
	ID2D1StrokeStyle* styl = NULL;

	switch (Styl) {
	case KRESKOWANIE_KROPKI:
		Direct2dFactory->CreateStrokeStyle(
			D2D1::StrokeStyleProperties(
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_ROUND,
				D2D1_LINE_JOIN_ROUND,
				10.0f,
				D2D1_DASH_STYLE_DOT,
				0.0f),
			NULL,
			0,
			&styl
		);
		break;

	case KRESKOWANIE_TORY: {
		float kreski[] = { 4.0f, 4.0f };

		Direct2dFactory->CreateStrokeStyle(
			D2D1::StrokeStyleProperties(
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_SQUARE,
				D2D1_LINE_JOIN_ROUND,
				10.0f,
				D2D1_DASH_STYLE_CUSTOM,
				0.0f),
			kreski,
			2,
			&styl
		);
		break;
	}

	case KRESKOWANIE_SCHODY: {
		float kreski[] = { 5.0f, 1.0f, 5.0f, 1.0f };

		Direct2dFactory->CreateStrokeStyle(
			D2D1::StrokeStyleProperties(
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_FLAT,
				D2D1_CAP_STYLE_FLAT,
				D2D1_LINE_JOIN_ROUND,
				10.0f,
				D2D1_DASH_STYLE_CUSTOM,
				0.0f),
			kreski,
			4,
			&styl
		);
		break;
	}

	}

	if (styl != NULL) Kreskowania.push_back(styl);
	return (int)styl;
}

void DLL RysujSciezke(int geometria, int pedzel, float grubosc, ID2D1StrokeStyle* styl) {
	// Dash array for dashStyle D2D1_DASH_STYLE_CUSTOM
	//float dashes[] = { 1.0f, 2.0f, 2.0f, 3.0f, 2.0f, 2.0f };
	//ID2D1StrokeStyle* styl = NULL;
	// Stroke Style with Dash Style -- Custom
	/*
	Direct2dFactory->CreateStrokeStyle(
		D2D1::StrokeStyleProperties(
			D2D1_CAP_STYLE_FLAT,
			D2D1_CAP_STYLE_FLAT,
			D2D1_CAP_STYLE_ROUND,
			D2D1_LINE_JOIN_ROUND,
			10.0f,
			D2D1_DASH_STYLE_DOT,
			0.0f),
		NULL,
		0,
		&styl
	);*/



	ID2D1TransformedGeometry* tg = NULL;
	Direct2dFactory->CreateTransformedGeometry((ID2D1Geometry*)geometria, transformacja, &tg);
	RenderTarget->DrawGeometry(tg, ((Pedzel*)pedzel)->pedzel, grubosc, styl);
	SafeRelease(&tg);
}

void DLL PrzesunPedzle(int dx, int dy) {
	for (unsigned i = 0; i < Pedzle.size(); i++) {
		if (Pedzle[i]->PobierzRodzaj() == PEDZEL_OBRAZ) {
			((PedzelObraz*)Pedzle[i])->Przesun(dx, dy);
		}
	}
}

void DLL RysujTekst(wchar_t* tekst, Pedzel* pedzel, float x, float y, float kat, float szerokosc, bool wysrodkuj) {
	Teksty.push_back(DaneTekstu(tekst, pedzel, x, y, kat, szerokosc, wysrodkuj));
	/*
	return;

	float pozx = x;
	float pozy = y;
	float wys_sr = 0;

	if (wysrodkuj) {
		DWRITE_TEXT_METRICS m;
		IDWriteTextLayout* tl = NULL;
		DirectWriteFactory->CreateTextLayout(tekst, wcslen(tekst), FormatTekstu, szerokosc, 200, &tl);
		tl->GetMetrics(&m);
		pozy -= (m.height / 2.0);
		wys_sr = (m.height / 2.0);
		SafeRelease(&tl);
	}

	D2D1_MATRIX_3X2_F t;
	if (kat != 0.0F) {
		RenderTarget->GetTransform(&t);
		RenderTarget->SetTransform(D2D1::Matrix3x2F::Rotation(kat, D2D1::Point2F(pozx + (szerokosc / 2.0), y)));
	}


	RenderTarget->DrawTextW(tekst, wcslen(tekst), FormatTekstu, D2D1::RectF(pozx, y - wys_sr, pozx + szerokosc, pozy + 200), pedzel->pedzel);

	RenderTarget->SetTransform(D2D1::Matrix3x2F::Identity());
	//if (PedzZiel == NULL) PedzZiel = (PedzelKolorowy*)UtworzPedzelKolor(0, 1.0F, 0, 1.0);
	//RenderTarget->DrawEllipse(D2D1::Ellipse(D2D1::Point2F(pozx + (szerokosc / 2.0), y), 10, 10), PedzZiel->pedzel);

	if (kat != 0.0F) RenderTarget->SetTransform(t);
	*/
}

DaneTekstu::DaneTekstu(wchar_t * tekst_, Pedzel * pedzel_, float x_, float y_, float kat_, float szerokosc_, bool wysrodkuj_){
	tekst = wstring(tekst_);
	pedzel = pedzel_;
	x = x_;
	y = y_;
	kat = kat_;
	szerokosc = szerokosc_;
	wysrodkuj = wysrodkuj_;
}