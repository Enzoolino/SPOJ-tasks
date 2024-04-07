using System;
using System.Collections.Generic;

class Program
{
    const int me = 2525;
    const int mod = 1000000007;

    static int[,] c = new int[me, me];
    static int[] cnt = new int[me];

    static int Power(int a, int b)
    {
        if (b == 0)
        {
            return 1;
        }
        if ((b & 1) == 1)
        {
            return (int)((long)Power(a, b - 1) * a % mod);
        }
        int half = Power(a, b >> 1);
        return (int)((long)half * half % mod);
    }

    static int Inverse(int a)
    {
        return Power(a, mod - 2);
    }

    static void Main(string[] args)
    {
        int MAGIC = Inverse(2);

        for (int i = 0; i < me; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (j == 0)
                {
                    c[i, j] = 1;
                }
                else
                {
                    c[i, j] = (c[i - 1, j] + c[i - 1, j - 1]) % mod;
                }
            }
        }

        int T, N;
        T = int.Parse(Console.ReadLine());
        while (T-- > 0)
        {
            N = int.Parse(Console.ReadLine());
            int result = 0;
            for (int i = 1; i <= N; i++)
            {
                for (int j = i + 1; j <= N; j++)
                {
                    result = (result + (int)((long)GCD(i, j) * c[N, i] % mod * c[N, j] % mod)) % mod;
                }
                result = (result + (int)((long)i * c[N, i] % mod * (c[N, i] + mod - 1) % mod * MAGIC % mod)) % mod;
            }
            Console.WriteLine(result);
        }
    }

    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
