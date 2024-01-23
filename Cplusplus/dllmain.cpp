// dllmain.cpp : Definiuje punkt wejścia dla aplikacji DLL.
#include "pch.h"
#include <climits>

constexpr int S_MAX = 16000;

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

extern "C" __declspec(dllexport) void __stdcall cppFloydWarshall(int* resultGraph, int n, int iteration) {

    int ik, kj, ij;

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                ik = resultGraph[i * n + iteration];
                kj = resultGraph[iteration * n + j];
                ij = resultGraph[i * n + j];

                if (ik != S_MAX && kj != S_MAX && ((ik + kj) < ij))
                {
                    resultGraph[i * n + j] = ik + kj;
                }
            }
        }
    
}