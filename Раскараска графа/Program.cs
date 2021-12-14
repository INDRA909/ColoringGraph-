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
                        alg.BruteForce();
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
        private static void Testing()
        {
            int NumColorBruteForse;
            int NumColorTrivialGreedy;
            int NumColorOptimizetGreedy;
            int ErrorTrivialGreedy = 0;
            int ErrorOptimizetGreedy = 0;
            double OtklTrivial = 0;
            double OtklOptimized = 0;
            int n, m;//Выбор пользователя - Кол-во вершин графа      
            Console.Write("Введите сколько кол-во тестов - ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите размерность матрицы - ");
            m = Convert.ToInt32(Console.ReadLine());
            Graph graph;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Номер Итерации {i}");
                graph= new Graph(m);
                СoloringAlgorithms alg = new СoloringAlgorithms(graph.RandomGeneratingGraph(graph, m).AdjMatrix);
                NumColorBruteForse = alg.BruteForceNoOut().Max() + 1;
                NumColorOptimizetGreedy = alg.TrivialGreedyNoOut().Max() + 1;
                NumColorTrivialGreedy = alg.GreedyOptimizedNoOut().Max() + 1;
                Console.WriteLine($"Кол-во используемых цветов:" +
                    $"\nПолный перебор = {NumColorBruteForse} Жадный = {NumColorTrivialGreedy} Жадный оптимизированный = {NumColorOptimizetGreedy}");
                if (NumColorOptimizetGreedy != NumColorBruteForse)
                {
                    ErrorOptimizetGreedy++;
                    OtklOptimized += (double)(Math.Abs(NumColorOptimizetGreedy - NumColorBruteForse) / (double)NumColorBruteForse);
                }
                if (NumColorTrivialGreedy != NumColorBruteForse)
                {
                    ErrorTrivialGreedy++;
                    OtklTrivial += (double)(Math.Abs(NumColorTrivialGreedy - NumColorBruteForse) / (double)NumColorBruteForse);
                }

                Console.WriteLine("\n-------------------------------------------------------------------------");
            }
            Console.WriteLine($"Ошибок для тривиального {ErrorTrivialGreedy} Cреднее отклонение {OtklTrivial / 100}" +
                       $" Ошибок для оптимизмрованного {ErrorOptimizetGreedy} Cреднее отклонение {OtklOptimized / 100} ");
        }
        static void Main(string[] Args)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\\MyAdjMatrix.txt";//Путь к считываемому файлу на рабочем столе
            int n;//Выбор пользователя - Кол-во вершин графа      
            Graph graph;
            do
            {
                Console.WriteLine("Выберите пункт и введите соответсвующее значение: " +
                                   "\nИз файла - введите '0' (на рабочем столе должен быть файл 'MyAdjMatrix.txt')" +
                                   "\nСгенерировать - введите необходимо кол-во вершин 'n'" +
                                   "\nТестирование - введите '-2'" +
                                   "\nЗакончить работу - введите '-1'");
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
                    case -2://Тестирование
                        {
                            Testing();
                            break;
                        }
                    default://Генерация матрицы
                        {
                            graph = new Graph(n);
                            graph = graph.RandomGeneratingGraph(graph, n);//Генерация матрицы
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

