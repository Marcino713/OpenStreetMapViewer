#pragma once
#ifndef makra_h
#define makra_h

#include <Windows.h>

template<class Interface>
inline void SafeRelease(
	Interface **ppInterfaceToRelease
	) {
	if (*ppInterfaceToRelease != NULL) {
		(*ppInterfaceToRelease)->Release();

		(*ppInterfaceToRelease) = NULL;
	}
}

enum typ_grafiki { bmp = 1, png = 2 };

#endif