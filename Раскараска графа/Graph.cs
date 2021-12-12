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
        public void AddEdge(int i, int j)//Добавление ребра
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
    }
}
