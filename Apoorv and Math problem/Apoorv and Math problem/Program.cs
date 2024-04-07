using System;
using System.Collections.Generic;

class Primality
{
    const int mod = 1000000007;

    long gcd(long a, long b)
    {
        return b == 0 ? a : gcd(b, a % b);
    }

    long mul_mod(long a, long b, long mod)
    {
        if (mod < (long)1e9) return (a * b) % mod;
        long k = (long)((double)a * b / mod);
        long res = (a * b) - (k * mod);
        res %= mod;
        if (res < 0) res += mod;
        return res;
    }

    long pow_mod(long a, long n, long m)
    {
        long res = 1;
        for (a %= m; n != 0; n >>= 1)
        {
            if ((n & 1) == 1) res = mul_mod(res, a, m);
            a = mul_mod(a, a, m);
        }
        return res;
    }

    bool is_prime(long n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if ((n & 1) == 0) return false;
        int[] u = {2, 3, 5, 7, 325, 9375, 28178, 450775, 9780504, 1795265022};
        long e = n - 1, a, c = 0;
        while ((e & 1) == 0) { e >>= 1; c++; }
        foreach (int i in u)
        {
            if (n <= i) return true;
            a = pow_mod(i, e, n);
            if (a == 1) continue;
            for (int j = 1; a != n - 1; j++)
            {
                if (j == c) return false;
                a = mul_mod(a, a, n);
            }
        }
        return true;
    }

    long pollard_rho(long n)
    {
        if (n <= 3 || is_prime(n)) return n;
        while (true)
        {
            int i = 1, cnt = 2;
            long x = new Random().Next() % n, y = x, c = new Random().Next() % n;
            if (c == 0 || c == n - 2) c++;
            do
            {
                long u = gcd(n - x + y, n);
                if (u > 1 && u < n) return u;
                if (++i == cnt) { y = x; cnt <<= 1; }
                x = (c + mul_mod(x, x, n)) % n;
            } while (x != y);
        }
    }

    List<KeyValuePair<long, int>> factorize(long n)
    {
        List<KeyValuePair<long, int>> fac = new List<KeyValuePair<long, int>>();
        int[] sp = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
        foreach (int i in sp)
        {
            if (n % i != 0) continue;
            int c = 0;
            while (n % i == 0) { n /= i; c++; }
            fac.Add(new KeyValuePair<long, int>(i, c));
        }
        List<long> u = new List<long>();
        if (n > 1) u.Add(n);
        for (int i = 0; i < u.Count; i++)
        {
            long x = pollard_rho(u[i]);
            if (x == u[i]) continue;
            u[i--] /= x;
            u.Add(x);
        }
        u.Sort();
        for (int i = 0, j, m = u.Count; i < m; i = j)
        {
            for (j = i; j < m && u[i] == u[j]; j++) ;
            fac.Add(new KeyValuePair<long, int>(u[i], j - i));
        }
        return fac;
    }

    static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        Primality pp = new Primality();
        for (int cas = 1; cas <= T; cas++)
        {
            long n = long.Parse(Console.ReadLine());
            var pf = pp.factorize(n);
            long cnt = 1;
            foreach (var e in pf)
            {
                cnt *= e.Value + 1;
            }
            long ret = 0;
            if ((cnt & 1) == 1)
            {
                long sq = (long)Math.Sqrt(n);
                while (sq * sq <= n) sq++;
                while (sq * sq > n) sq--;
                ret = pp.pow_mod(sq % mod, cnt, mod);
            }
            else
            {
                ret = pp.pow_mod(n % mod, cnt / 2, mod);
            }
            Console.WriteLine(ret);
        }
    }
}
