namespace Раскараска_графа
{
    internal static class PermutationExtension
    {
        public static List<List<MyType>> Permutations(List<MyType> source)
        {
            var results = new List<List<MyType>>();
            Permute(source, 0, source.Count - 1, results);
            return results;
        }

        private static void Swap<T>(IList<T> list, int aIndex, int bIndex)
        {
            T value = list[aIndex];
            list[aIndex] = list[bIndex];
            list[bIndex] = value;
        }

        private static void Permute(List<MyType> elements, int recursionDepth, int maxDepth, List<List<MyType>> results)
        {

            if (recursionDepth == maxDepth)
            {
                results.Add(new List<MyType>(elements));
                return;
            }

            for (var i = recursionDepth; i <= maxDepth; i++)
            {
                Swap(elements,recursionDepth,i);
                Permute(elements, recursionDepth + 1, maxDepth, results);
                Swap(elements,recursionDepth, i);
            }
        }
    }
}
