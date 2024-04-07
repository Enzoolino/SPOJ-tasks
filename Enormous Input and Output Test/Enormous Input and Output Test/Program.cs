class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        
        int[,] integers = new int[n, 2];

        for (int i = 0; i < n; i++)
        {
            string[] inputNumebrs = Console.ReadLine().Split(' ');
            integers[i, 0] = int.Parse(inputNumebrs[0]);
            integers[i, 1] = int.Parse(inputNumebrs[1]);
        }

        for (int j = 0; j < n; j++)
        {
            Console.WriteLine(integers[j, 0] * integers[j, 1]);
        }
    }
}