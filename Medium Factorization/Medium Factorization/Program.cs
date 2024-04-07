using System;
using System.IO;
using System.Text;

class Program
{
    static int[] fact = new int[10000001];
    static int[] n = new int[10000001];

    static void Pre()
    {
        for (int i = 2; i < 10000001; i += 2)
        {
            fact[i] = 2;
            n[i] = i >> 1;
        }
        for (int i = 3; i < 10000001; i += 2)
        {
            if (fact[i] == 0)
            {
                fact[i] = i;
                if (i < 3163)
                {
                    int k = i << 1;
                    for (int j = i * i; j < 10000001; j += k)
                    {
                        if (fact[j] == 0)
                        {
                            fact[j] = i;
                            n[j] = j / i;
                        }
                    }
                }
            }
        }
    }

    static async System.Threading.Tasks.Task Main(string[] args)
    {
        Pre();
        int[] f = new int[250];
        string line;
        StringBuilder output = new StringBuilder();

        while ((line = await Console.In.ReadLineAsync()) != null)
        {
            int x = int.Parse(line);
            f[0] = 1;
            int j = 0;
            while (x > 1)
            {
                f[++j] = fact[x];
                x = n[x];
            }
            for (int i = 0; i < j; i++)
            {
                output.Append($"{f[i]} x ");
            }
            output.AppendLine(f[j].ToString());
        }
        Console.Write(output);
    }
}
