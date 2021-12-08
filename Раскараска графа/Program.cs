using System;
namespace ColoringGraph
{
    public class Graph
    {
        public bool[,] AdjMatrix;
        private int NumVertices;

        public Graph(int NumVerticies)
        {

            NumVertices = NumVerticies;
            AdjMatrix = new bool[NumVertices, NumVertices];
        }
        public void AddEdge(int i, int j)
        {
            AdjMatrix[i, j] = true;
            AdjMatrix[j, i] = true;
        }
        public void PrintMatrix()
        {
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                Console.Write($"{i}: ");
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
        public static T[,] ToRectangularArray<T>(this IReadOnlyList<T[]> arrays)
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
        static void Main(string[] Args)
        {
            int s,x;
            Random rnd = new Random(DateTime.Now.Millisecond);
            Graph graph;
            Console.WriteLine("Если вы хотите считать матрицу смежности из файла введите - '0', сгенерировать - 'кол-во вершин'");
            s = Convert.ToInt32(Console.ReadLine());
            switch (s)
            {
                case 0:
                    {                        
                        graph = new Graph(0);
                        graph.AdjMatrix = File.ReadAllLines("MyAdjMatrix.txt").Select(x => x.Split(' ')
                                                                              .Select(int.Parse).Select(x => (x == 1 ? true : false))
                                                                              .ToArray()).ToArray().ToRectangularArray();
                        graph.PrintMatrix();
                        break; 
                    }
                default:
                    {
                        graph = new Graph(s);
                        for (int i = 0; i < s; i++)
                            for (int j = 0; j < s; j++)
                            {
                                x = rnd.Next(0, 2);
                                if (x == 1) graph.AddEdge(i, j);
                            }
                        graph.PrintMatrix();       
                        break;
                    }

            }
        }
    }
}

