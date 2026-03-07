namespace MergeArrayApplication
{
    internal class Program
    {
        public static int[] mergeArrays(int[] arrayA, int[] arrayB)
        {
            int minLength = Math.Min(arrayA.Length, arrayB.Length);

            int totalSize =
                minLength +
                (arrayA.Length - minLength) +
                (arrayB.Length - minLength);

            int[] mergedArray = new int[totalSize];
            int index = 0;

            // Common indices
            for (int i = 0; i < minLength; i++)
            {
                if (i % 2 == 0)
                {
                    mergedArray[index++] = Math.Max(arrayA[i], arrayB[i]);
                }
                else
                {
                    mergedArray[index++] = (arrayA[i] + arrayB[i]) / 2;
                }
            }

            // Remaining of arrayA
            for (int i = minLength; i < arrayA.Length; i++)
            {
                mergedArray[index++] = arrayA[i];
            }

            // Remaining of arrayB
            for (int i = minLength; i < arrayB.Length; i++)
            {
                mergedArray[index++] = arrayB[i];
            }

            return mergedArray;
        }



        static void Main(string[] args)
        {
            int[] arrayA = { 1, 3, 5, 7 };
            int[] arrayB = { 2, 4, 6, 8, 10, 12 };

            int[] result = mergeArrays(arrayA, arrayB);

            Console.WriteLine("Merging two arrays:");
            foreach (int value in result)
            {
                Console.Write(value + " ");
            }
        }

    }
}
