using System;

namespace Heaps
{
    public static class QuickSelect
    {
        public static int SwapCount { get; private set; }

        public static int GetMedianIndex(int[] list, int lo, int hi, int k)
        {
            while (true)
            {
                int n = hi - lo;
                if (n < 2)
                {
                    return lo;
                }
                int a = list[0];
                int b = list[(lo + hi) / 2];
                int c = list[hi - 1];
                int pivot = Math.Max(Math.Min(a, b), Math.Min(Math.Max(a, b), c));
                int nLess = 0;
                int nSame = 0;
                int nMore = 0;
                int lo3 = lo;
                int hi3 = hi;
                int indexB;
                while (lo3 < hi3)
                {
                    int cmp = list[lo3].CompareTo(pivot);
                    if (cmp < 0)
                    {
                        nLess++;
                        lo3++;
                    }
                    else if (cmp > 0)
                    {
                        indexB = --hi3;
                        int tmp = list[lo3];
                        list[lo3] = list[indexB];
                        list[indexB] = tmp;
                        if (nSame > 0)
                        {
                            indexB = hi3 + nSame;
                            int tmp1 = list[hi3];
                            list[hi3] = list[indexB];
                            list[indexB] = tmp1;
                        }
                        nMore++;
                    }
                    else
                    {
                        nSame++;
                        indexB = --hi3;
                        int tmp = list[lo3];
                        list[lo3] = list[indexB];
                        list[indexB] = tmp;
                    }
                }
                if (k >= n - nMore)
                {
                    lo = hi - nMore;
                    k = k - nLess - nSame;
                    continue;
                }
                if (k < nLess)
                {
                    int lo1 = lo;
                    hi = lo1 + nLess;
                    continue;
                }
                return lo + k;
            }
        }

        private static void Swap(this int[] list, int indexA, int indexB)
        {
            // ++SwapCount;
            int tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}