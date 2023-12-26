// dllmain.cpp : Definiuje punkt wejścia dla aplikacji DLL.
#include "pch.h"
#include <climits>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" __declspec(dllexport) void __stdcall cppFloydWarshall(int* resultGraph, int rAndC) {

    int ik, kj, ij;

    for (int k = 0; k < rAndC; ++k)
    {
        for (int i = 0; i < rAndC; ++i)
        {
            for (int j = 0; j < rAndC; ++j)
            {
                ik = i * rAndC + k;
                kj = k * rAndC + j;
                ij = i * rAndC + j;

                if (resultGraph[ik] != INT_MAX && resultGraph[kj] != INT_MAX &&
                    ( resultGraph[ik] + resultGraph[kj] ) < resultGraph[ij])
                {
                    resultGraph[ij] = resultGraph[ik] + resultGraph[kj];
                }
            }
        }
    }
}