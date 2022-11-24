#include "pedzle.h"

extern ID2D1HwndRenderTarget* RenderTarget;
extern IWICImagingFactory* WicFactory;


#ifndef HINST_THISCOMPONENT
EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT ((HINSTANCE)&__ImageBase)
#endif


Pedzel::Pedzel() {
	this->pedzel = NULL;
}

Pedzel::~Pedzel() {
	SafeRelease(&this->pedzel);
}



PedzelKolorowy::PedzelKolorowy(float r, float g, float b, float a) {
	ID2D1SolidColorBrush* p;
	RenderTarget->CreateSolidColorBrush(D2D1::ColorF::ColorF(r, g, b, a), &p);
	this->pedzel = p;
}

PedzelKolorowy::~PedzelKolorowy() {
	Pedzel::~Pedzel();
}



PedzelObraz::PedzelObraz(BYTE* Zasob, DWORD Dlugosc) {
	this->UtworzPedzelZasob(Zasob, Dlugosc);
	this->dx = this->dy = 0;
}

PedzelObraz::~PedzelObraz() {
	Pedzel::~Pedzel();
}

void PedzelObraz::Przesun(int dx, int dy) {
	this->dx = (this->dx + dx) % this->szer;
	this->dy = (this->dy + dy) % this->wys;
	this->pedzel->SetTransform(D2D1::Matrix3x2F::Translation(D2D1::SizeF((float)this->dx, (float)this->dy)));
}

void PedzelObraz::UtworzPedzelZasob(BYTE* Zasob, DWORD Dlugosc) {
	ID2D1Bitmap *obraz_d2d = NULL;
	IWICBitmapDecoder *pDecoder = NULL;
	IWICBitmapFrameDecode *pSource = NULL;
	IWICStream *pStream = NULL;
	IWICFormatConverter *pConverter = NULL;
	ID2D1BitmapBrush* p = NULL;

	WicFactory->CreateStream(&pStream);
	pStream->InitializeFromMemory(Zasob, Dlugosc);
	WicFactory->CreateDecoderFromStream(pStream, NULL, WICDecodeMetadataCacheOnLoad, &pDecoder);
	pDecoder->GetFrame(0, &pSource);

	WicFactory->CreateFormatConverter(&pConverter);
	pConverter->Initialize(pSource, GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone, NULL, 0.f, WICBitmapPaletteTypeMedianCut);

	RenderTarget->CreateBitmapFromWicBitmap(pConverter, NULL, &obraz_d2d);

	RenderTarget->CreateBitmapBrush(obraz_d2d, &p);
	p->SetExtendModeX(D2D1_EXTEND_MODE_WRAP);
	p->SetExtendModeY(D2D1_EXTEND_MODE_WRAP);
	this->pedzel = p;

	this->szer = (int)obraz_d2d->GetSize().width;
	this->wys = (int)obraz_d2d->GetSize().height;

	SafeRelease(&pDecoder);
	SafeRelease(&pSource);
	SafeRelease(&pStream);
	SafeRelease(&pConverter);
	SafeRelease(&obraz_d2d);
}