# Floyd Warshall Algorithm Benchmarking

This project aims to implement the Floyd Warshall algorithm in both C++ and assembly language (ASM), comparing the performance between these two implementations. Additionally, the project provides a C# interface for user interaction and visualization.

## Introduction

The Floyd Warshall algorithm is a fundamental graph algorithm used to find the shortest paths between all pairs of vertices in a weighted graph. This project explores the efficiency of implementing this algorithm in both C++ and assembly language, offering insights into the performance differences between these languages.

Furthermore, the project provides a user-friendly interface built in C# to interact with the implemented algorithms, allowing users to input graphs, execute the algorithms, and visualize the results.

## Features

- Implementation of Floyd Warshall algorithm in C++ and assembly language (ASM).
- User interface implemented in C# for ease of interaction.
- Benchmarking module to compare the performance of C++ and ASM implementations.

## Requirements

- C++ compiler compatible with C++17 standards.
- Assembler compatible with x86 or x86-64 architecture (MASM).
- .NET Framework or .NET Core for running the C# interface.(4.8.0)
## GUI

![image](https://github.com/Miczigun/Floyd-Warshall-algorithm/assets/92275832/b2b7f2b3-1f82-4c8e-bf97-34a0af0a9097)

## Usage

1. Run the C# interface application by executing the provided executable or by compiling and running the source files.

2. Import graphfile (you can take my graphs or generate using graphgenerator.py, vertices have to be % 8 == 0)
   
3. Use the interface to input graphs and execute the Floyd Warshall algorithm in either C++ or ASM.

4. Result is writing to the file output.txt

## Benchmarking

The benchmarking module allows users to compare the performance of the C++ and ASM implementations of the Floyd Warshall algorithm. The module records execution times for various graph sizes and provides insights into the efficiency of each implementation.

To perform benchmarking:

1. Generate random graphs of different sizes.
2. Execute the Floyd Warshall algorithm on each graph using both C++ and ASM implementations.
3. Record and analyze the execution times for comparison.

Times Comparison:

![image](https://github.com/Miczigun/Floyd-Warshall-algorithm/assets/92275832/d1013a92-f44f-4f20-868f-6e715b91b4ad)


