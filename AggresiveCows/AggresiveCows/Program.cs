using System;
using System.Linq;

namespace AggresiveCows
{
    class Program
    {
        static bool CheckDistance(long[] stalls, int cows, long minDistance)
        {
            long lastPosition = stalls[0];
            int count = 1;

            for (int i = 1; i < stalls.Length; i++)
            {
                if (stalls[i] - lastPosition >= minDistance)
                {
                    lastPosition = stalls[i];
                    count++;

                    if (count == cows)
                        return true;
                }
            }

            return false;
        }

        static long LargestMinimumDistance(int N, int C, long[] stalls)
        {
            Array.Sort(stalls);
            long start = 0;
            long end = stalls[N - 1] - stalls[0];
            long result = -1;

            while (start <= end)
            {
                long mid = start + (end - start) / 2;

                if (CheckDistance(stalls, C, mid))
                {
                    result = mid;
                    start = mid + 1;
                }
                else
                    end = mid - 1;
            }

            return result;
        }

        static void Main(string[] args)
        {
            int t = int.Parse(Console.ReadLine());

            for (int i = 0; i < t; i++)
            {
                string[] input = Console.ReadLine().Split();
                int N = int.Parse(input[0]);
                int C = int.Parse(input[1]);

                long[] stalls = new long[N];
                for (int j = 0; j < N; j++)
                    stalls[j] = long.Parse(Console.ReadLine());

                long largestMinDistance = LargestMinimumDistance(N, C, stalls);
                Console.WriteLine(largestMinDistance);
            }
        }
    }
}