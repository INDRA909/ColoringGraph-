using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Раскараска_графа
{
    internal class Sort
    {
        private static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        //метод возвращающий индекс опорного элемента
        private static int Partition(List<MyType> array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].Degree < array[maxIndex].Degree)
                {
                    pivot++;
                    Swap(ref array[pivot].Degree, ref array[i].Degree);
                }
            }

            pivot++;
            Swap(ref array[pivot].Degree, ref array[maxIndex].Degree);
            return pivot;
        }

        //быстрая сортировка
        private static List<MyType> QuickSort(List<MyType> array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }
        public static List<MyType> QuickSort(List<MyType> array)
        {
            return QuickSort(array, 0, array.Count - 1);
        }       
    }
}
