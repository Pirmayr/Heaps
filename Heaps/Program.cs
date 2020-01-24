using System;
using System.Diagnostics;

// ReSharper disable TooWideLocalVariableScope

namespace Heaps
{
    public static class Example
    {
        private const int Length = 1000000;
        private const int Repetitions = 1; // 100000;

        public static void Main()
        {
            int[] array = new int[Length];
            for (int currentRepetition = 0; currentRepetition < Repetitions; ++currentRepetition)
            {
                int seed = new Random().Next(array.Length * 4);
                WriteLine($"Seed: {seed}");
                InitializeArray(array, seed);
                WriteLine($"{HeapMedian.Maximum1(array)}");
                Dump(array);
                WriteLine($"Swaps: {HeapMedian.swapCount}");
                WriteLine($"Comparisons: {HeapMedian.comparisonCount}");
                InitializeArray(array, seed);
                WriteLine($"{HeapMedian.Maximum2(array)}");
                Dump(array);
                WriteLine($"Swaps: {HeapMedian.swapCount}");
                WriteLine($"Comparisons: {HeapMedian.comparisonCount}");
            }
        }

        private static void Dump(int[] array)
        {
            if (array.Length <= 100)
            {
                foreach (int currentElement in array)
                {
                    Write(currentElement + " ");
                }
                WriteLine("");
            }
        }

        private static void InitializeArray(int[] array, int seed)
        {
            Random random = new Random(seed);
            int length = array.Length;
            for (int i = 0; i < length; ++i)
            {
                array[i] = random.Next(length * 2);
            }
        }

        private static void Write(string formatString, params object[] values)
        {
            Console.Write(formatString, values);
            Debug.Write(string.Format(formatString, values));
        }

        private static void WriteLine(string formatString, params object[] values)
        {
            Console.WriteLine(formatString, values);
            Debug.WriteLine(formatString, values);
        }
    }
}