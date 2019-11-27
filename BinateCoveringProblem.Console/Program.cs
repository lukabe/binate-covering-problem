namespace BinateCoveringProblem.Console
{
    using BinateCoveringProblem.Console.Extensions;
    using BinateCoveringProblem.Core.Algorithms.Covering;
    using BinateCoveringProblem.Core.Extensions;
    using Serilog;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            string inputString;
            int rowsCount, columnsCount;

            do
            {
                Console.Write("Input number of rows: ");
                inputString = Console.ReadLine();
            }
            while (!int.TryParse(inputString, out rowsCount));

            do
            {
                Console.Write("Input number of columns: ");
                inputString = Console.ReadLine();
            }
            while (!int.TryParse(inputString, out columnsCount));

            int[,] matrix = new int[rowsCount, columnsCount];

            matrix.Fill();
            Console.Clear();

            var source = matrix.ToDictionary();

            Log.Information($"Source: {source.Print()}");

            var covering = source.IsBinate() 
                ? (ICoveringAlgorithm)new BinateCovering(source) 
                : new UnateCovering(source);

            Log.Information($"Solution: {covering.Result.Print()}");

            System.Console.ReadLine();
        }
    }
}
