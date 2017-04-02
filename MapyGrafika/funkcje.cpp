#include "funkcje.h"

extern ID2D1HwndRenderTarget* RenderTarget;
extern IWICImagingFactory* WicFactory;
extern ID2D1Factory* Direct2dFactory;

HRESULT UtworzZasobyNiezalezne() {
	HRESULT hr = S_OK;

	hr = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &Direct2dFactory);
	CoCreateInstance(CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, IID_IWICImagingFactory, (LPVOID*)&WicFactory);

	return hr;
}

HRESULT UtworzZasoby(HWND Okno, UINT Szerokosc, UINT Wysokosc) {
	HRESULT hr = S_OK;

	if (!RenderTarget) {
		D2D1_SIZE_U size = D2D1::SizeU(Szerokosc, Wysokosc);
		hr = Direct2dFactory->CreateHwndRenderTarget(D2D1::RenderTargetProperties(), D2D1::HwndRenderTargetProperties(Okno, size), &RenderTarget);
	}

	return hr;
}