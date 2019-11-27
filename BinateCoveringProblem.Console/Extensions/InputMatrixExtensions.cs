namespace BinateCoveringProblem.Console.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class InputMatrixExtensions
    {
        public static void Print(this int[,] matrix, int? rowIndex = null, int? columnIndex = null)
        {
            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            for (int r = 0; r < rowsCount; r++)
            {
                Console.Write(" [ ");
                for (int c = 0; c < columnsCount; c++)
                {
                    var next = $"{matrix[r, c]}";

                    if (rowIndex.HasValue && rowIndex.Equals(r) && columnIndex.HasValue && columnIndex.Equals(c))
                    {
                        ConsoleExtensions.Write(next, ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        Console.Write(next);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("]");
            }
        }

        public static void Fill(this int[,] matrix)
        {
            string inputString;

            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < columnsCount; c++)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Fill the source set: ");
                        matrix.Print(r, c);

                        Console.Write("Input value of next element {-1, 0, 1}: ");
                        inputString = Console.ReadLine();
                    }
                    while (inputString != "-1" && inputString != "0" && !string.IsNullOrEmpty(inputString) && inputString != "1");

                    matrix[r, c] = !string.IsNullOrEmpty(inputString) ? int.Parse(inputString) : 0;
                }
            }
        }

        public static Dictionary<int, List<int>> ToDictionary(this int[,] matrix)
        {
            var source = new Dictionary<int, List<int>>();

            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            for (int r = 1; r < rowsCount + 1; r++)
            {
                source.Add(r, new List<int>());

                for (int c = 1; c < columnsCount + 1; c++)
                {
                    var value = matrix[r - 1, c - 1];
                    if (value > 0)
                    {
                        source[r].Add(c);
                    }
                    else if (value < 0)
                    {
                        source[r].Add(-c);
                    }
                }

                if (!source[r].Any())
                {
                    source.Remove(r);
                }
            }

            return source;
        }
    }
}
