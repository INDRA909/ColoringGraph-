namespace Раскараска_графа
{
    internal class Graph
    {
        public bool[,] AdjMatrix;//Матрица смежности
        private int NumVertices;//Кол-во вершин
        public Graph(int NumVerticies)
        {

            NumVertices = NumVerticies;
            AdjMatrix = new bool[NumVertices, NumVertices];
        }
        private void AddEdge(int i, int j)//Добавление ребра
        {
            AdjMatrix[i, j] = true;
            AdjMatrix[j, i] = true;
        }
        public void PrintMatrix()//Вывод матрицы
        {
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                Console.Write(i >= 9 ? $"{i + 1}:   " : $" {i + 1}:   ");
                for (int j = 0; j < AdjMatrix.GetLength(1); j++)
                {
                    Console.Write($"{(AdjMatrix[i, j] ? 1 : 0)} ");
                }
                Console.WriteLine();
            }
        }
        public Graph RandomGeneratingGraph(Graph graph, int n)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int x;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    x = rnd.Next(0, 2);
                    if (x == 1)
                    {
                        graph.AddEdge(i, j);
                        graph.AddEdge(j, i);
                    }
                }
            return graph;
        }
        public static Graph BadGeneratingGraph(Graph graph, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    graph.AddEdge(i, j);
                    graph.AddEdge(j, i);
                }
            return graph;
        }
    }
}
