using System.Diagnostics;

namespace Раскараска_графа
{
    internal class СoloringAlgorithms
    {
        private bool[,] AdjMatrix;//Матрица смежности     
        public СoloringAlgorithms(bool[,] AdjMatrix)
        {
            this.AdjMatrix = AdjMatrix;
        }
        private void PrintColoring(int [] colors)
        {
            //Вывод вершин и соответсвующего цвета
            int NumberOfColorsUsed = colors.Max() + 1;
            Console.WriteLine($"Кол-во используемых цветов = {NumberOfColorsUsed}");
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
                Console.WriteLine($"Вершина {i + 1} ---> Цвет {colors[i]}");
        }
        private List<MyType> Vs = new List<MyType>();            
        private void InitializationVs()//Инициализация структуры хранящей номер вершин и веса
        {
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                Vs.Add(new MyType() { Number = i, Degree = 0 });
            }
        }
        public void TrivialGreedy()//Жадный алгоритм
        {
            InitializationVs();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int [] clr = TrivialGreedy(Vs);
            stopwatch.Stop();
            Console.WriteLine("Раскраска жадным алгоритмом");
            Console.WriteLine($"Время работы в Тиках {stopwatch.ElapsedTicks}\t" +
                               $"Время работы в Миллисекундах {stopwatch.ElapsedTicks / 10000}\t" +
                               $"Время работы в Секундах { stopwatch.ElapsedTicks / 10000000} ");
            PrintColoring(clr);
        }
        private int[] TrivialGreedy(List<MyType> Vs)//Жадный алгоритм  для всех других    
        {    
            
            int[] colors = new int[AdjMatrix.GetLength(0)];//Массив цветов
            
            Array.Fill(colors, -1);//Инициализация всех вершин без цвета
            colors[Vs[0].Number] = 0;//Назначение первой вершине первого цвета

            //Временный массив для хранения доступных цветов.
            //Ложное значение available[cr] будет означать,
            //что цвет cr назначен одной из соседних вершин.
            bool[] available = new bool[AdjMatrix.GetLength(0)];

            Array.Fill(available, true);//Все цвета доступны

            //Назначаем цвета оставшимся вершинам V - 1
            int counter = 1;
            int k;
            while (counter < AdjMatrix.GetLength(0))
            {
                k = Vs[counter].Number;
                //Обрабатываем все соседние вершины и помечаем их цвета как недоступные
                for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                    if (AdjMatrix[k, j])
                        if (colors[j] != -1)
                            available[colors[j]] = false;

                //Поиск первого доступного цвета
                int cr;
                for (cr = 0; cr < AdjMatrix.GetLength(0); cr++)
                    if (available[cr]) break;

                colors[k] = cr;//Назначение найденого цвета

                Array.Fill(available, true);
                counter++;
            }           
            return colors;
        }
        public void GreedyOptimized()//Жадный оптимизированны алгоритм(с учётом степеней вершин)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            InitializationVs();
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)//Обход графа с подсчётом степеней 
            {
                for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                {
                    if (AdjMatrix[i, j]) Vs[i].Degree++;
                }
            }          
            Vs = Sort.QuickSort(Vs);//Сортировка структуры по степени вершины
                 
            int[]clr=TrivialGreedy(Vs);
            stopwatch.Stop();
            Console.WriteLine("Раскраска жадным-оптимизированным алгоритмом");
            Console.WriteLine($"Время работы в Тиках {stopwatch.ElapsedTicks}\t" +
                               $"Время работы в Миллисекундах {stopwatch.ElapsedTicks / 10000}\t" +
                               $"Время работы в Секундах { stopwatch.ElapsedTicks / 10000000} ");
            PrintColoring(clr);
        }       
        public void BruteForse()
        {
            Stopwatch stopwatch = new Stopwatch();          
            stopwatch.Start();
            //Список комбинаций - перестановок вершин
            List<List<MyType>> vert_combs = new List<List<MyType>>();
            //Минимальное использлванное кол-во цветов методом жадной раскраски для всех перестановок
            int MinColors = int.MaxValue;
            //Порядок вершин с минимальным кол-вом использованных цветов
            List<MyType> myTypes = new List<MyType>();
            //Инизиализация изначального порядка вершин(по порядку номера)
            InitializationVs();
            //Заполнение списка перестановок вершин
            vert_combs = PermutationExtension.Permutations(Vs);
            //Кол-во цветов для рассматриваемой перестановки
            int NumberOfColorsUsed;
            foreach (var combs in vert_combs)//Перебор всех перестановок для жадного алгоритма и поиск оптимальой
            {
                NumberOfColorsUsed=TrivialGreedy(combs).Max()+1;
                if (NumberOfColorsUsed < MinColors)//Выбор перестановки с минимальным кол-вом цветов
                {
                    MinColors = NumberOfColorsUsed;
                    myTypes = combs;
                }
            }
            int []clr=TrivialGreedy(myTypes);
            stopwatch.Stop();
            Console.WriteLine("Раскраска алгоритмом полного перебора");
            Console.WriteLine($"Время работы в Тиках {stopwatch.ElapsedTicks}\t" +
                              $"Время работы в Миллисекундах {stopwatch.ElapsedTicks / 10000}\t" +
                              $"Время работы в Секундах { stopwatch.ElapsedTicks / 10000000} ");
            PrintColoring(clr);
        }
    }
}
