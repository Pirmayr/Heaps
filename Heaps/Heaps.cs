// ReSharper disable TooWideLocalVariableScope
// ReSharper disable ConvertToCompoundAssignment

namespace Heaps
{
    public delegate void SinkMethod(int[] array, int root);

    public static class Heaps
    {
        public static int comparisons;
        public static int maximum;
        public static int swaps;

        public static void Maxify(int[] array, SinkMethod sinkMethod)
        {
            swaps = 0;
            comparisons = 0;
            int length = array.Length;
            int root = length;
            while (root >= 0)
            {
                sinkMethod(array, root);
                --root;
            }
            maximum = array[0];
        }

        public static void Sink(int[] array, SinkMethod sinkMethod)
        {
            swaps = 0;
            comparisons = 0;
            sinkMethod(array, 0);
            maximum = array[0];
        }

        private static int NextRoot(int[] array, long child, int nextRoot)
        {
            ++comparisons;
            if (array[child] > array[nextRoot])
            {
                nextRoot = (int) child;
            }
            return nextRoot;
        }

        public static void SinkStandard(int[] array, int root)
        {
            int length = array.Length;
            int nextRoot = root;
            while (true)
            {
                int currentRoot = nextRoot;
                long child = (long) nextRoot * 2;
                for (int i = 0; i < 2; ++i)
                {
                    if (++child < length)
                    {
                        nextRoot = NextRoot(array, child, nextRoot);
                        continue;
                    }
                    break;
                }
                if (nextRoot != currentRoot)
                {
                    Swap(ref array[nextRoot], ref array[currentRoot]);
                    continue;
                }
                break;
            }
        }

        public static void SinkWeak(int[] array, int root)
        {
            const int childrenCount = 5;
            int length = array.Length;
            int child;
            int nextRoot = root;
            while (true)
            {
                int currentRoot = nextRoot;
                for (int i = 1; i <= childrenCount; ++i)
                {
                    child = currentRoot;
                    if (child == 0 || (child + childrenCount - i) % childrenCount != 0)
                    {
                        while (true)
                        {
                            child = child * childrenCount + i;
                            if (child < length)
                            {
                                nextRoot = NextRoot(array, child, nextRoot);
                                continue;
                            }
                            break;
                        }
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

        public static void SinkWeakStandard(int[] array, int root)
        {
            int length = array.Length;
            int nextRoot = root;
            while (true)
            {
                int currentRoot = nextRoot;
                int child = nextRoot * 2 + 1;
                if (child < length)
                {
                    nextRoot = NextRoot(array, child, nextRoot);
                    while (true)
                    {
                        child *= 2;
                        if (child < length)
                        {
                            nextRoot = NextRoot(array, child, nextRoot);
                            continue;
                        }
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

        private static void Swap(ref int a, ref int b)
        {
            ++swaps;
            int temporaryValue = a;
            a = b;
            b = temporaryValue;
        }
    }
}