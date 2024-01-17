using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FloydWarshallAlgorithm
{
    internal class Graph
    {
        private short verticesCount, edgesCount;
        private short[,] graph;
        private short[] resultGraph;

        public String loadGraphFromFile(String file)
        {
            string[] fileContent = File.ReadAllLines(file);

            if (fileContent.Length <= 1)
            {
                return "File does not have any data!";
            }

            string[] counts = fileContent[0].Split(' ');
            verticesCount = short.Parse(counts[0]);
            edgesCount = short.Parse(counts[1]);

            if (verticesCount < 1 && edgesCount < 1)
            {
                return "Edges and Vertices count can not be less than 1!";
            }

            graph = new short[verticesCount, verticesCount];

            //Initialize graph of infinity value
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    graph[i, j] = 16000;
                }
                graph[i, i] = 0;
            }

            for (int i = 0; i < edgesCount; i++)
            {
                string[] values = fileContent[i+1].Split(' ');
                graph[int.Parse(values[0]) - 1, int.Parse(values[1]) - 1] = short.Parse(values[2]);
            }
            return "The graph was added correctly";
        }

        public void saveToFile()
        {
            short[,] outputGraph = new short[verticesCount, verticesCount];
            Buffer.BlockCopy(resultGraph, 0, outputGraph, 0, sizeof(short)* verticesCount * verticesCount);

            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < verticesCount; i++)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (outputGraph[i, j] == 16000)
                        {
                            writer.WriteLine($"{i + 1} -> {j + 1}: Not connected");
                        }
                        else
                        {
                            writer.WriteLine($"{i + 1} -> {j + 1}: {outputGraph[i, j]}");
                        }
                    }
                }
            }
        }

        public void graphToFlat()
        {
            resultGraph = new short[verticesCount * verticesCount];
            Buffer.BlockCopy(graph,0,resultGraph,0,sizeof(short) * verticesCount * verticesCount);
        }

        [DllImport(@"C:\Users\micha\Desktop\studia\sem5\JA\Floyd-Warshall-algorithm\FloydWarshallAlgorithm\x64\Debug\Cplusplus.dll")]
        static extern void cppFloydWarshall(short[] graph, short rowsAndColumns, short iteration);
        [DllImport(@"C:\Users\micha\Desktop\studia\sem5\JA\Floyd-Warshall-algorithm\FloydWarshallAlgorithm\x64\Debug\Asm.dll")]
        static extern void asmFloydWarshall(short[] graph, short rowsAndColumns, short iteration);

        public double invokeCppMethod(int threadsNo)
        {
            graphToFlat();
            short start = 0;
            if (threadsNo > verticesCount) { threadsNo = verticesCount; }
            Task[] tasks = new Task[threadsNo+1];

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (short i = 0; i < threadsNo; ++i)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    cppFloydWarshall(resultGraph, verticesCount, i);
                });
                start += 1;
            }

            if ( (start+1) != verticesCount)
            {
                tasks[threadsNo] = Task.Factory.StartNew(() =>
                {
                    for (short i = start; i < verticesCount; ++i)
                    {
                        cppFloydWarshall(resultGraph, verticesCount, i);
                    }
                });
            }

            // Wait for all threads to finish
            Task.WaitAll(tasks);

            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds * 1000;
        }

        public double invokeAsmMethod(int theardsNo)
        {
            graphToFlat();

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (short i = 0; i < verticesCount; ++i)
            {
                asmFloydWarshall(resultGraph, verticesCount, i);
            }
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}
