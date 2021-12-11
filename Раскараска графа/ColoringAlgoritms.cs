namespace Раскараска_графа
{
    internal class СoloringAlgorithms
    {
        private bool[,] AdjMatrix;//Матрица смежности
        
        public СoloringAlgorithms(bool[,] AdjMatrix)
        {
            this.AdjMatrix = AdjMatrix;
        }
        private void PrintColoring(int[] colors)
        {
            //Вывод вершин и соответсвующего цвета
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
                Console.WriteLine($"Вершина {i+1} ---> Цвет {colors[i]}");
        }
        public void TrivialGreedy(bool[,] AdjMatrix)//Жадный алгоритм      
        {
            int[] colors = new int[AdjMatrix.GetLength(0)]; //Массив цветов
            Array.Fill(colors, -1);//Инициализация всех вершин без цвета
            colors[0] = 0;//Назначение первой вершине первого цвета

            //Временный массив для хранения доступных цветов.
            //Ложное значение available[cr] будет означать,
            //что цвет cr назначен одной из соседних вершин.
            bool[] available = new bool[AdjMatrix.GetLength(0)];

            Array.Fill(available, true);//Все цвета доступны

            //Назначаем цвета оставшимся вершинам V - 1
            for (int i = 1; i < AdjMatrix.GetLength(0); i++)
            {
                //Обрабатываем все соседние вершины и помечаем их цвета как недоступные
                for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                    if (AdjMatrix[i, j])
                        if (colors[j] != -1)
                        available[colors[j]] = false;
              
                //Поиск первого доступного цвета
                int cr;
                for (cr = 0; cr < AdjMatrix.GetLength(0); cr++)
                    if (available[cr]) break;

                colors[i] = cr;//Назначение найденого цвета

                //for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                //    if (AdjMatrix[i, j])
                //        if (colors[j] != -1)
                //        available[colors[j] = true;
                Array.Fill(available, true);
            }
           PrintColoring(colors);
        }
        public void GreedyOptimized()
        {
            bool[,] SortAdjMatrix = AdjMatrix;
            List<MyType>  vs = new List<MyType>();
            for(int i=0; i<SortAdjMatrix.GetLength(0); i++)
            { 
                vs.Add(new MyType() { Number = i, Degree = 0 });
            }           
            for (int i = 0; i < SortAdjMatrix.GetLength(0);i++)
            {
                for(int j = 0;j< SortAdjMatrix.GetLength(0);j++)
                {
                    if (SortAdjMatrix[i, j]) vs[i].Degree++; 
                }
            }
            Sort.QuickSort(vs);
            for (int i = 0; i < vs.Count; i++)
            {
                Console.WriteLine($"вершина {vs[i].Number+1}  сепень { vs[i].Degree}");
            }
            
            //TrivialGreedy(SortAdjMatrix);
        }
        public void BruteForse()
        {

        }
    }
}
