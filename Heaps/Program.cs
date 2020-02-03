using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Heaps
{
    public static class Example
    {
        public static void Main()
        {
            const int Repetitions = 1;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<AlgorithmInformation> algorithmInformations = new List<AlgorithmInformation> {new AlgorithmInformation(Heaps.SinkStandard), new AlgorithmInformation(Heaps.SinkWeakStandard), new AlgorithmInformation(Heaps.SinkWeak)};
            for (int length = 10000000; length <= 10000000; length *= 2)
            {
                foreach (AlgorithmInformation currentAlgorithmInformation in algorithmInformations)
                {
                    currentAlgorithmInformation.Reset();
                }
                int[] array = new int[length];
                for (int currentRepetition = 0; currentRepetition < Repetitions; ++currentRepetition)
                {
                    int seed = new Random().Next();
                    foreach (AlgorithmInformation currentAlgorithmInformation in algorithmInformations)
                    {
                        TestMaxify(array, seed, currentAlgorithmInformation.SinkMethod, currentRepetition, ref currentAlgorithmInformation.maxifyComparisons, ref currentAlgorithmInformation.maxifySwaps);
                        TestSink(array, seed, currentAlgorithmInformation.SinkMethod, currentRepetition, ref currentAlgorithmInformation.sinkComparisons, ref currentAlgorithmInformation.sinkSwaps);
                    }
                }
                foreach (AlgorithmInformation currentAlgorithmInformation in algorithmInformations)
                {
                    currentAlgorithmInformation.maxifyComparisonValues.Add(length, (double) currentAlgorithmInformation.maxifyComparisons / Repetitions);
                    currentAlgorithmInformation.maxifySwapValues.Add(length, (double) currentAlgorithmInformation.maxifySwaps / Repetitions);
                    currentAlgorithmInformation.sinkComparisonValues.Add(length, (double) currentAlgorithmInformation.sinkComparisons / Repetitions);
                    currentAlgorithmInformation.sinkSwapValues.Add(length, (double) currentAlgorithmInformation.sinkSwaps / Repetitions);
                    Console.WriteLine($"Maxify; Average Swaps: {currentAlgorithmInformation.maxifySwaps / Repetitions} Average Comparisons: {currentAlgorithmInformation.maxifyComparisons / Repetitions}");
                    Console.WriteLine($"Sink; Average Swaps: {currentAlgorithmInformation.sinkSwaps / Repetitions} Average Comparisons: {currentAlgorithmInformation.sinkComparisons / Repetitions}");
                }
            }
            foreach (AlgorithmInformation currentAlgorithmInformation in algorithmInformations)
            {
                Console.WriteLine($"Algorithm {currentAlgorithmInformation.SinkMethod.Method.Name}:");
                Console.WriteLine("Sink-Comparisons:");
                foreach (KeyValuePair<int, double> currentValue in currentAlgorithmInformation.sinkComparisonValues)
                {
                    Console.WriteLine($"{currentValue.Key} {currentValue.Value}");
                }
                Console.WriteLine("Maxify-Comparisons:");
                foreach (KeyValuePair<int, double> currentValue in currentAlgorithmInformation.maxifyComparisonValues)
                {
                    Console.WriteLine($"{currentValue.Key} {currentValue.Value}");
                }
            }
        }

        private static void Dump(int[] array)
        {
            if (array.Length <= 60)
            {
                for (int i = 0; i < array.Length; ++i)
                {
                    Console.Write("{0,3}", i);
                }
                Console.WriteLine("");
                foreach (int currentElement in array)
                {
                    Console.Write("{0,3}", currentElement);
                }
                Console.WriteLine("");
            }
        }

        private static void InitializeArray(int[] array, int seed)
        {
            Random random = new Random(seed);
            int length = array.Length;
            for (int i = 0; i < length; ++i)
            {
                array[i] = random.Next();
            }
        }

        private static int TestMaxify(int[] array, int seed, SinkMethod sinkMethod, int repetitionIndex, ref long comparisonsSum, ref long swapsSum)
        {
            InitializeArray(array, seed);
            if (repetitionIndex == 0)
            {
                Dump(array);
            }
            Heaps.Maxify(array, sinkMethod);
            if (repetitionIndex == 0)
            {
                Dump(array);
                Console.WriteLine($"Maximum: {Heaps.maximum} Seed: {seed} Swaps: {Heaps.swaps} Comparisons: {Heaps.comparisons}", new object[0]);
            }
            comparisonsSum += Heaps.comparisons;
            swapsSum += Heaps.swaps;
            return Heaps.maximum;
        }

        private static int TestSink(int[] array, int seed, SinkMethod sinkMethod, int repetitionIndex, ref long comparisonsSum, ref long swapsSum)
        {
            int value = new Random(seed * 2).Next(array.Length);
            array[0] = value;
            if (repetitionIndex == 0)
            {
                Dump(array);
            }
            Heaps.Sink(array, sinkMethod);
            if (repetitionIndex == 0)
            {
                Dump(array);
                Console.WriteLine($"Maximum: {Heaps.maximum} Seed: {seed} Swaps: {Heaps.swaps} Comparisons: {Heaps.comparisons}", new object[0]);
            }
            comparisonsSum += Heaps.comparisons;
            swapsSum += Heaps.swaps;
            return Heaps.maximum;
        }

        private class AlgorithmInformation
        {
            public long maxifyComparisons;
            public long maxifySwaps;
            public long sinkComparisons;
            public long sinkSwaps;

            public readonly Dictionary<int, double> maxifyComparisonValues = new Dictionary<int, double>();
            public readonly Dictionary<int, double> maxifySwapValues = new Dictionary<int, double>();
            public readonly Dictionary<int, double> sinkComparisonValues = new Dictionary<int, double>();
            public readonly Dictionary<int, double> sinkSwapValues = new Dictionary<int, double>();

            public SinkMethod SinkMethod { get; }

            public AlgorithmInformation(SinkMethod sinkMethod)
            {
                SinkMethod = sinkMethod;
            }

            public void Reset()
            {
                maxifyComparisons = 0;
                maxifySwaps = 0;
                sinkComparisons = 0;
                sinkSwaps = 0;
            }
        }
    }
}