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
        private int verticesCount, edgesCount;
        private int[,] graph;
        private int[] resultGraph;

        public String loadGraphFromFile(String file)
        {
            string[] fileContent = File.ReadAllLines(file);

            if (fileContent.Length <= 1)
            {
                return "File does not have any data!";
            }

            string[] counts = fileContent[0].Split(' ');
            verticesCount = int.Parse(counts[0]);
            if (verticesCount % 8 != 0)
            {
                return "Vertices count must be a multiple of 16";
            }
            edgesCount = int.Parse(counts[1]);

            if (verticesCount < 1 && edgesCount < 1)
            {
                return "Edges and Vertices count can not be less than 1!";
            }

            graph = new int[verticesCount, verticesCount];

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
                graph[int.Parse(values[0]) - 1, int.Parse(values[1]) - 1] = int.Parse(values[2]);
            }
            resultGraph = new int[verticesCount * verticesCount];
            return "The graph was added correctly";
        }

        public void saveToFile()
        {
            int[,] outputGraph = new int[verticesCount, verticesCount];
            Buffer.BlockCopy(resultGraph, 0, outputGraph, 0, sizeof(int)* verticesCount * verticesCount);

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
            Buffer.BlockCopy(graph,0,resultGraph,0,sizeof(int) * verticesCount * verticesCount);
        }

        [DllImport(@"C:\Users\micha\Desktop\studia\sem5\JA\Floyd-Warshall-algorithm\FloydWarshallAlgorithm\x64\Debug\Cplusplus.dll")]
        static extern void cppFloydWarshall(int[] graph, int rowsAndColumns, int iteration);
        [DllImport(@"C:\Users\micha\Desktop\studia\sem5\JA\Floyd-Warshall-algorithm\FloydWarshallAlgorithm\x64\Debug\Asm.dll")]
        static extern void asmFloydWarshall(int[] graph, int rowsAndColumns, int iteration);

        public double invokeCppMethod(int threadsNo)
        {
            graphToFlat();

            int[] copiedGraph = new int[verticesCount * verticesCount];
            Buffer.BlockCopy(resultGraph, 0, copiedGraph, 0, sizeof(int) * verticesCount * verticesCount);

            int start = 0;
            int increment = (int)(verticesCount / threadsNo);
            int end = increment;
            if (threadsNo > verticesCount) { threadsNo = verticesCount; }

            //Task[] tasks = new Task[threadsNo];
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int j = start; j < end; j++)
            {
                cppFloydWarshall(copiedGraph, verticesCount, j);
            }

            // Update the original resultGraph with the modified copiedGraph            
            stopwatch.Stop();
            Buffer.BlockCopy(copiedGraph, 0, resultGraph, 0, sizeof(int) * verticesCount * verticesCount);

            return stopwatch.Elapsed.TotalMilliseconds;
        }


        public double invokeAsmMethod(int threadsNo)
        {
            graphToFlat();

            int[] copiedGraph = new int[verticesCount * verticesCount];
            Buffer.BlockCopy(resultGraph, 0, copiedGraph, 0, sizeof(int) * verticesCount * verticesCount);

            if (threadsNo > verticesCount) { threadsNo = verticesCount; }

            int start = 0;
            int increment = (int)(verticesCount / threadsNo);
            int end = increment;

            //Task[] tasks = new Task[threadsNo];
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int j = start; j < end; j++)
            {
                asmFloydWarshall(copiedGraph, verticesCount, j);
            }         

            stopwatch.Stop();
            Buffer.BlockCopy(copiedGraph, 0, resultGraph, 0, sizeof(int) * verticesCount * verticesCount);

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public bool checkGraph()
        {
            if (graph == null)
            {
                return true;
            }
            return false;
        }
    }
}
