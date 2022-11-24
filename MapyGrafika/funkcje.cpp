#include "funkcje.h"

extern ID2D1HwndRenderTarget* RenderTarget;
extern IWICImagingFactory* WicFactory;
extern ID2D1Factory* Direct2dFactory;
extern IDWriteFactory* DirectWriteFactory;
extern IDWriteTextFormat* FormatTekstu;

void UtworzZasobyNiezalezne() {
	D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &Direct2dFactory);
	CoCreateInstance(CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, IID_IWICImagingFactory, (LPVOID*)&WicFactory);


	static const WCHAR msc_fontName[] = L"Arial";
	static const FLOAT msc_fontSize = 11;

	HRESULT hr = DWriteCreateFactory(
		DWRITE_FACTORY_TYPE_SHARED,
		__uuidof(DirectWriteFactory),
		reinterpret_cast<IUnknown **>(&DirectWriteFactory)
		);

	hr = DirectWriteFactory->CreateTextFormat(
		msc_fontName,
		NULL,
		DWRITE_FONT_WEIGHT_NORMAL,
		DWRITE_FONT_STYLE_NORMAL,
		DWRITE_FONT_STRETCH_NORMAL,
		msc_fontSize,
		L"", //locale
		&FormatTekstu
		);
	FormatTekstu->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);
}

void UtworzZasoby(HWND Okno, UINT Szerokosc, UINT Wysokosc) {
	if (!RenderTarget) {
		D2D1_SIZE_U size = D2D1::SizeU(Szerokosc, Wysokosc);
		Direct2dFactory->CreateHwndRenderTarget(D2D1::RenderTargetProperties(), D2D1::HwndRenderTargetProperties(Okno, size), &RenderTarget);
	}
}