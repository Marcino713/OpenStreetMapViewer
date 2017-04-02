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



PedzelKolorowy::PedzelKolorowy(float r, float g, float b) {
	this->r = r;
	this->g = g;
	this->b = b;

	ID2D1SolidColorBrush* p;
	RenderTarget->CreateSolidColorBrush(D2D1::ColorF::ColorF(r, g, b), &p);
	this->pedzel = p;
}

PedzelKolorowy::~PedzelKolorowy() {
	Pedzel::~Pedzel();
}



PedzelObraz::PedzelObraz(int Zasob, typ_grafiki Typ) {
	if (Typ == typ_grafiki::bmp) {
		this->UtworzPedzelBmp(Zasob);
	} else {
		this->UtworzPedzelPng(Zasob);
	}

	this->dx = this->dy = 0;
}

PedzelObraz::~PedzelObraz() {
	Pedzel::~Pedzel();
}

void PedzelObraz::Przesun(int dx, int dy) {
	this->dx = (this->dx + dx) % this->szer;
	this->dy = (this->dy + dy) % this->wys;
	this->pedzel->SetTransform(D2D1::Matrix3x2F::Translation(D2D1::SizeF(this->dx, this->dy)));
}

void PedzelObraz::UtworzPedzelBmp(int Zasob) {
	IWICImagingFactory *pFactory = NULL;
	HBITMAP obraz_h = NULL;
	IWICBitmap* obraz_wic = NULL;
	ID2D1Bitmap* obraz_d2d = NULL;
	ID2D1BitmapBrush* p;

	CoCreateInstance(CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, IID_IWICImagingFactory, (LPVOID*)&pFactory);

	obraz_h = LoadBitmap(HINST_THISCOMPONENT, MAKEINTRESOURCEW(Zasob));
	if (obraz_h == NULL) {
		MessageBoxW(NULL, L"B³¹d: LoadBitmap", L"B³¹d", 0);
		return;
	}
	pFactory->CreateBitmapFromHBITMAP(obraz_h, NULL, WICBitmapAlphaChannelOption::WICBitmapIgnoreAlpha, &obraz_wic);

	RenderTarget->CreateBitmapFromWicBitmap(obraz_wic, NULL, &obraz_d2d);
	RenderTarget->CreateBitmapBrush(obraz_d2d, &p);
	p->SetExtendModeX(D2D1_EXTEND_MODE_WRAP);
	p->SetExtendModeY(D2D1_EXTEND_MODE_WRAP);
	this->pedzel = p;

	this->szer = obraz_d2d->GetSize().width;
	this->wys = obraz_d2d->GetSize().height;

	SafeRelease(&obraz_d2d);
	SafeRelease(&obraz_wic);
	DeleteObject(obraz_h);
	SafeRelease(&pFactory);
}

void PedzelObraz::UtworzPedzelPng(int Zasob) {
	ID2D1Bitmap *obraz_d2d = NULL;
	IWICBitmapDecoder *pDecoder = NULL;
	IWICBitmapFrameDecode *pSource = NULL;
	IWICStream *pStream = NULL;
	IWICFormatConverter *pConverter = NULL;
	ID2D1BitmapBrush* p = NULL;
	HRSRC imageResHandle = NULL;
	HGLOBAL imageResDataHandle = NULL;
	void *pImageFile = NULL;
	DWORD imageFileSize = 0;

	imageResHandle = FindResource(HINST_THISCOMPONENT, MAKEINTRESOURCE(Zasob), RT_RCDATA);
	imageResDataHandle = LoadResource(HINST_THISCOMPONENT, imageResHandle);
	pImageFile = LockResource(imageResDataHandle);
	imageFileSize = SizeofResource(HINST_THISCOMPONENT, imageResHandle);

	WicFactory->CreateStream(&pStream);
	pStream->InitializeFromMemory(reinterpret_cast<BYTE*>(pImageFile), imageFileSize);
	WicFactory->CreateDecoderFromStream(pStream, NULL, WICDecodeMetadataCacheOnLoad, &pDecoder);
	pDecoder->GetFrame(0, &pSource);

	WicFactory->CreateFormatConverter(&pConverter);
	pConverter->Initialize(pSource, GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone, NULL, 0.f, WICBitmapPaletteTypeMedianCut);

	RenderTarget->CreateBitmapFromWicBitmap(pConverter, NULL, &obraz_d2d);

	RenderTarget->CreateBitmapBrush(obraz_d2d, &p);
	p->SetExtendModeX(D2D1_EXTEND_MODE_WRAP);
	p->SetExtendModeY(D2D1_EXTEND_MODE_WRAP);
	this->pedzel = p;

	this->szer = obraz_d2d->GetSize().width;
	this->wys = obraz_d2d->GetSize().height;

	SafeRelease(&pDecoder);
	SafeRelease(&pSource);
	SafeRelease(&pStream);
	SafeRelease(&pConverter);
	SafeRelease(&obraz_d2d);
}