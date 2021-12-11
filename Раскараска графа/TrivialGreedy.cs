namespace Раскараска_графа
{
    internal class TrivialGreedy
    {
        private bool[,] AdjMatrix;//Матрица смежности
        public TrivialGreedy(bool[,] AdjMatrix)
        {
            this.AdjMatrix = AdjMatrix;
        }
        public void Coloring()//Раскраска Графа
        {
          int[] colors = new int[AdjMatrix.GetLength(0)]; //Массив цветов
          Array.Fill(colors, -1);//Инициализация всех вершин без цвета
          colors[0] = 0;//Назначение первой вершине первого цвета

          //Временный массив для хранения доступных цветов.
          //Ложное значение available[cr] будет означать,
          //что цвет cr назначен одной из соседних вершин.
          bool[] available = new bool[AdjMatrix.GetLength(0)];

          Array.Fill(available, true);//Все цвета доступны

          //Назначьте цвета оставшимся вершинам V - 1
          for (int i = 1; i < AdjMatrix.GetLength(0); i++)
          {
              //Обрабатываем все соседние вершины и помечаем их цвета как недоступные
              for (int j = 0; j < AdjMatrix.GetLength(0); j++)
                  if (colors[AdjMatrix[i, j] ? 1 : 0] != -1)
                      available[colors[AdjMatrix[i, j] ? 1 : 0]] = false;

              //Поиск первого доступного цвета
              int cr;
              for (cr = 0; cr < AdjMatrix.GetLength(0); cr++)
                  if (available[cr]) break;

              colors[i] = cr;//Назначение найденого цвета
              Array.Fill(available, true);// сброс доступных цветов для следущей вершины               
          }
          //Вывод вершин и соответсвующего цвета
          for(int i = 0;i < AdjMatrix.GetLength(0); i++)
                Console.WriteLine($"Вершина {i} ---> Цвет {colors[i]}");
        }
    }
}
