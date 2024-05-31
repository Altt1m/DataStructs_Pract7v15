using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        // Функція для перетворення матриці суміжності у список суміжності
        static List<int>[] ConvertToAdjList(int[,] matrix)
        {
            int n = matrix.GetLength(0); // Кількість вершин
            List<int>[] adjList = new List<int>[n];

            for (int i = 0; i < n; ++i)
            {
                adjList[i] = new List<int>();
                for (int j = 0; j < n; ++j)
                {
                    if (matrix[i, j] != 0)
                    {
                        adjList[i].Add(j + 1); // Нумерація з 1
                    }
                }
            }

            return adjList;
        }

        // Функція для обходу графа в ширину (BFS)
        static void BFS(List<int>[] adjList, int start)
        {
            bool[] visited = new bool[adjList.Length];
            Queue<int> queue = new Queue<int>();

            visited[start - 1] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int vertex = queue.Dequeue();
                Console.Write(vertex + " ");

                foreach (int neighbor in adjList[vertex - 1])
                {
                    if (!visited[neighbor - 1])
                    {
                        visited[neighbor - 1] = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            Console.WriteLine();
        }

        // Функція для обходу графа в глибину (DFS)
        static void DFS(List<int>[] adjList, int start)
        {
            bool[] visited = new bool[adjList.Length];
            Stack<int> stack = new Stack<int>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                int vertex = stack.Pop();

                if (!visited[vertex - 1])
                {
                    visited[vertex - 1] = true;
                    Console.Write(vertex + " ");

                    foreach (int neighbor in adjList[vertex - 1])
                    {
                        if (!visited[neighbor - 1])
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        // Функція для пошуку найкоротшого шляху між вершинами (алгоритм Дейкстри)
        static void Dijkstra(int[,] matrix, int start, int end)
        {
            int n = matrix.GetLength(0);
            int[] distances = new int[n];
            bool[] visited = new bool[n];
            int[] previous = new int[n];

            for (int i = 0; i < n; ++i)
            {
                distances[i] = int.MaxValue;
                previous[i] = -1;
            }
            distances[start - 1] = 0;

            for (int i = 0; i < n - 1; ++i)
            {
                int u = MinDistance(distances, visited);
                visited[u] = true;

                for (int v = 0; v < n; ++v)
                {
                    if (!visited[v] && matrix[u, v] != 0 && distances[u] != int.MaxValue && distances[u] + matrix[u, v] < distances[v])
                    {
                        distances[v] = distances[u] + matrix[u, v];
                        previous[v] = u;
                    }
                }
            }

            PrintPath(previous, start - 1, end - 1);
            Console.WriteLine($"with distance {distances[end - 1]}");
        }

        static int MinDistance(int[] distances, bool[] visited)
        {
            int min = int.MaxValue, minIndex = -1;

            for (int v = 0; v < distances.Length; ++v)
            {
                if (!visited[v] && distances[v] <= min)
                {
                    min = distances[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        static void PrintPath(int[] previous, int start, int end)
        {
            if (end == start)
            {
                Console.Write((start + 1) + " ");
                return;
            }

            if (previous[end] == -1)
            {
                Console.Write("No path");
                return;
            }

            PrintPath(previous, start, previous[end]);
            Console.Write((end + 1) + " ");
        }

        static void PrintAdjList(List<int>[] adjList)
        {
            for (int i = 0; i < adjList.Length; ++i)
            {
                Console.Write((i + 1) + ": ");
                foreach (int vertex in adjList[i])
                {
                    Console.Write(vertex + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main()
        {
            // Матриця суміжності
            int[,] matrix = {
            { 0, 4, 5, 0, 0, 4 },
            { 4, 0, 0, 0, 0, 0 },
            { 0, 6, 0, 0, 3, 0 },
            { 0, 5, 0, 1, 0, 3 },
            { 0, 0, 1, 0, 0, 0 },
            { 5, 0, 0, 0, 6, 0 }
            };

            List<int>[] adjList = ConvertToAdjList(matrix);
            //PrintAdjList(adjList);

            // Обхід графа в ширину (BFS)
            Console.WriteLine("BFS:");
            BFS(adjList, 1);

            // Обхід графа в глибину (DFS)
            Console.WriteLine("DFS:");
            DFS(adjList, 1);

            // Пошук найкоротшого шляху між вершинами
            Console.WriteLine("Shortest path from 1 to 6:");
            Dijkstra(matrix, 1, 6);
        }
    }


}