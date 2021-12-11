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
            Console.WriteLine($"Кол-во используемых цветов = {NumberOfColorsUsed}");
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
                Console.WriteLine($"Вершина {i + 1} ---> Цвет {colors[i]}");
        }
        private List<MyType> Vs = new List<MyType>();
        private List<List<MyType>> vert_combs = new List<List<MyType>>();
        private int NumberOfColorsUsed;      
        private void InitializationVs()
        {
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                Vs.Add(new MyType() { Number = i, Degree = 0 });
            }
        }
        public void TrivialGreedy()
        {
            //TrivialGreedy(Vs);
            PrintColoring(TrivialGreedy(Vs));
        }
        private int[] TrivialGreedy(List<MyType> Vs)//Жадный алгоритм      
        {

            int[] colors = new int[AdjMatrix.GetLength(0)];//Массив цветов
            InitializationVs();
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
            NumberOfColorsUsed = colors.Max() + 1;
            return colors;
        }
        public void GreedyOptimized()
        {
            InitializationVs();
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                {
                    if (AdjMatrix[i, j]) Vs[i].Degree++;
                }
            }
            Vs = Sort.QuickSort(Vs);
            Vs.Reverse();           
            PrintColoring(TrivialGreedy(Vs));
        }
        private void Permute(List<MyType> perm, int i, int n) //O(n!)
        {
            int j;
            if (i == n)
            {
                vert_combs.Add(new List<MyType>(perm));
            }
            else
            {
                for (j = i; i < perm.Count; ++j)
                {
                    Swap(perm, i, j);
                    Permute(perm, i + 1, n);
                    Swap(perm, i, j); //backtrack
                }
            }
        }
        private static void Swap<T>(IList<T> list, int aIndex, int bIndex)
        {
            T value = list[aIndex];
            list[aIndex] = list[bIndex];
            list[bIndex] = value;
        }
        public void BruteForse()
        {
            int MinColors = int.MaxValue;
            List<MyType> myTypes = new List<MyType>();
            foreach (var combs in vert_combs)
            {
                TrivialGreedy(combs);
                if (NumberOfColorsUsed < MinColors)
                {
                    MinColors = NumberOfColorsUsed;
                    myTypes = combs;
                }
            }
            PrintColoring(TrivialGreedy(myTypes));
        }
    }
}
