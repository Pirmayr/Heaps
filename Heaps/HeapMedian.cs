// ReSharper disable TooWideLocalVariableScope
// ReSharper disable JoinDeclarationAndInitializer

// hugo 1

using System;
using System.Diagnostics;

namespace Heaps
{
    public static class HeapMedian
    {
        public static int comparisonCount;
        public static int swapCount;

        public static int Maximum1(int[] array)
        {
            swapCount = 0;
            comparisonCount = 0;
            int length = array.Length;
            int root = length - 1;
            while (root >= 0)
            {
                Maxify1(array, length, root);
                --root;
            }
            return array[0];
        }

        public static int Maximum2(int[] array)
        {
            swapCount = 0;
            comparisonCount = 0;
            int length = array.Length;
            int root = length - 1;
            while (root >= 0)
            {
                Maxify2(array, length, root);
                --root;
            }
            return array[0];
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

        private static void Maxify1(int[] array, int length, int root)
        {
            const int BlockSize = 10000;
            int currentRoot;
            int nextRoot;
            long child;
            nextRoot = root;
            while (true)
            {
                currentRoot = nextRoot;
                child = (long) nextRoot * BlockSize;
                for (int i = 0; i < BlockSize; ++i)
                {
                    if (++child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = (int) child;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (nextRoot != currentRoot)
                {
                    Swap(ref array[nextRoot], ref array[currentRoot]);
                    continue;
                }
                break;
            }
        }

        private static void Maxify2(int[] array, int length, int root)
        {
            int currentRoot;
            int nextRoot;
            int child;
            nextRoot = root;
            while (true)
            {
                currentRoot = nextRoot;
                switch (currentRoot % 3)
                {
                case 0:
                    child = currentRoot * 2 + 1;
                    if (child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = child;
                        }
                    }
                    child = child + 2;
                    if (child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = child;
                        }
                    }
                    if (nextRoot != currentRoot)
                    {
                        Swap(ref array[nextRoot], ref array[currentRoot]);
                        continue;
                    }
                    break;
                case 1:
                    child = currentRoot + 1;
                    if (child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = child;
                        }
                    }
                    if (nextRoot != currentRoot)
                    {
                        Swap(ref array[nextRoot], ref array[currentRoot]);
                        continue;
                    }
                    break;
                case 2:
                    child = currentRoot * 2;
                    if (child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = child;
                        }
                    }
                    child = child + 2;
                    if (child < length)
                    {
                        ++comparisonCount;
                        if (array[child] > array[nextRoot])
                        {
                            nextRoot = child;
                        }
                    }
                    if (nextRoot != currentRoot)
                    {
                        Swap(ref array[nextRoot], ref array[currentRoot]);
                        continue;
                    }
                    break;
                }
                break;
            }
        }

        /*
        public static int GetMedian(int[] array)
        {
            int length = array.Length;
            int length2 = length / 2;
            int length1 = length - length2;
            int offset = 1 - 2 * length1;
            int limit2 = (length - offset + 2) / 3;
            int limit1 = (length1 + 1) / 3;
            int root = length1 / 2 - 1;
            int temporaryValue;
            while (root >= 0)
            {
                while (true)
                {
                    Maxify1(array, length, root, length1, limit1);
                    Minify1(array, root, length, limit2, length1, offset);
                    if (array[root] <= array[root + length1])
                    {
                        break;
                    }
                    temporaryValue = array[root];
                    array[root] = array[root + length1];
                    array[root + length1] = temporaryValue;
                }
                --root;
            }
            return array[length1];
        }
        */

        private static void Minify1(int[] array, int root, int high, int limit, int baseOffset, int offset)
        {
            int currentRoot;
            int nextRoot;
            int child;
            nextRoot = root + baseOffset;
            while (nextRoot < limit)
            {
                currentRoot = nextRoot;
                child = nextRoot * 3 + offset;
                switch (high - child)
                {
                case 2:
                    nextRoot = array[child] < array[nextRoot] ? child : nextRoot;
                    break;
                case 3:
                    nextRoot = array[child] < array[nextRoot] ? child : nextRoot;
                    nextRoot = array[++child] < array[nextRoot] ? child : nextRoot;
                    break;
                default:
                    nextRoot = array[child] < array[nextRoot] ? child : nextRoot;
                    nextRoot = array[++child] < array[nextRoot] ? child : nextRoot;
                    nextRoot = array[++child] < array[nextRoot] ? child : nextRoot;
                    break;
                }
                if (nextRoot == currentRoot)
                {
                    break;
                }
                Swap(ref array[currentRoot], ref array[nextRoot]);
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            ++swapCount;
            int temporaryValue = a;
            a = b;
            b = temporaryValue;
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