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
            Stopwatch heapMedianStopWatch = new Stopwatch();
            Stopwatch quickSelectStopWatch = new Stopwatch();
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
                /*
                arrayClone = (int[]) array.Clone();
                heapMedianStopWatch.Start();
                int heapMedianMedian = Heaps.GetMedian(arrayClone);
                heapMedianStopWatch.Stop();
                DumpResult(arrayClone, Repetitions, heapMedianMedian, Heaps.SwapCount);
                arrayClone = (int[]) array.Clone();
                quickSelectStopWatch.Start();
                int quickSelectMedian = arrayClone[QuickSelect.GetMedianIndex(arrayClone, 0, arrayClone.Length - 1, arrayClone.Length / 2)];
                quickSelectStopWatch.Stop();
                DumpResult(array, Repetitions, quickSelectMedian, QuickSelect.SwapCount);
                */
            }
            /*
            WriteLine($"Average time (Heaps): {heapMedianStopWatch.ElapsedMilliseconds / 1000.0 / Repetitions}");
            WriteLine($"Comparison-factor (Heaps): {(double) Heaps.ComparisonCount / Length / Repetitions}");
            // WriteLine($"No swap count (Heaps): {Heaps.noSwapCount}");
            WriteLine($"Swap count (Heaps): {Heaps.swapCount}");
            WriteLine($"Swap-factor (Heaps): {(double) Heaps.SwapCount / Length / Repetitions}");
            WriteLine($"Average time (QuickSelect): {quickSelectStopWatch.ElapsedMilliseconds / 1000.0 / Repetitions}");
            WriteLine($"Swap-factor (QuickSelect): {(double) QuickSelect.SwapCount / Length / Repetitions}");
            WriteLine($"Algorithm-factor (HeadMedian/Quickselect): {(double) heapMedianStopWatch.ElapsedMilliseconds / quickSelectStopWatch.ElapsedMilliseconds}");
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

        private static void DumpResult(int[] array, int repetitions, int median, long swapCount)
        {
            if (repetitions < 10)
            {
                WriteLine(median.ToString());
                GetCounts(array, median, out int smaller, out int bigger);
                WriteLine(smaller.ToString());
                WriteLine(bigger.ToString());
                WriteLine(swapCount.ToString());
                Dump(array);
            }
        }

        private static void GetCounts(int[] array, int value, out int smaller, out int bigger)
        {
            smaller = 0;
            bigger = 0;
            foreach (int currentElement in array)
            {
                if (currentElement < value)
                {
                    ++smaller;
                }
                else
                {
                    ++bigger;
                }
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