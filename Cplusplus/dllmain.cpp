// dllmain.cpp : Definiuje punkt wejścia dla aplikacji DLL.
#include "pch.h"
#include <climits>
constexpr short S_MAX = 16000;
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

extern "C" __declspec(dllexport) void __stdcall cppFloydWarshall(short* resultGraph, short rAndC, short iteration) {

    short ik, kj, ij;

        for (int i = 0; i < rAndC; ++i)
        {
            for (int j = 0; j < rAndC; ++j)
            {
                ik = i * rAndC + iteration;
                kj = iteration * rAndC + j;
                ij = i * rAndC + j;

                if (resultGraph[ik] != S_MAX && resultGraph[kj] != S_MAX &&
                    ( resultGraph[ik] + resultGraph[kj] ) < resultGraph[ij])
                {
                    resultGraph[ij] = resultGraph[ik] + resultGraph[kj];
                }
            }
        }
    
}