class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        string[] inputNumbers = input.Split(' ');
        
        int n = int.Parse(inputNumbers[0]);
        int k = int.Parse(inputNumbers[1]);

        int[] integers = new int[n];

        for (int i = 0; i < n; i++)
        {
            integers[i] = int.Parse(Console.ReadLine());
        }

        int divCounter = 0;

        foreach (var integer in integers)
        {
            if (integer % k == 0)
            {
                divCounter++;
            }
        }
        
        Console.WriteLine(divCounter);

    }
}
