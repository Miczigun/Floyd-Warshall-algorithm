﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydWarshallAlgorithm
{
    internal class Logic
    {
        private int verticesCount, edgesCount;
        private int[,] graph;

        public String loadGraphFromFile(String file)
        {
            string[] fileContent = File.ReadAllLines(file);

            if (fileContent.Length <= 1)
            {
                return "File does not have any data!";
            }

            string[] counts = fileContent[0].Split(' ');
            verticesCount = int.Parse(counts[0]);
            edgesCount = int.Parse(counts[1]);

            if (verticesCount < 1 && edgesCount < 1)
            {
                return "Edges and Vertices count can not be less than 1!";
            }

            graph = new int[verticesCount, verticesCount];

            //Initialize graph of infinity value
            for(int i=0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    graph[i, j] = int.MaxValue;
                }
                graph[i, i] = 0;
            }

            for(int i=0; i < edgesCount; i++)
            {
                string[] values = fileContent[i+1].Split(' ');
                graph[int.Parse(values[0]), int.Parse(values[1])] = int.Parse(values[2]);
            }
            return "The graph was added correctly";
        }
    }
}