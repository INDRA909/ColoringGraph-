using Раскараска_графа;
namespace ColoringGraph
{ 
    class Program
    {
        private static void AlgorithmSelection(bool[,] AdjMatrix)
        {
            int k;//Выбор пользователя
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
                        СoloringAlgorithms alg = new СoloringAlgorithms(AdjMatrix);
                        alg.BruteForse();
                        break;
                    }
                case 1:
                    {
                        СoloringAlgorithms alg = new СoloringAlgorithms(AdjMatrix);
                        alg.TrivialGreedy();
                        break;
                    }
                case 2:
                    {
                        СoloringAlgorithms alg = new СoloringAlgorithms(AdjMatrix);
                        alg.GreedyOptimized();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private static Graph GeneratingGraph(Graph graph,int n)
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
        static void Main(string[] Args)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+ @"\\MyAdjMatrix.txt";//Путь к считываемому файлу на рабочем столе
            int n;//Выбор пользователя - Кол-во вершин графа      
            Graph graph;
            do
            {
                Console.WriteLine("Выберите способ задания матрицы: " +
                                   "\nИз файла - введите '0'" +
                                   "\nСгенерировать - введите 'кол-во вершин'" +
                                   "\nЗакончить работу - введите '-1' ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 0://Считывание матрицв из файла
                        {
                            graph = new Graph(0);
                            graph.AdjMatrix = File.ReadAllLines(filePath).Select(x => x.Split(' ')
                                                                                  .Select(int.Parse).Select(x => (x == 1 ? true : false))
                                                                                  .ToArray()).ToArray().ToRectangularArray();
                            graph.PrintMatrix();//Вывод матрицы
                            AlgorithmSelection(graph.AdjMatrix);//Выбор алгоритма сортировки
                            break;
                        }
                    case -1://Выход
                        {
                            break;
                        }
                    default://Генерация матрицы
                        {
                            graph = new Graph(n);
                            graph = GeneratingGraph(graph, n);//Генерация матрицы
                            graph.PrintMatrix();//Вывод матрицы
                            AlgorithmSelection(graph.AdjMatrix);//Выбор алгоритма сортировки
                            break;
                        }
                }
                Console.WriteLine("\n-------------------------------------------------------------");
            } while (n != -1);
        }
    }
}

