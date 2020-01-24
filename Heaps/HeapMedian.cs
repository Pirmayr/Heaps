// ReSharper disable TooWideLocalVariableScope
// ReSharper disable JoinDeclarationAndInitializer

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

        private static void Maxify1(int[] array, int length, int root)
        {
            const int BlockSize = 2;
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

        private static void Swap(ref int a, ref int b)
        {
            ++swapCount;
            int temporaryValue = a;
            a = b;
            b = temporaryValue;
        }
    }
}