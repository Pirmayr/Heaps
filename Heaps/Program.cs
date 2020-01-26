using System;
using System.Diagnostics;

// ReSharper disable TooWideLocalVariableScope

namespace Heaps
{
    public static class Example
    {
        public static void Main()
        {
            DumpBinaryTree(0, 0, 4);
            return;
            /*
            const int Length = 10000;
            const int Repetitions = 1; // 100000;
            long comparisonsSum1 = 0;
            long swapsSum1 = 0;
            long comparisonsSum2 = 0;
            long swapsSum2 = 0;
            int[] array = new int[Length];
            for (int currentRepetition = 0; currentRepetition < Repetitions; ++currentRepetition)
            {
                int seed = new Random().Next(array.Length * 4);
                PerformTest(array, seed, 1, Repetitions);
                comparisonsSum1 += Heaps.comparisons;
                swapsSum1 += Heaps.swaps;
                PerformTest(array, seed, 2, Repetitions);
                comparisonsSum2 += Heaps.comparisons;
                swapsSum2 += Heaps.swaps;
            }
            WriteLine($"Average Swaps: {swapsSum1 / Repetitions} Average Comparisons: {comparisonsSum1 / Repetitions}");
            WriteLine($"Average Swaps: {swapsSum2 / Repetitions} Average Comparisons: {comparisonsSum2 / Repetitions}");
            */
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

        private static void DumpBinaryTree(int startNumber, int currentLevel, int maximalLevel)
        {
            if (currentLevel <= maximalLevel)
            {
                if (startNumber == 0)
                {
                    WriteLine(0.ToString());
                    Write(new string('-', currentLevel + 1));
                    WriteLine(1.ToString());
                    DumpBinaryTree(1, currentLevel + 1, maximalLevel);
                }
                else
                {
                    Write(new string('-', currentLevel + 1));
                    WriteLine((startNumber + 1).ToString());
                    DumpBinaryTree(3 * startNumber + 1, currentLevel + 1, maximalLevel);
                    ++startNumber;
                    Write(new string('-', currentLevel + 1));
                    WriteLine((startNumber + 1).ToString());
                    DumpBinaryTree(3 * startNumber + 1, currentLevel + 1, maximalLevel);
                    ++startNumber;
                    Write(new string('-', currentLevel + 1));
                    WriteLine((startNumber + 1).ToString());
                    DumpBinaryTree(3 * startNumber + 1, currentLevel + 1, maximalLevel);

                }
            }
        }

        private static void InitializeArray(int[] array, int seed)
        {
            Random random = new Random(seed);
            int length = array.Length;
            for (int i = 0; i < length; ++i)
            {
                array[i] = random.Next(length * 16);
            }
        }

        private static void PerformTest(int[] array, int seed, int testIndex, int repetitions)
        {
            InitializeArray(array, seed);
            switch (testIndex)
            {
            case 1:
                Heaps.MaxifyStandard(array);
                break;
            case 2:
                Heaps.MaxifyWeak(array);
                break;
            }
            if (repetitions == 1)
            {
                Dump(array);
                WriteLine($"Maximum: {Heaps.maximum} Seed: {seed} Swaps: {Heaps.swaps} Comparisons: {Heaps.comparisons}");
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