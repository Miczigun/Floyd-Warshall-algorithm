// dllmain.cpp : Definiuje punkt wejścia dla aplikacji DLL.
#include "pch.h"
#include <vector>

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

extern "C" __declspec(dllexport) void __stdcall cppFloydWarshall(int* graph, int* result, int rows, int columns) {
    
    // Copy graph to result
    for (int i = 0; i < rows * columns; ++i)
    {
        result[i] = graph[i];
    }

    // floyd warshall algorithm
    for (int x = 0; x < rows; ++x)
    {
        for (int y = 0; y < rows; ++y)
        {
            for (int z = 0; z < columns; ++z)
            {
                int yx = y * columns + x;
                int xz = x * columns + z;
                int yz = y * columns + z;

                if (result[yx] != INT_MAX && result[xz] != INT_MAX &&
                    result[yx] + result[xz] < result[yz])
                {
                    result[yz] = result[yx] + result[xz];
                }
            }
        }
    }
}