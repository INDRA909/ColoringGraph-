using System;
using Раскараска_графа;
namespace ColoringGraph
{
    public class Graph
    {
        public bool[,] AdjMatrix;//Матрица смежности
        private int NumVertices;//Кол-во вершин
        public Graph(int NumVerticies)
        {

            NumVertices = NumVerticies;
            AdjMatrix = new bool[NumVertices, NumVertices];
        }
        public void AddEdge(int i, int j)//Добавление ребра
        {
            AdjMatrix[i, j] = true;
            AdjMatrix[j, i] = true;
        }
        public void PrintMatrix()//Вывод матрицы
        {
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                Console.Write(i > 9 ? $"{i}:   " : $" {i}:   ");
                for (int j = 0; j < AdjMatrix.GetLength(1); j++)
                {
                    Console.Write($"{(AdjMatrix[i, j] ? 1 : 0)} ");
                }
                Console.WriteLine();
            }
        }
    }
    public static class Extensions
    {
        public static T[,] ToRectangularArray<T>(this IReadOnlyList<T[]> arrays)//Метод расширения для удобного считывания из файла
        {
            var ret = new T[arrays.Count, arrays[0].Length];
            for (var i = 0; i < arrays.Count; i++)
                for (var j = 0; j < arrays[0].Length; j++)
                    ret[i, j] = arrays[i][j];
            return ret;
        }
    }
    class Program
    {
        private static void AlgorithmSelection(bool[,] AdjMatrix)
        {
            int k;
            Console.WriteLine("Выберите алгоритм раскраски: " +
                               "\nПолный перебор - введите '0'" +
                               "\nЖадный - введите '1'" +
                               "\nЖадный оптимизированный - введите '2'" +
                               "\nЗакончить - введите -1");
            k = Convert.ToInt32(Console.ReadLine());
            switch (k)
            {
                case 0:
                    {

                        break;
                    }
                case 1:
                    {
                        TrivialGreedy coloring = new TrivialGreedy(AdjMatrix);
                        coloring.Coloring();
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        static void Main(string[] Args)
        {
            int s, x;
            Random rnd = new Random(DateTime.Now.Millisecond);
            Graph graph;
            Console.WriteLine("Выберите способ задания матрицы: " +
                               "\nИз файла - введите '0'" +
                               "\n Сгенерировать - введите 'кол-во вершин'" +
                               "\n Закончить работу - введите -1");
            s = Convert.ToInt32(Console.ReadLine());
            switch (s)
            {
                case 0://Считывание матрицв из файла
                    {
                        graph = new Graph(0);
                        graph.AdjMatrix = File.ReadAllLines("MyAdjMatrix.txt").Select(x => x.Split(' ')
                                                                              .Select(int.Parse).Select(x => (x == 1 ? true : false))
                                                                              .ToArray()).ToArray().ToRectangularArray();
                        graph.PrintMatrix();
                        AlgorithmSelection(graph.AdjMatrix);
                        break;
                    }
                case -1://Выход
                    {
                        break;
                    }
                default://Генерация матрицы
                    {
                        graph = new Graph(s);
                        for (int i = 0; i < s; i++)
                            for (int j = i; j < s; j++)
                            {
                                x = rnd.Next(0, 2);
                                if (x == 1)
                                {
                                    graph.AddEdge(i, j);
                                    graph.AddEdge(j, i);
                                }
                            }
                        graph.PrintMatrix();
                        AlgorithmSelection(graph.AdjMatrix);
                        break;
                    }

            }
        }
    }
}

